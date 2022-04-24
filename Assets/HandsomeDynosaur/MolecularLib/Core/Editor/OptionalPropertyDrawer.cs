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

using MolecularLib.Helpers;
using UnityEditor;
using UnityEngine;

namespace MolecularLib
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        private const float ToggleSize = 18f;

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"));
            var useValueHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("useValue"));

            return Mathf.Max(valueHeight, useValueHeight);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueLabel = label.text;
            
            EditorGUI.BeginProperty(position, label, property);
            property.serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            var valueProperty = property.FindPropertyRelative("value");
            var useValueProperty = property.FindPropertyRelative("useValue");
            
            EditorGUI.BeginDisabledGroup(!useValueProperty.boolValue);
            var valueRect = new Rect(position.x, position.y, position.width - ToggleSize - 3f, 0f)
            {
                height = EditorGUI.GetPropertyHeight(valueProperty)
            };

            EditorGUI.PropertyField(valueRect, valueProperty, new GUIContent(valueLabel));
            EditorGUI.EndDisabledGroup();

            var useValueRect = new Rect(valueRect.xMax + 3f, position.y, ToggleSize, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(useValueRect, useValueProperty, GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
            
            EditorGUI.EndProperty();
        }
    }
}