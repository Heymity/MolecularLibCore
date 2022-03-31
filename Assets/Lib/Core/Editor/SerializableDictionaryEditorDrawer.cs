using Molecular;
using UnityEditor;
using UnityEngine;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class SerializableDictionaryEditorDrawer : PropertyDrawer
    {
        private const float Padding = 5f;
        private const float HeaderHeight = 25f;
        private const float FooterHeight = 20f;
        private const float NoElementHeight = 25f;
        private const float ElementHeight = 22f;
        
        private int _selectedIndex = -1;
        private bool _dontCheckForNewSelection;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var arraySize = property.FindPropertyRelative("keys").arraySize;
            var height = HeaderHeight + arraySize * (EditorGUIUtility.singleLineHeight + Padding) + FooterHeight;
            if (arraySize == 0) height += NoElementHeight;
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            _dontCheckForNewSelection = false;
            
            var keysProp = property.FindPropertyRelative("keys");
            var valuesProp = property.FindPropertyRelative("values");

            DrawDictionary(position, keysProp, valuesProp, label);

            EditorGUI.EndChangeCheck();
        }

        private void DrawDictionary(Rect position, SerializedProperty keysProp, SerializedProperty valuesProp, GUIContent label)
        {
            // Draw header
            var headerStyle = (GUIStyle)"RL Header";
            headerStyle.fixedHeight = 0;

            var rectBox = new Rect(position.x, position.y, position.width, HeaderHeight);
            GUI.Box(rectBox, GUIContent.none, headerStyle);

            var headerTextRect = rectBox;
            headerTextRect.x += 5;
            EditorGUI.LabelField(headerTextRect, label);
            
            // Draw background
            var bgStyle = (GUIStyle)"RL Background";
  
            rectBox.y += rectBox.height;
            rectBox.height = position.height - rectBox.height - FooterHeight;
            if (keysProp.arraySize == 0) rectBox.height = NoElementHeight;
            
            GUI.Box(rectBox, GUIContent.none, bgStyle);
            if (keysProp.arraySize == 0)
            {
                var noElementRect = rectBox;
                noElementRect.x += Padding;
                EditorGUI.LabelField(noElementRect, "No elements");
            }
            
            // Draw elements
            const float handleWidth = 20f;
            for (var i = 0; i < keysProp.arraySize; i++)
            {
                var keyProp = keysProp.GetArrayElementAtIndex(i);
                var valueProp = valuesProp.GetArrayElementAtIndex(i);
                //EditorGUI.GetPropertyHeight(keyProp);
                var elementAreaRect = new Rect(rectBox.x + 1, rectBox.y + (i * ElementHeight), rectBox.width - 2, ElementHeight);
                var elementRect = new Rect(
                    elementAreaRect.x + Padding + handleWidth, 
                    elementAreaRect.y, 
                    elementAreaRect.width - (2 * Padding) - handleWidth, 
                    elementAreaRect.height);

                HandleSelection(elementAreaRect, i, keyProp);
                if (_selectedIndex == i && Event.current.type == EventType.Repaint) ((GUIStyle)"selectionRect").Draw(elementAreaRect, false, false, false, false);
                
                DrawDictionaryElement(elementRect, keyProp, valueProp);
            }
            
            DrawFooter(rectBox, keysProp, valuesProp);
        }

        private void DrawDictionaryElement(Rect position, SerializedProperty keyProp, SerializedProperty valueProp)
        {
            EditorGUI.BeginChangeCheck();

            var keyRect = new Rect(position.x, position.y + 2, position.width * 0.5f - Padding, position.height - 4);
            var valueRect = new Rect(position.x + position.width * 0.5f + Padding,
                position.y + (position.height - 18) / 2, position.width * 0.5f - Padding, 18);

            EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none);
            EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);

            if (!EditorGUI.EndChangeCheck()) return;
            
            Undo.RecordObject(keyProp.serializedObject.targetObject, "Dictionary Changed");
            EditorUtility.SetDirty(keyProp.serializedObject.targetObject);
        }

        private void DrawFooter(Rect position, SerializedProperty keysProp, SerializedProperty valuesProp)
        {
            const int footerSize = 60;
            var footerStyle = (GUIStyle) "RL Footer";
            
            var footerRect = new Rect(position.width - footerSize, position.y + position.height, footerSize, FooterHeight);
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
            
            _selectedIndex = currentIndex; //<--- This (setting it to some value) is causing the bug
            _dontCheckForNewSelection = true;
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }
}