using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace MolecularLib.AutoAssign
{
    public static class AutoAssignController
    {
        private struct AutoAssignData
        {
            public AutoAssignAt AssignAt;
            public MemberInfo MemberInfo;
            
        }
        
        private static Dictionary<string, List<AutoAssignData>> _autoAssignData = new Dictionary<string, List<AutoAssignData>>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        //[InitializeOnLoadMethod]
        private static void Start()
        {
            Debug.Log("AutoAssignController Start");
            foreach (var t in TypeLibrary.AllNonUnityAssembliesTypes)
            {
                if (!t.IsSubclassOf(typeof(MonoBehaviour))) continue;
                var useAutoAssignAtt = t.GetCustomAttribute<UseAutoAssignAttribute>();
                if (useAutoAssignAtt is null) continue;
                if (useAutoAssignAtt.DefaultAutoAssignMoment == AutoAssignAt.None) throw new NotSupportedException("Cannot have UseAutoAssignAttribute with DefaultAutoAssignMoment of None");

                var typeName = t.FullName;

                if (typeName is null) throw new Exception("Type name is null");
                
                var members = t.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);

                foreach (var member in members)
                {
                    var att = member.GetCustomAttribute<GetComponentAttribute>();
                    if (att is null) continue;
                    
                    var data = new AutoAssignData
                    {
                        AssignAt = att.OverrideAssignMoment == AutoAssignAt.None ? useAutoAssignAtt.DefaultAutoAssignMoment : att.OverrideAssignMoment,
                        MemberInfo = member
                    };
                    
                    Debug.Log($"AutoAssign INIT: {typeName} {member.Name}");
                    if (_autoAssignData.TryGetValue(typeName, out var datas))
                        datas.Add(data);
                    else
                        _autoAssignData.Add(typeName, new List<AutoAssignData> {data});

                    HandleAssignMembersAtAwake(t, member);
                }
            }
        }

        private static void HandleAssignMembersAtAwake(Type targetType, MemberInfo member)
        {
            
        }

        public static void AutoAssign(MonoBehaviour targetMonoBehaviour)
        {
            var targetType = targetMonoBehaviour.GetType();
            Debug.Log($"AutoAssign: {targetType.FullName}");
            if (targetType.FullName is null) throw new Exception("Type name is null");
            
            foreach (var assignData in _autoAssignData[targetType.FullName])
            {
                switch (assignData.MemberInfo.MemberType)
                {
                    case MemberTypes.Field:
                        var field = (FieldInfo) assignData.MemberInfo;
                        field.SetValue(targetMonoBehaviour, targetMonoBehaviour.GetComponent(field.FieldType));
                        break;
                    case MemberTypes.Property:
                        var property = (PropertyInfo) assignData.MemberInfo;
                        property.SetValue(targetMonoBehaviour, targetMonoBehaviour.GetComponent(property.PropertyType));
                        break;  
                    default:
                        throw new NotSupportedException($"Can only use AutoAssign in fields and properties, not on {assignData.MemberInfo.MemberType}");
                }
            }
        }
    }
}