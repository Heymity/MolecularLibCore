/*  Copyright 2022 Gabriel Pasquale Rodrigues Scavone
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using MolecularLib;
using MolecularLib.Helpers;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace MolecularEditor
{
    public class ToggleableProp<T1>
    {
        public T1 Value { get; set; }

        public bool Active { get; set; }

        public void DoEditor(string toggleLabel, Action drawer)
        {
            Active = EditorGUILayout.Toggle(toggleLabel, Active);
            EditorGUI.BeginDisabledGroup(!Active);
            drawer?.Invoke();
            EditorGUI.EndDisabledGroup();
        }

        public ToggleableProp(bool active, T1 value = default)
        {
            Active = active;
            Value = value;
        }

        public ToggleableProp() : this(false) { }

        public static implicit operator bool(ToggleableProp<T1> a) => a.Active;

        public static implicit operator T1(ToggleableProp<T1> a) => a.Value;

        public static implicit operator ToggleableProp<T1>(bool a) => new ToggleableProp<T1>(a);

        public static implicit operator ToggleableProp<T1>(T1 a) => new ToggleableProp<T1>(false, a);

        public static implicit operator ToggleableProp<T1>((bool active, T1 value) a) => new ToggleableProp<T1>(a.active, a.value);
    }

    public static class EditorHelper
    {
        #region IMGUI

        private static readonly GUIStyle headerBackground = "RL Header";
        private static readonly GUIStyle boxBackground = "RL Background";

        // TODO Make a foldout option with this
        public static Rect DrawBoxWithTitle(Rect totalPos, GUIContent tittle)
        {
            if (Event.current.type == EventType.Repaint)
                headerBackground.Draw(totalPos, false, false, false, false);
   
            var fieldRect = totalPos;
            fieldRect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(fieldRect, tittle);
  
            var boxRect = fieldRect;
            boxRect.y += fieldRect.height;
            boxRect.height = totalPos.height - fieldRect.height;
            if (Event.current.type == EventType.Repaint)
                boxBackground.Draw(boxRect, false, false, false, false);

            var boxAreaRect = boxRect;
            boxAreaRect.height -= 6;
            boxAreaRect.y += 3;

            return boxAreaRect;
        }

        public static Rect BeginBoxWithTittle(GUIContent tittle, params GUILayoutOption[] options)
        {
            var tittleRect = EditorGUILayout.GetControlRect();
            
            if (Event.current.type == EventType.Repaint)
                headerBackground.Draw(tittleRect, false, false, false, false);

            tittleRect.x += 2;
            tittleRect.height = EditorGUIUtility.singleLineHeight + 2;
            EditorGUI.LabelField(tittleRect, tittle);

            var paddingStyle = new GUIStyle
            {
                padding = new RectOffset(8, 8, 8, 8)
            };
            var verticalRect = EditorGUILayout.BeginVertical(paddingStyle, options);
            
            if (Event.current.type == EventType.Repaint)
                boxBackground.Draw(verticalRect, false, false, false, false);

            return verticalRect;
        }

        public static void EndBoxWithTittle()
        {
            EditorGUILayout.EndVertical();
        }
        
       public static void AutoTypeFieldInfo(Rect rect, FieldInfo fi, object targetObj, string label = null)
        {
            var field = fi.GetValue(targetObj);
            label ??= fi.Name;

            fi.SetValue(targetObj, AutoTypeField(rect, fi.FieldType, field, label));
        }

        public static void AutoTypePropertyInfo(Rect rect, PropertyInfo pi, object targetObj, string label = null)
        {
            var field = pi.GetValue(targetObj);
            label ??= pi.Name;

            // some properties dont have the set method, to be refactored
            pi.SetValue(targetObj, AutoTypeField(rect, pi.PropertyType, field, label));
        }

        public static object AutoTypeField(Rect rect, Type valueType, object value, string labelStr = null)
        {
            var label = GUIContent.none;
            if (!string.IsNullOrEmpty(labelStr)) label = new GUIContent(labelStr);

            if (valueType == typeof(int)) return EditorGUI.IntField(rect, label, value is int i ? i : 0);
            if (valueType == typeof(float)) return EditorGUI.FloatField(rect, label, value is float i ? i : 0);
            if (valueType == typeof(double)) return EditorGUI.DoubleField(rect, label, value is double i ? i : 0);
            if (valueType == typeof(bool)) return EditorGUI.Toggle(rect, label, value is bool i && i);
            if (valueType == typeof(string)) return EditorGUI.TextField(rect, label, value is string i ? i : "");
            if (valueType == typeof(Vector3)) return EditorGUI.Vector3Field(rect, label, value is Vector3 vec3 ? vec3 : Vector3.zero);
            if (valueType == typeof(Vector3Int)) return EditorGUI.Vector3IntField(rect, label, value is Vector3Int vec3Int ? vec3Int : Vector3Int.zero);
            if (valueType == typeof(Vector2)) return EditorGUI.Vector2Field(rect, label, value is Vector2 vec2 ? vec2 : Vector2.zero);
            if (valueType == typeof(Vector2Int)) return EditorGUI.Vector2IntField(rect, label, value is Vector2Int vec2Int ? vec2Int : Vector2Int.zero);

            if (valueType.IsEnum)
                return EditorGUI.EnumPopup(rect, label, (Enum) Enum.Parse(valueType, value?.ToString() ?? ""));

            if (valueType.IsSubclassOf(typeof(Object)))
                return EditorGUI.ObjectField(rect, label, value as Object, valueType, true);

            if (valueType == typeof(object))
                return null;

            EditorGUI.LabelField(rect, label, new GUIContent(value?.ToString() ?? $"{valueType.Name} has null value and is not supported"));

            return value;
        }
        public static readonly Dictionary<string, (Func<Rect, string, object, object> drawer, Type type)> ObjectTypes = new Dictionary<string, (Func<Rect, string, object, object> drawer, Type type)>
        {
            { "Bool", ((rect, label, value) => EditorGUI.Toggle(rect, label, value is bool v && v), typeof(bool)) }, 
            { "Int", ((rect, label, value) => EditorGUI.IntField(rect, label, value is int v ? v : 0), typeof(int)) }, 
            { "Float", ((rect, label, value) => EditorGUI.FloatField(rect, label, value is float v ? v : 0f), typeof(float)) }, 
            { "Object", ((rect, label, value) 
                => EditorGUI.ObjectField(rect, label, value is Object obj ? obj : null, value?.GetType() ?? typeof(Object), true), typeof(Object)) }, 
            { "Vector3", ((rect, label, value) => EditorGUI.Vector3Field(rect, label, value is Vector3 v ? v : Vector3.zero), typeof(Vector3)) }, 
            { "Vector2", ((rect, label, value) => EditorGUI.Vector2Field(rect, label, value is Vector2 v ? v : Vector2.zero), typeof(Vector2)) },
        };
        public static object ObjectField(Rect rect, object value, string label, ref string selectedType)
        {
            var btnRect = new Rect(rect.x, rect.y, rect.width / ObjectTypes.Count, EditorGUIUtility.singleLineHeight);
            for (var i = 0; i < ObjectTypes.Count; i++)
            {
                var style = EditorStyles.miniButtonMid;
                if (i == 0) style = EditorStyles.miniButtonLeft;
                if (i == ObjectTypes.Count - 1) style = EditorStyles.miniButtonRight;

                var thisElement = ObjectTypes.ElementAt(i);

                if (GUI.Toggle(btnRect, thisElement.Key == selectedType, thisElement.Key, style))
                    selectedType = thisElement.Key;

                btnRect.x += btnRect.width;
            }

            var valueRect = new Rect(rect.x, rect.y + btnRect.height + 2f, rect.width, EditorGUIUtility.singleLineHeight);
            value = ObjectTypes[selectedType].drawer?.Invoke(valueRect, label, value);

            return value;
        }       
        
        public static Type TypeField<TBaseClass>(Rect rect, string label, Type currentValue, bool showBaseType)
        {
            return TypeField(rect, label, currentValue, typeof(TBaseClass), showBaseType);
        }

        public static Type TypeField(Rect rect, string label, Type currentValue, Type baseType, bool showBaseType)
        {
            var types = GetTypesForPopup(baseType, showBaseType);

            var r = DrawTypeField(rect, label, types, currentValue);
            if (r != currentValue) GUI.changed = true;
            
            return r;
        }

        /* These require a different optimization and caching approach to be useful
        public static Type TypeField(Rect rect, string label, Type currentValue, Assembly assembly)
        {
            var types = assembly.GetTypes().ToList();

            return DrawTypeField(rect, label, types, currentValue);
        }

        public static Type TypeField<TBaseClass>(Rect rect, string label, Type currentValue, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(TBaseClass))).ToList();

            return DrawTypeField(rect, label, types, currentValue);
        }

        public static Type TypeField(Rect rect, string label, Type currentValue, Type baseType, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => type.IsSubclassOf(baseType)).ToList();

            return DrawTypeField(rect, label, types, currentValue);
        }*/
        
        public static Type DrawTypeField(Rect rect, string label, List<Type> types, Type current)
        {
            var selected = types.FindIndex(t => t == current);
            if (selected <= 0) selected = 0;

            var popupNames = types.Select(t => t.FullName?.Replace('.', '/')).ToArray();

            selected = EditorGUI.Popup(
                rect,
                label,
                selected,
                popupNames);

            return types.Count <= 0 ? null : types[selected];
        }

        #endregion

        #region UIElements

        public static void LinkNodes(this GraphView graphView, Port outputPort, Port inputPort)
        {
            var tmpEdge = new Edge
            {
                output = outputPort,
                input = inputPort
            };

            tmpEdge.input.Connect(tmpEdge);
            tmpEdge.output.Connect(tmpEdge);

            graphView.Add(tmpEdge);
        }

        public static Button ButtonWithText(Action clickEvent, string text)
        {
            var btn = new Button(clickEvent)
            {
                text = text
            };

            return btn;
        }

        public static ToolbarButton ToolbarButtonWithText(Action clickEvent, string text)
        {
            var btn = new ToolbarButton(clickEvent)
            {
                text = text
            };

            return btn;
        }

        public static void MakeBorder(this VisualElement container, float borderWidth, Color borderColor, float borderCornerRadius = 0)
        {
            container.MakeTopBorder(borderWidth, borderColor, borderCornerRadius);
            container.MakeBottomBorder(borderWidth, borderColor, borderCornerRadius);
            container.MakeLeftBorder(borderWidth, borderColor, borderCornerRadius);
            container.MakeRightBorder(borderWidth, borderColor, borderCornerRadius);
        }

        public static void MakeTopBorder(this VisualElement container, float borderWidth, Color borderColor, float borderCornerRadius = 0)
        {
            container.style.borderTopWidth = borderWidth;
            container.style.borderTopColor = borderColor;
            container.style.borderTopLeftRadius = borderCornerRadius;
            container.style.borderTopLeftRadius = borderCornerRadius;
        }
        public static void MakeBottomBorder(this VisualElement container, float borderWidth, Color borderColor, float borderCornerRadius = 0)
        {
            container.style.borderBottomWidth = borderWidth;
            container.style.borderBottomColor = borderColor;
            container.style.borderBottomLeftRadius = borderCornerRadius;
            container.style.borderBottomRightRadius = borderCornerRadius;
        }
        public static void MakeLeftBorder(this VisualElement container, float borderWidth, Color borderColor, float borderCornerRadius = 0)
        {
            container.style.borderLeftWidth = borderWidth;
            container.style.borderLeftColor = borderColor;
            container.style.borderTopLeftRadius = borderCornerRadius;
            container.style.borderBottomLeftRadius = borderCornerRadius;
        }
        public static void MakeRightBorder(this VisualElement container, float borderWidth, Color borderColor, float borderCornerRadius = 0)
        {
            container.style.borderRightWidth = borderWidth;
            container.style.borderRightColor = borderColor;
            container.style.borderTopRightRadius = borderCornerRadius;
            container.style.borderBottomRightRadius = borderCornerRadius;
        }

        #endregion

        #region General Utilities

        public const BindingFlags UnitySerializesBindingFlags = BindingFlags.Instance |
                                                                BindingFlags.Public |
                                                                BindingFlags.NonPublic |
                                                                BindingFlags.FlattenHierarchy;

        private static List<Type> _cachedTypes;
        public static IReadOnlyList<Type> AllTypes
        {
            get
            {
                if (_cachedTypes != null && _cachedTypes.Count > 0) return _cachedTypes;
                
                if (TypeLibrary.AllAssemblies == null || TypeLibrary.AllAssemblies.Count == 0)
                {
                    TypeLibrary.BootstrapEditor();
                }
                
                _cachedTypes = TypeLibrary.AllAssemblies!.SelectMany(a => a.Value.GetTypes()).ToList();
                return _cachedTypes;
            }
        }

        private static readonly Dictionary<string, List<Type>> cachedDerivedTypes = new Dictionary<string, List<Type>>();

        public static List<Type> GetTypesForPopup<TBaseClass>(bool showBaseType) => GetTypesForPopup(typeof(TBaseClass), showBaseType);
        public static List<Type> GetTypesForPopup(Type baseType, bool showBaseType)
        {
            if (cachedDerivedTypes.TryGetValue(baseType.ToString(), out var derivedTypes))
                return derivedTypes;
            
            var types = AllTypes.Where(baseType.IsAssignableFrom).Where(t => showBaseType || t != baseType).ToList();
            cachedDerivedTypes.Add(baseType.ToString(), types);

            return types;
        }

        public static Texture2D Tex2DOfColorScreenSize(Color32 color) => Tex2DOfColorAndSize(color, Screen.width, Screen.height);

        public static Texture2D Tex2DOfColorAndSize(Color32 color, int width, int height)
        {
            var texture = new Texture2D(width, height);
            var pixels = Enumerable.Repeat(color, width * height).ToArray();
            texture.SetPixels32(pixels);
            texture.Apply();
            return texture;
        }
        
        public static Texture2D Tex2DOfColorScreenSize(Color color) => Tex2DOfColorScreenSize(color.ToColor32());

        public static Texture2D Tex2DOfColorAndSize(Color color, int width, int height) => Tex2DOfColorAndSize(color.ToColor32(), width, height);

        public static Texture2D Tex2DOfColor(Color color)
        {
            var newTex = new Texture2D(1, 1);
            newTex.SetPixel(0, 0, color);
            newTex.Apply();
            return newTex;
        }
 
        public static T GetTargetValue<T>(SerializedProperty property)
        {
            var propertyPath = property.propertyPath;
            var pathSegments = propertyPath.Split('.');
            
            object currentSearchObj = property.serializedObject.targetObject;
            var currentSearchObjType = currentSearchObj.GetType();

            for (var i = 0; i < pathSegments.Length; i++)
            {
                var pathSegment = pathSegments[i];
                
                if (pathSegment == "Array" && i < pathSegments.Length - 1 && pathSegments[i + 1].Split('[')[0] == "data")
                {
                    var arrayIndex = int.Parse(pathSegments[i + 1].Split('[', ']')[1]);
                    
                    currentSearchObj = ((IList)currentSearchObj)[arrayIndex];
                    currentSearchObjType = currentSearchObj.GetType();
                    
                    // Skip the data[] part of the path
                    i++;
                    continue;
                }

                var fieldInfo = currentSearchObjType.GetField(pathSegment, UnitySerializesBindingFlags);

                if (fieldInfo == null)
                {
                    Debug.LogError($"Field {pathSegment} not found on {currentSearchObjType}");
                    return default;
                }

                currentSearchObj = fieldInfo.GetValue(currentSearchObj);
                currentSearchObjType = currentSearchObj.GetType();
            }

            return (T)currentSearchObj;
        }

        internal static bool IsGenericList(this object o)
        {
            var oType = o.GetType();
            return oType.IsGenericType && oType.GetGenericTypeDefinition() == typeof(List<>);
        }

        public static Color GetColorFromString(string value, byte minRGBValue = 0, byte maxRGBValue = 255)
        {
            var divider = value.Length >= 3 ? Mathf.CeilToInt(value.Length / 3f) : 0;

            var span = maxRGBValue - minRGBValue;

            var r = Mathf.Abs(value.Substring(0, divider).GetHashCode() % span) + minRGBValue;
            var g = Mathf.Abs(value.Substring(divider, divider).GetHashCode() % span) + minRGBValue;
            var b = Mathf.Abs(value.Substring(2 * divider).GetHashCode() % span) + minRGBValue;

            return new Color(r / 255f, g / 255f, b / 255f);

        }

        public static readonly Color DarkTextColor = NormalizeToColor(16, 16, 16);
        public static readonly Color LightTextColor = NormalizeToColor(201, 201, 201);
        public static Color GetTextColorFromBackground(Color background) => TextColorShouldBeDark(background) ? DarkTextColor : LightTextColor;

        public static bool TextColorShouldBeDark(Color backgroundColor)
        {
            var luminance = (0.299 * backgroundColor.r + 0.587 * backgroundColor.g + 0.114 * backgroundColor.b);

            return luminance > 0.5;
        }

        public static Color NormalizeToColor(byte r, byte g, byte b, byte a = 255) => new Color(r / 255f, g / 255f, b / 255f, a / 255f);

        public static Color HexColor(string hex)
        {
            if (hex[0] == '#') hex = hex.Substring(1);

            byte r = 0;
            byte g = 0;
            byte b = 0;
            byte a = 255;
            if (hex.Length >= 6)
            {
                r = (byte)int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                g = (byte)int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                b = (byte)int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                if (hex.Length == 8) a = (byte)int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
            }
            else if (hex.Length >= 3)
            {
                r = (byte)int.Parse(hex.Substring(0, 1) + hex.Substring(0, 1), NumberStyles.HexNumber);
                g = (byte)int.Parse(hex.Substring(1, 1) + hex.Substring(1, 1), NumberStyles.HexNumber);
                b = (byte)int.Parse(hex.Substring(2, 1) + hex.Substring(2, 1), NumberStyles.HexNumber);
                if (hex.Length == 4)
                    a = (byte)int.Parse(hex.Substring(3, 1) + hex.Substring(3, 1), NumberStyles.HexNumber);
            }

            return NormalizeToColor(r, g, b, a);
        }

        #endregion
    }
}