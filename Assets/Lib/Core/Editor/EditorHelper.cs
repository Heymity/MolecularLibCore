using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using MolecularLib;
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

            fi.SetValue(targetObj, AutoTypeField(ref rect, field, label));
        }

        public static void AutoTypePropertyInfo(Rect rect, PropertyInfo pi, object targetObj, string label = null)
        {
            var field = pi.GetValue(targetObj);
            label ??= pi.Name;

            // some properties dont have the set method, to be refactored
            pi.SetValue(targetObj, AutoTypeField(ref rect, field, label));
        }

        public static object AutoTypeField(ref Rect rect, object value, string labelStr = null)
        {
            var label = GUIContent.none;
            if (!string.IsNullOrEmpty(labelStr)) label = new GUIContent(labelStr);
            
            switch (value)
            {
                case int i:
                    return EditorGUI.IntField(rect, label, i);
                case float f:
                    return EditorGUI.FloatField(rect, label, f);
                case double d:
                    return EditorGUI.DoubleField(rect, label, d);
                case bool b:
                    return EditorGUI.Toggle(rect, label, b);
                case string s:
                    return EditorGUI.TextField(rect, label, s);
                case Vector3 vector3:
                    return EditorGUI.Vector3Field(rect, label, vector3);
                case Vector3Int vector3Int:
                    return EditorGUI.Vector3IntField(rect, label, vector3Int);
                case Vector2 vector2:
                    return EditorGUI.Vector2Field(rect, label, vector2);
                case Vector2Int vector2Int:
                    return EditorGUI.Vector2IntField(rect, label, vector2Int);
                default:
                {
                    if (value.GetType().IsEnum)
                        return EditorGUI.EnumPopup(rect, label, (Enum)Enum.Parse(value.GetType(), value.ToString()));

                    if (value.GetType().IsSubclassOf(typeof(Object)))
                        return EditorGUI.ObjectField(rect, label, value as Object, value.GetType(), true);

                    if (value.GetType() == typeof(object))
                        return null;

                    EditorGUI.LabelField(rect, label, value.ToString());
                    break;
                }
            }

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
        
        public static Type TypeField<TBaseClass>(Rect rect, string label, Type currentValue)
        {
            var types = GetTypesForPopup<TBaseClass>();

            return DrawTypeField(rect, label, types, currentValue);
        }

        public static Type TypeField(Rect rect, string label, Type currentValue, Type baseType)
        {
            var types = GetTypesForPopup(baseType);

            var r = DrawTypeField(rect, label, types, currentValue);
            
            return r;
        }

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
        }
        
        public static Type DrawTypeField(Rect rect, string label, List<Type> types, Type current)
        {
            var selected = types.FindIndex(t => t == current);
            if (selected <= 0) selected = 0;

            if (CachedDisplayTypeNames is null || CachedDisplayTypeNames.Length == 0)
                CachedDisplayTypeNames = types.Select(t => t.FullName?.Replace('.', '/')).ToArray();

            selected = EditorGUI.Popup(
                rect,
                label,
                selected,
                CachedDisplayTypeNames);

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
                                                                BindingFlags.DeclaredOnly;
        private static string[] CachedDisplayTypeNames { get; set; }

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

        private static readonly Dictionary<Type, List<Type>> cachedDerivedTypes = new Dictionary<Type, List<Type>>();

        public static List<Type> GetTypesForPopup<TBaseClass>() => GetTypesForPopup(typeof(TBaseClass));
        public static List<Type> GetTypesForPopup(Type baseType)
        {
            if (cachedDerivedTypes.TryGetValue(baseType, out var derivedTypes))
                return derivedTypes;
            
            var types = AllTypes.Where(type => type.IsSubclassOf(baseType)).ToList();
            cachedDerivedTypes.Add(baseType, types);

            return types;
        }

        public static Texture2D Tex2DOfColorScreenSize(Color32 color)
        {
            var texture = new Texture2D(Screen.width, Screen.height);
            var pixels = Enumerable.Repeat(color, Screen.width * Screen.height).ToArray();
            texture.SetPixels32(pixels);
            texture.Apply();
            return texture;
        }

        public static Texture2D Tex2DOfColor(Color color)
        {
            var newTex = new Texture2D(1, 1);
            newTex.SetPixel(0, 0, color);
            newTex.Apply();
            return newTex;
        }
        
        public static T GetSerializedValue<T>(this PropertyDrawer propertyDrawer, SerializedProperty property)
        {
            var obj = propertyDrawer.fieldInfo.GetValue(property.serializedObject.targetObject);

            if (!obj.IsGenericList()) return (T)obj;
            int propertyIndex = int.Parse(property.propertyPath[property.propertyPath.Length - 2].ToString());

            return ((IList<T>)obj)[propertyIndex];
        }
        
        private static T GetTargetValue<T>(SerializedProperty property) where T : class
        {
            var propertyPath = property.propertyPath;
            var pathSegments = propertyPath.Split('.');

            if (pathSegments.Length == 0)
            {
                Debug.LogError("Field not found by editor");
                return null;
            }
            
            var targetObjType = property.serializedObject.targetObject.GetType();

            var currentSearchObjType = targetObjType;
            object currentSearchObj = property.serializedObject.targetObject;
            foreach (var pathSegment in pathSegments)
            {
                var targetField = currentSearchObjType.GetField(pathSegment, UnitySerializesBindingFlags);
                if (targetField is null)
                {
                    Debug.Log(pathSegment + " field not found by editor");
                    return null;
                }
                currentSearchObjType = targetField.FieldType;
                
                currentSearchObj = targetField.GetValue(currentSearchObj);
            }
        
            var dict = currentSearchObj as T;
            return dict;
        }

        /// <summary>
        /// Does not work for properties in arrays
        /// </summary>
        /// <param name="propertyDrawer">The property drawer.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static object GetSerializedValue(this PropertyDrawer propertyDrawer, SerializedProperty property)
        {
            var obj = propertyDrawer.fieldInfo.GetValue(property.serializedObject.targetObject);
            return obj;
        }

        public static bool IsGenericList(this object o)
        {
            var oType = o.GetType();
            return oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>));
        }

        public static Color GetColorFromString(string value, byte minRGBvalue = 0, byte maxRGBvalue = 255)
        {
            var divider = value.Length >= 3 ? Mathf.CeilToInt(value.Length / 3f) : 0;

            var span = maxRGBvalue - minRGBvalue;

            var r = Mathf.Abs(value.Substring(0, divider).GetHashCode() % span) + minRGBvalue;
            var g = Mathf.Abs(value.Substring(divider, divider).GetHashCode() % span) + minRGBvalue;
            var b = Mathf.Abs(value.Substring(2 * divider).GetHashCode() % span) + minRGBvalue;

            return new Color(r / 255f, g / 255f, b / 255f);

        }

        public static readonly Color DarkTextColor = NormalizeToColor(16, 16, 16);
        public static readonly Color LightTextColor = NormalizeToColor(201, 201, 201);
        public static Color GetTextColorFromBackground(Color background)
        {
            var luminance = (0.299 * background.r + 0.587 * background.g + 0.114 * background.b);

            return luminance > 0.5 ? DarkTextColor : LightTextColor;
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