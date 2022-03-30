using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MolecularLib;
using UnityEditor;
using UnityEngine;

namespace MolecularEditor
{
    [CustomPropertyDrawer(typeof(Range<>), true)]
    public class RangeEditorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + 8f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var prevLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100f;

            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            position.x += 4;
            position.width -= 4;
            position.y += 4;
            position.height += 4;

            property.serializedObject.Update();
            var rangeObj = this.GetSerializedValue(property);
            var props = GetProperties(rangeObj);

            position = EditorGUI.PrefixLabel(position, EditorGUI.BeginProperty(position, label, property));
            EditorGUI.BeginChangeCheck();

            ShowMinMax();
            ValidateMinMax(rangeObj);

            EditorGUI.EndProperty();
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);

                property.serializedObject.ApplyModifiedProperties();
            }

            void ShowMinMax()
            {
                var fieldPos = new Rect(position.x, position.y, (position.width / 2f) - 5f, EditorGUIUtility.singleLineHeight + 2f);

                EditorGUIUtility.labelWidth = 30f;
                foreach (var prop in props)
                {
                    EditorHelper.AutoTypePropertyInfo(fieldPos, prop, rangeObj);

                    fieldPos.x += fieldPos.width + 5f;
                }
                EditorGUIUtility.labelWidth = prevLabelWidth;
            }
        }    
        
        private static List<PropertyInfo> GetProperties(object rangeObj)
        {            
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Default;
            var props = rangeObj.GetType().GetProperties(flags).Where(info => info.Name == "Min" || info.Name == "Max").ToList();
            return new List<PropertyInfo>(2) { props[0], props[1] };
        }

        private static void ValidateMinMax(object rangeObj)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Default;
            var validateMethod = rangeObj.GetType().GetMethod("ValidateMinMaxValues", flags);
            validateMethod?.Invoke(rangeObj, null);
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

            property.serializedObject.ApplyModifiedProperties();

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

        private const float labelWidth = 100f;
        private const float reducedControlSize = 90f;
        private const float padding = 10f;
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
            var foldoutPos = new Rect(position.x + 12f, position.y, labelWidth, position.height);
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
            position.x += labelWidth;
            position.width -= labelWidth + 4;
            
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
                
                var minValuePos = new Rect(position.x, position.y, reducedControlSize, position.height);
                range.Min = EditorGUI.IntField(minValuePos, "Min", range.Min);

                var minMaxSliderPos = new Rect(minValuePos.x + minValuePos.width + padding, position.y, position.width - (2 * reducedControlSize) - (2 * padding), position.height);

                var maxValuePos = new Rect(minMaxSliderPos.x + minMaxSliderPos.width + padding, position.y, reducedControlSize, position.height);
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