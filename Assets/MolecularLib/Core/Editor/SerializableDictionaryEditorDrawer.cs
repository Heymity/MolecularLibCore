using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using MolecularLib;
using UnityEditor;
using UnityEngine;
using Logger = MolecularInternal.Logger;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
    public class SerializableDictionaryEditorDrawer : PropertyDrawer
    {
        private const float Padding = 6f;
        private const float HeaderHeight = 25f;
        private const float FooterHeight = 20f;
        private const float NoElementHeight = 25f;
        private const float ElementHeight = 22f;
        
        private int _selectedIndex = -1;
        private float _cumulativeHeight;
        private bool _dontCheckForNewSelection;
        
        private List<object> _usedKeys;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return HeaderHeight;
            
            var arraySize = property.FindPropertyRelative("keys").arraySize;
            var height = HeaderHeight + _cumulativeHeight + FooterHeight;
            if (arraySize == 0) height += NoElementHeight;
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();
            
            property.serializedObject.Update();
            
            _dontCheckForNewSelection = false;
        
            var keysProp = property.FindPropertyRelative("keys");
            var valuesProp = property.FindPropertyRelative("values");

            if (keysProp is null || valuesProp is null)
            {
                EditorGUI.LabelField(position, $"{label.text} Error: Key or DeserializedValue type isn't serializable; Use [HideInInspector] to hide this field");
                EditorGUI.EndProperty();
                return;
            }

            var boxRect = DrawBox(position, label, keysProp.arraySize > 0, property);
            
            if (property.isExpanded)
                DrawDictionary(boxRect, keysProp, valuesProp, label);
            
            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
            
            EditorGUI.EndProperty();
        }

        private static Rect DrawBox(Rect position, GUIContent label, bool hasElements, SerializedProperty property)
        {
            // Draw header
            var headerStyle = (GUIStyle)"RL Header";
            headerStyle.fixedHeight = 0;

            var rectBox = new Rect(position.x, position.y, position.width, HeaderHeight);
            GUI.Box(rectBox, GUIContent.none, headerStyle);

            var headerTextRect = rectBox;
            headerTextRect.x += 18;
            headerTextRect.width -= 18;
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(headerTextRect, property.isExpanded, label, EditorStyles.foldout);
            EditorGUI.EndFoldoutHeaderGroup();
            
            if (!property.isExpanded) return rectBox;
            
            // Draw background
            var bgStyle = (GUIStyle)"RL Background";
  
            rectBox.y += rectBox.height;
            rectBox.height = position.height - rectBox.height - FooterHeight;
            if (!hasElements) rectBox.height = NoElementHeight;
            
            GUI.Box(rectBox, GUIContent.none, bgStyle);
            if (!hasElements)
            {
                var noElementRect = rectBox;
                noElementRect.x += Padding;
                EditorGUI.LabelField(noElementRect, "No elements");
            }

            return rectBox;
        }
        
        private void DrawDictionary(Rect boxRect, SerializedProperty keysProp, SerializedProperty valuesProp, GUIContent label)
        {
            // Draw elements
            _usedKeys ??= new List<object>();
            _usedKeys.Clear();
            
            _cumulativeHeight = 0f;
            const float handleWidth = 0; //= 10f;
            for (var i = 0; i < keysProp.arraySize; i++)
            {
                var keyProp = keysProp.GetArrayElementAtIndex(i);
                var valueProp = valuesProp.GetArrayElementAtIndex(i);
                
                var keyHeight = EditorGUI.GetPropertyHeight(keyProp);
                var valueHeight = EditorGUI.GetPropertyHeight(valueProp);
                var maxHeight = Mathf.Max(keyHeight, valueHeight);

                var elementHeight = maxHeight <= ElementHeight ? ElementHeight : maxHeight;
                var elementAreaRect = new Rect(boxRect.x + 1, boxRect.y + _cumulativeHeight, boxRect.width - 2, elementHeight);
                var elementRect = new Rect(
                    elementAreaRect.x + 2 * Padding + handleWidth, 
                    elementAreaRect.y, 
                    elementAreaRect.width - (3 * Padding) - handleWidth, 
                    elementAreaRect.height);

                _cumulativeHeight += elementHeight;
                
                HandleSelection(elementAreaRect, i, keyProp);
                
                var isDuplicate = false;

                var keyObj = GetKeyValue(keyProp);
                if (_usedKeys.Contains(keyObj)) isDuplicate = true;
                else _usedKeys.Add(keyObj);
                
                var style = _selectedIndex == i ? "selectionRect" : i % 2 == 0 ? "CN EntryBackEven" : "CN EntryBackOdd"; 
                if (Event.current.type == EventType.Repaint) ((GUIStyle)style).Draw(elementAreaRect, false, false, false, false);

                //var dragHandleStyle = (GUIStyle)"RL DragHandle";
                //var dragHandleRect = new Rect(elementAreaRect.x + Padding, elementAreaRect.y + elementAreaRect.height / 2 - dragHandleStyle.fixedHeight / 2.5f, handleWidth, elementAreaRect.height);
                //if (Event.current.type == EventType.Repaint) dragHandleStyle.Draw(dragHandleRect, false, false, false, false);
                
                DrawDictionaryElement(elementRect, keyProp, valueProp, isDuplicate, i);
            }

            _cumulativeHeight += 5f;
            
            DrawFooter(boxRect, keysProp, valuesProp);
        }

        private static void DrawDictionaryElement(Rect position, SerializedProperty keyProp, SerializedProperty valueProp, bool duplicatedKey, int index)
        {
            EditorGUI.BeginChangeCheck();

            var keyRect = new Rect(position.x, position.y + 2, position.width * 0.5f - Padding, ElementHeight - 4);
            var valueRect = new Rect(position.x + position.width * 0.5f + Padding,
                position.y + 2, position.width * 0.5f - Padding, ElementHeight - 4);
            
            if (duplicatedKey)
            {
                var duplicatedKeyRect = keyRect;
                duplicatedKeyRect.width = 18;
                
                keyRect.x += 18;
                keyRect.width -= 18;
                EditorGUI.LabelField(duplicatedKeyRect, EditorGUIUtility.TrIconContent("d_Invalid@2x", "Duplicated key"));
            }
            else EditorGUI.LabelField(Rect.zero, "");

            var originalLabelWidth = EditorGUIUtility.labelWidth;
            
            EditorGUIUtility.labelWidth = 50;
            if (keyProp.isExpanded) EditorGUIUtility.labelWidth = originalLabelWidth / 2;
            EditorGUI.PropertyField(keyRect, keyProp, new GUIContent($"Key {index}"), true);
            
            if (!valueProp.isExpanded) EditorGUIUtility.labelWidth = 55;
            else EditorGUIUtility.labelWidth = originalLabelWidth / 2;
            EditorGUI.PropertyField(valueRect, valueProp, new GUIContent($"Value {index}"), true);
            
            EditorGUIUtility.labelWidth = originalLabelWidth;
            
            if (!EditorGUI.EndChangeCheck()) return;
            
            Undo.RecordObject(keyProp.serializedObject.targetObject, "Dictionary Changed");
            EditorUtility.SetDirty(keyProp.serializedObject.targetObject);
        }

        private void DrawFooter(Rect position, SerializedProperty keysProp, SerializedProperty valuesProp)
        {
            const int footerSize = 60;
            var footerStyle = (GUIStyle) "RL Footer";
            
            var footerRect = new Rect(position.width - footerSize + Padding, position.y + position.height, footerSize, FooterHeight);
            GUI.Box(footerRect, GUIContent.none, footerStyle);

            var btnsRect = new Rect(footerRect.x + 5, footerRect.y, footerRect.width - 10, footerRect.height);
            var addBtnRect = new Rect(btnsRect.x, btnsRect.y, btnsRect.width / 2, btnsRect.height);
            var removeBtnRect = new Rect(btnsRect.x + btnsRect.width / 2, btnsRect.y, btnsRect.width / 2, btnsRect.height);
            if (GUI.Button(addBtnRect, EditorGUIUtility.TrIconContent("Toolbar Plus", "Add to the list"), "RL FooterButton"))
            {
                keysProp.arraySize++;
                valuesProp.arraySize++;
            }
            if (GUI.Button(removeBtnRect, EditorGUIUtility.TrIconContent("Toolbar Minus", "Remove selection from the list"), "RL FooterButton") && _selectedIndex >= 0)
            {
                keysProp.DeleteArrayElementAtIndex(_selectedIndex);
                valuesProp.DeleteArrayElementAtIndex(_selectedIndex);
                _selectedIndex = -1;
            }
        }
        
        private void HandleSelection(Rect area, int currentIndex, SerializedProperty property)
        {
            if (_dontCheckForNewSelection || Event.current.type != EventType.MouseDown) return;
            if (!area.Contains(Event.current.mousePosition))
                return;
            
            _selectedIndex = currentIndex;
            _dontCheckForNewSelection = true;
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
        
        private static object GetKeyValue(SerializedProperty property)
        {
            var propertyPath = property.propertyPath;
            var pathSegments = propertyPath.Split('.');

            if (pathSegments.Length == 0)
            {
                Debug.LogError("Dictionary field not found by editor");
                return null;
            }
            
            var targetObjType = property.serializedObject.targetObject.GetType();

            var currentSearchObjType = targetObjType;
            object currentSearchObj = property.serializedObject.targetObject;
            for (var index = 0; index < pathSegments.Length; index++)
            {
                FieldInfo targetField = null;
                var pathSegment = pathSegments[index];
                while (targetField is null && currentSearchObjType.BaseType != null)
                {
                    if (pathSegment == "Array" && index == pathSegments.Length - 2)
                    {
                        var dataSegment = pathSegments[index + 1];
                        var arrayIndex = int.Parse(dataSegment.Split('[', ']')[1]);

                        return (currentSearchObj as IList)?.Cast<object>().ElementAt(arrayIndex);
                    }

                    targetField = currentSearchObjType.GetField(pathSegment, EditorHelper.UnitySerializesBindingFlags);

                    if (targetField is null && currentSearchObjType.BaseType != null)
                        currentSearchObjType = currentSearchObjType.BaseType;
                }

                if (targetField is null)
                {
                    Debug.LogWarning(pathSegment + " field not found by editor");
                    Logger.MolecularVerbose($"[DEBUG] CurrentSearchObject: {currentSearchObj} | pathSegment: {pathSegment} | propertyPath: {propertyPath}");
                    return null;
                }

                currentSearchObjType = targetField.FieldType;

                currentSearchObj = targetField.GetValue(currentSearchObj);
            }

            return currentSearchObj;
        }
    }
}