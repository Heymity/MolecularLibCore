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

using System.Linq;
using MolecularLib.Helpers;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(Tag))]
    public class TagEditorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();

            var tag = property.FindPropertyRelative("tag");

            var pos = EditorGUI.PrefixLabel(position, label);

            var index = InternalEditorUtility.tags.ToList().IndexOf(tag.stringValue);
            tag.stringValue = InternalEditorUtility.tags[EditorGUI.Popup(pos, index, InternalEditorUtility.tags)];

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}