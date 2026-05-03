using System;
using System.Linq;
using System.Reflection;
using Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    public class GetComponentMenuItem
    {
        [MenuItem("Call/Get Components")]
        public static void GetComponents()
        {
            MonoBehaviour[] monoBehaviours = Object.FindObjectsByType<MonoBehaviour>();

            foreach (MonoBehaviour monoBehaviour in monoBehaviours)
            {
                monoBehaviour.GetType().GetFields(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(field => field.GetCustomAttribute<GetComponentAttribute>() != null)
                    .ToList()
                    .ForEach(GetComponent(monoBehaviour));
            }
        }

        private static Action<FieldInfo> GetComponent(MonoBehaviour monoBehaviour)
        {
            return field =>
            {
                if (field.GetValue(monoBehaviour) == null)
                {
                    field.SetValue(monoBehaviour, monoBehaviour.GetComponent(field.FieldType));
                    EditorUtility.SetDirty(monoBehaviour);
                }
            };
        }
    }
}