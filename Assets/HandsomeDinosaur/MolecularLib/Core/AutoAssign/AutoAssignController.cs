using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MolecularLib.AutoAssign
{
    public static class AutoAssignController
    {
        private enum Mode
        {
            None,
            GetComponent,
            GetComponentInChild,
            Find,
            FindWithTag,
            FindGameObjectsWithTag,
            FindObjectOfType,
            FindObjectsOfType,
        }
        
        private struct AutoAssignData
        {
            //public AutoAssignAt AssignAt;
            public Mode AssignMode;
            public MemberInfo MemberInfo;
            public Type ProvidedType;
            public string NameOrTag;
        }
        
        private static readonly Dictionary<string, List<AutoAssignData>> autoAssignData = new Dictionary<string, List<AutoAssignData>>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Start()
        {
            foreach (var t in TypeLibrary.AllNonUnityAssembliesTypes)
            {
                if (!t.IsSubclassOf(typeof(MonoBehaviour))) continue;
                var useAutoAssignAtt = t.GetCustomAttribute<UseAutoAssignAttribute>();
                if (useAutoAssignAtt is null) continue;
                //if (useAutoAssignAtt.DefaultAutoAssignMoment == AutoAssignAt.None) throw new NotSupportedException("Cannot have UseAutoAssignAttribute with DefaultAutoAssignMoment of None");

                var typeName = t.FullName;

                if (typeName is null) throw new Exception("Type name is null");
                
                var members = t.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.SetField);

                foreach (var member in members)
                {
                    Mode mode;
                    var nameOrTag = "";
                    Type providedType = null;
                    
                    var getCompAtt = member.GetCustomAttribute<GetComponentAttribute>();
                    var getCompInChildAtt = member.GetCustomAttribute<GetComponentInChildrenAttribute>();
                    var findAtt = member.GetCustomAttribute<FindAttribute>();
                    var findTagAtt = member.GetCustomAttribute<FindWithTagAttribute>();
                    var findAllTagAtt = member.GetCustomAttribute<FindGameObjectsWithTag>();
                    var findTypeAtt = member.GetCustomAttribute<FindObjectOfTypeAttribute>();
                    var findAllTypeAtt = member.GetCustomAttribute<FindObjectsOfTypeAttribute>();
                    
                    if (getCompAtt != null) {mode = Mode.GetComponent;}
                    else if (getCompInChildAtt != null) mode = Mode.GetComponentInChild;
                    else if (findAtt != null)
                    {
                        mode = Mode.Find;
                        nameOrTag = findAtt.Name;
                    }
                    else if (findTagAtt != null)
                    {
                        mode = Mode.FindWithTag;
                        nameOrTag = findTagAtt.Tag;
                    }
                    else if (findAllTagAtt != null)
                    {
                        mode = Mode.FindGameObjectsWithTag;
                        nameOrTag = findAllTagAtt.Tag;
                    }
                    else if (findTypeAtt != null)
                    {
                        mode = Mode.FindObjectOfType;
                        providedType = findTypeAtt.Type;
                    }
                    else if (findAllTypeAtt != null)
                    {
                        mode = Mode.FindObjectsOfType;
                        providedType = findAllTypeAtt.Type;
                    }
                    else continue;
                    
                    var data = new AutoAssignData
                    {
                        //AssignAt = att.OverrideAssignMoment == AutoAssignAt.None ? useAutoAssignAtt.DefaultAutoAssignMoment : att.OverrideAssignMoment,
                        MemberInfo = member,
                        AssignMode = mode,
                        ProvidedType = providedType,
                        NameOrTag = nameOrTag,
                    };
                    
                    if (autoAssignData.TryGetValue(typeName, out var datas))
                        datas.Add(data);
                    else
                        autoAssignData.Add(typeName, new List<AutoAssignData> {data});
                }
            }
        }

        public static void AutoAssign(this MonoBehaviour targetMonoBehaviour)
        {
            var targetType = targetMonoBehaviour.GetType();
            
            if (targetType.FullName is null) throw new Exception("Type name is null");
            
            foreach (var assignData in autoAssignData[targetType.FullName])
            {
                switch (assignData.MemberInfo.MemberType)
                {
                    case MemberTypes.Field:
                        var field = (FieldInfo) assignData.MemberInfo;
                        field.SetValue(targetMonoBehaviour, GetValue(targetMonoBehaviour, assignData, field.FieldType));
                        break;
                    case MemberTypes.Property:
                        var property = (PropertyInfo) assignData.MemberInfo;
                        property.SetValue(targetMonoBehaviour, GetValue(targetMonoBehaviour, assignData, property.PropertyType));
                        break;
                    case MemberTypes.All:
                    case MemberTypes.Constructor:
                    case MemberTypes.Custom:
                    case MemberTypes.Event:
                    case MemberTypes.Method:
                    case MemberTypes.NestedType:
                    case MemberTypes.TypeInfo:
                    default:
                        throw new NotSupportedException($"Can only use AutoAssign in fields and properties, not on {assignData.MemberInfo.MemberType}");
                }
            }
        }
        
        private static object GetValue(Component targetMonoBehaviour, AutoAssignData data, Type memberType)
        {
            return data.AssignMode switch
            {
                Mode.GetComponent => targetMonoBehaviour.GetComponent(memberType),
                Mode.GetComponentInChild => targetMonoBehaviour.GetComponentInChildren(memberType),
                Mode.Find => GameObject.Find(data.NameOrTag),
                Mode.FindWithTag => GameObject.FindWithTag(data.NameOrTag),
                Mode.FindGameObjectsWithTag => GameObject.FindGameObjectsWithTag(data.NameOrTag),
                Mode.FindObjectOfType => Object.FindObjectOfType(data.ProvidedType),
                Mode.FindObjectsOfType => Object.FindObjectsOfType(data.ProvidedType),
                Mode.None => throw new NotSupportedException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}