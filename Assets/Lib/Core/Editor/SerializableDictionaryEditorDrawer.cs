using System.Collections.Generic;
using Molecular;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class SerializableDictionaryEditorDrawer : PropertyDrawer
    {
        private const float Padding = 5f;
        private const float HeaderHeight = 25f;
        
        private static int _selectedIndex = -1;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (property.FindPropertyRelative("keys").arraySize * 22) + HeaderHeight + 20;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var keysProp = property.FindPropertyRelative("keys");
            var valuesProp = property.FindPropertyRelative("values");

            DrawDictionary(position, keysProp, valuesProp);
        }

        private void DrawDictionary(Rect position, SerializedProperty keysProp, SerializedProperty valuesProp)
        {
            var labelStyle = (GUIStyle)"RL Header";
            labelStyle.fixedHeight = 0;
            
            var rectBox = new Rect(position.x, position.y, position.width, HeaderHeight);
            GUI.Box(rectBox, GUIContent.none, labelStyle);

            var headerTextRect = rectBox;
            headerTextRect.x += 5;
            EditorGUI.LabelField(headerTextRect, "Dictionary");
            
            var bgStyle = (GUIStyle)"RL Background";
  
            rectBox.y += rectBox.height;
            rectBox.height = position.height - rectBox.height - 25;
            GUI.Box(rectBox, GUIContent.none, bgStyle);
            
            
            for (var i = 0; i < keysProp.arraySize; i++)
            {
                var keyProp = keysProp.GetArrayElementAtIndex(i);
                var valueProp = valuesProp.GetArrayElementAtIndex(i);
                //EditorGUI.GetPropertyHeight(keyProp);
                var elementRect = new Rect(rectBox.x + Padding, rectBox.y + (i * 20) + 2, rectBox.width - (2 * Padding), 22);
                DrawDictionaryElement(elementRect, keyProp, valueProp);
            }
            
            DrawFooter(rectBox, keysProp, valuesProp);
        }

        private void DrawFooter(Rect position, SerializedProperty keysProp, SerializedProperty valuesProp)
        {
            const int footerSize = 60;
            var footerStyle = (GUIStyle) "RL Footer";
            
            var footerRect = new Rect(position.width - footerSize - 25, position.y + position.height, footerSize, footerStyle.fixedHeight);
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

        private void DrawDictionaryElement(Rect position, SerializedProperty keyProp, SerializedProperty valueProp)
        {
            var keyRect = new Rect(position.x, position.y, position.width * 0.5f - Padding, 18);
            var valueRect = new Rect(position.x + position.width * 0.5f + Padding, position.y, position.width * 0.5f - Padding, 18);

            EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none);
            EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);
        }
    }
}