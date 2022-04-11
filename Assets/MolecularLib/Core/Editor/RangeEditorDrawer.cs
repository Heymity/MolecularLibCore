using System;
using System.Linq;
using System.Reflection;
using MolecularLib.Helpers;
using UnityEditor;
using UnityEngine;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(Range<>), true)]
    public class RangeEditorDrawer : PropertyDrawer
    {
        internal const BindingFlags BindingFlags = System.Reflection.BindingFlags.Instance |
                                                  System.Reflection.BindingFlags.Public |
                                                  System.Reflection.BindingFlags.NonPublic |
                                                  System.Reflection.BindingFlags.DeclaredOnly;

        private IRange _range;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + 8f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _range = GetRange(property);
            
            // Draw background box
            var prevLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100f;

            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            position.x += 4;
            position.width -= 4;
            position.y += 4;
            position.height += 4;

            property.serializedObject.Update();

            position = EditorGUI.PrefixLabel(position, EditorGUI.BeginProperty(position, label, property));
            EditorGUI.BeginChangeCheck();

            ShowMinMax();
            _range.ValidateMinMaxValues();

            EditorGUI.EndProperty();
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);

                property.serializedObject.ApplyModifiedProperties();
            }

            void ShowMinMax()
            {
                var fieldPos = new Rect(position.x, position.y, (position.width / 2f) - 5f, EditorGUIUtility.singleLineHeight + 2f);

                var minProp = property.FindPropertyRelative("min");
                var maxProp = property.FindPropertyRelative("max");
                
                EditorGUIUtility.labelWidth = 30f;
                
                EditorGUI.PropertyField(fieldPos, minProp, new GUIContent("Min"));
                fieldPos.x += fieldPos.width + 5f;
                EditorGUI.PropertyField(fieldPos, maxProp, new GUIContent("Max"));
                
                EditorGUIUtility.labelWidth = prevLabelWidth;
            }
        }

        private static IRange GetRange(SerializedProperty property)
        {
            return EditorHelper.GetTargetValue<IRange>(property);
        }
    }

    [CustomPropertyDrawer(typeof(RangeVector3), true)]
    public class RangeVector3Drawer : PropertyDrawer
    {
        private RangeVector3 _range;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + 8f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _range = GetRange(property);
            
            // Draw background box
            var prevLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100f;

            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            position.x += 4;
            position.width -= 4;
            position.y += 4;
            position.height += 4;

            property.serializedObject.Update();

            position = EditorGUI.PrefixLabel(position, EditorGUI.BeginProperty(position, label, property));
            EditorGUI.BeginChangeCheck();

            ShowMinMax();
            _range.ValidateMinMaxValues();

            EditorGUI.EndProperty();
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);

                property.serializedObject.ApplyModifiedProperties();
            }

            void ShowMinMax()
            {
                var fieldPos = new Rect(position.x, position.y, (position.width / 2f) - 5f, EditorGUIUtility.singleLineHeight + 2f);

                var minVec3 = _range.Min;
                var maxVec3 = _range.Max;
                
                EditorGUIUtility.labelWidth = 30f;

                _range.Min = EditorGUI.Vector3Field(fieldPos, new GUIContent("Min"), minVec3);
                fieldPos.x += fieldPos.width + 5f;
                _range.Max = EditorGUI.Vector3Field(fieldPos, new GUIContent("Max"), maxVec3);
                
                EditorGUIUtility.labelWidth = prevLabelWidth;
            }
        }
        
        private static RangeVector3 GetRange(SerializedProperty property)
        {
            return EditorHelper.GetTargetValue<RangeVector3>(property);
        }
    }
    
     [CustomPropertyDrawer(typeof(RangeVector2), true)]
    public class RangeVector2Drawer : PropertyDrawer
    {
        private RangeVector2 _range;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + 8f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _range = GetRange(property);
            
            // Draw background box
            var prevLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100f;

            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            position.x += 4;
            position.width -= 4;
            position.y += 4;
            position.height += 4;

            property.serializedObject.Update();

            position = EditorGUI.PrefixLabel(position, EditorGUI.BeginProperty(position, label, property));
            EditorGUI.BeginChangeCheck();

            ShowMinMax();
            _range.ValidateMinMaxValues();

            EditorGUI.EndProperty();
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);

                property.serializedObject.ApplyModifiedProperties();
            }

            void ShowMinMax()
            {
                var fieldPos = new Rect(position.x, position.y, (position.width / 2f) - 5f, EditorGUIUtility.singleLineHeight + 2f);

                var minVec3 = _range.Min;
                var maxVec3 = _range.Max;
                
                EditorGUIUtility.labelWidth = 30f;

                _range.Min = EditorGUI.Vector2Field(fieldPos, new GUIContent("Min"), minVec3);
                fieldPos.x += fieldPos.width + 5f;
                _range.Max = EditorGUI.Vector2Field(fieldPos, new GUIContent("Max"), maxVec3);
                
                EditorGUIUtility.labelWidth = prevLabelWidth;
            }
        }
        
        private static RangeVector2 GetRange(SerializedProperty property)
        {
            return EditorHelper.GetTargetValue<RangeVector2>(property);
        }
    }

    [CustomPropertyDrawer(typeof(Range), true)]
    public class FloatRangeEditorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? 2 * (EditorGUIUtility.singleLineHeight + 8f) : EditorGUIUtility.singleLineHeight + 8f;
        }

        private const float LabelWidth = 100f;
        private const float ReducedControlSize = 90f;
        private const float Padding = 10f;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var prevLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100f;

            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            position.x += 4;
            position.width -= 4;
            position.y += 4;
            position.height = EditorGUIUtility.singleLineHeight + 2f;

            var orgX = position.x;
            var orgWidth = position.width;

            EditorGUI.BeginChangeCheck();
            property.serializedObject.Update();

            var range = this.GetSerializedValue<Range>(property);
            
            var attrs = fieldInfo.GetCustomAttributes<MinMaxRangeAttribute>().ToList();
            var foldoutPos = new Rect(position.x + 12f, position.y, LabelWidth, position.height);
            var newLabel = EditorGUI.BeginProperty(position, label, property);
            if (!attrs.Any())
                property.isExpanded = EditorGUI.Foldout(foldoutPos, property.isExpanded, label);
            else
            {
                EditorGUI.PrefixLabel(position, newLabel);
                range.MaxValuePossible = attrs.FirstOrDefault()?.Max ?? 1;
                range.MinValuePossible = attrs.FirstOrDefault()?.Min ?? -1;
                property.isExpanded = false;
            }
            position.x += LabelWidth;
            position.width -= LabelWidth + 4;
            
            if (property.isExpanded)
            {
                ShowReduced();
                position.y += position.height + 4f;
                position.x = orgX;
                position.width = orgWidth;
                ShowMinMax();
            }
            else
                ShowReduced();

            EditorGUI.EndProperty();
            EditorGUIUtility.labelWidth = prevLabelWidth;

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);
                range.ValidateMinMaxValues(); 
                property.serializedObject.ApplyModifiedProperties();
            }
            
            void ShowMinMax()
            {
                EditorGUIUtility.labelWidth = 80f;
                
                var fieldPos = new Rect(position.x, position.y, (position.width / 2f) - 10f, EditorGUIUtility.singleLineHeight + 2f);

                range.MinValuePossible = EditorGUI.FloatField(fieldPos, "Min Limit", range.MinValuePossible);

                fieldPos.x += fieldPos.width + 5f;
                range.MaxValuePossible = EditorGUI.FloatField(fieldPos, "Max Limit", range.MaxValuePossible);

                EditorGUIUtility.labelWidth = prevLabelWidth;
            }
            
            void ShowReduced()
            {
                EditorGUIUtility.labelWidth = 30f;
                
                var minValuePos = new Rect(position.x, position.y, ReducedControlSize, position.height);
                range.Min = EditorGUI.FloatField(minValuePos, "Min", range.Min);

                var minMaxSliderPos = new Rect(minValuePos.x + minValuePos.width + Padding, position.y, position.width - (2 * ReducedControlSize) - (2 * Padding), position.height);

                var maxValuePos = new Rect(minMaxSliderPos.x + minMaxSliderPos.width + Padding, position.y, ReducedControlSize, position.height);
                range.Max = EditorGUI.FloatField(maxValuePos, "Max", range.Max);

                var min = range.Min;
                var max = range.Max;

                if (max > range.MaxValuePossible) range.MaxValuePossible = range.Max;
                if (min < range.MinValuePossible) range.MinValuePossible = range.Min;

                EditorGUI.MinMaxSlider(minMaxSliderPos, ref min, ref max, range.MinValuePossible, range.MaxValuePossible);

                if (Math.Abs(range.Min - min) > 0.00000000000000001f)
                    range.Min = min;
                if (Math.Abs(range.Max - max) > 0.00000000000000001f)
                    range.Max = max;
            }
        }
    }

    [CustomPropertyDrawer(typeof(RangeInteger), true)]
    public class IntRangeEditorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? 2 * (EditorGUIUtility.singleLineHeight + 8f) : EditorGUIUtility.singleLineHeight + 8f;
        }

        private const float LabelWidth = 100f;
        private const float ReducedControlSize = 90f;
        private const float Padding = 10f;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var prevLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100f;

            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            position.x += 4;
            position.width -= 4;
            position.y += 4;
            position.height = EditorGUIUtility.singleLineHeight + 2f;

            var orgX = position.x;
            var orgWidth = position.width;
            
            EditorGUI.BeginChangeCheck();
            property.serializedObject.Update();

            var range = this.GetSerializedValue<RangeInteger>(property);
            
            var attrs = fieldInfo.GetCustomAttributes<MinMaxRangeAttribute>().ToList();
            var foldoutPos = new Rect(position.x + 12f, position.y, LabelWidth, position.height);
            var newLabel = EditorGUI.BeginProperty(position, label, property);
            if (!attrs.Any())
                property.isExpanded = EditorGUI.Foldout(foldoutPos, property.isExpanded, label);
            else
            {
                EditorGUI.PrefixLabel(position, newLabel);
                range.MaxValuePossible = Mathf.RoundToInt(attrs.FirstOrDefault()?.Max ?? 1);
                range.MinValuePossible = Mathf.RoundToInt(attrs.FirstOrDefault()?.Min ?? -1);
                property.isExpanded = false;
            }
            position.x += LabelWidth;
            position.width -= LabelWidth + 4;
            
            if (property.isExpanded)
            {
                ShowReduced();
                position.y += position.height + 4f;
                position.x = orgX;
                position.width = orgWidth;
                ShowMinMax();
            }
            else
                ShowReduced();

            EditorGUI.EndProperty();
            EditorGUIUtility.labelWidth = prevLabelWidth;

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);
                range.ValidateMinMaxValues();
                property.serializedObject.ApplyModifiedProperties();
            }

            property.serializedObject.ApplyModifiedProperties();

            void ShowMinMax()
            {
                EditorGUIUtility.labelWidth = 80f;

                var fieldPos = new Rect(position.x, position.y, (position.width / 2f) - 10f, EditorGUIUtility.singleLineHeight + 2f);

                range.MinValuePossible = EditorGUI.IntField(fieldPos, "Min Limit", range.MinValuePossible);

                fieldPos.x += fieldPos.width + 5f;
                range.MaxValuePossible = EditorGUI.IntField(fieldPos, "Max Limit", range.MaxValuePossible);
                
                EditorGUIUtility.labelWidth = prevLabelWidth;
            }

            void ShowReduced()
            {
                EditorGUIUtility.labelWidth = 30f;
                
                var minValuePos = new Rect(position.x, position.y, ReducedControlSize, position.height);
                range.Min = EditorGUI.IntField(minValuePos, "Min", range.Min);

                var minMaxSliderPos = new Rect(minValuePos.x + minValuePos.width + Padding, position.y, position.width - (2 * ReducedControlSize) - (2 * Padding), position.height);

                var maxValuePos = new Rect(minMaxSliderPos.x + minMaxSliderPos.width + Padding, position.y, ReducedControlSize, position.height);
                range.Max = EditorGUI.IntField(maxValuePos, "Max", range.Max);

                float min = range.Min;
                float max = range.Max;

                if (max > range.MaxValuePossible) range.MaxValuePossible = range.Max;
                if (min < range.MinValuePossible) range.MinValuePossible = range.Min;

                EditorGUI.MinMaxSlider(minMaxSliderPos, ref min, ref max, range.MinValuePossible, range.MaxValuePossible);

                if (Math.Abs(range.Min - min) > 0.00000000000000001f) 
                    range.Min = Mathf.RoundToInt(min);
                if (Math.Abs(range.Max - max) > 0.00000000000000001f) 
                    range.Max = Mathf.RoundToInt(max);
            }
        }
    }
}