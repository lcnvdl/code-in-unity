using System.Collections.Generic;
using System.Reflection;
using CodeInUnity.Core.Attributes;
using UnityEngine;

namespace CodeInUnity.Scripts.GameObjects
{
    public class LinkedAttributeData
    {
        public LinkedAttributeData()
        {
        }

        public LinkedAttributeData(MonoBehaviour instance, FieldInfo fieldInfo)
        {
            this.instance = instance;
            this.fieldInfo = fieldInfo;
        }

        public FieldInfo fieldInfo;

        public MonoBehaviour instance;
    }

    public class LinkedAttributesScript : MonoBehaviour
    {
        public List<string> componentsFilter = new List<string>();

        public bool recursive = false;

        public bool ignoreComponentsFilter = false;

        public bool debugLog = true;

        private Dictionary<string, LinkedAttributeData> inputs = new Dictionary<string, LinkedAttributeData>();

        private Dictionary<string, List<LinkedAttributeData>> outputs = new Dictionary<string, List<LinkedAttributeData>>();

        private void OnEnable()
        {
            this.inputs.Clear();
            this.outputs.Clear();
            this.LoadInputsAndOutputs(transform);
        }

        private void OnDisable()
        {
        }

        private void LoadInputsAndOutputs(Transform container)
        {
            var components = recursive ? container.GetComponentsInChildren<MonoBehaviour>() : container.GetComponents<MonoBehaviour>();

            foreach (var component in components)
            {
                if (this.ignoreComponentsFilter || this.componentsFilter.Contains(component.GetType().Name))
                {
                    this.LinkComponentFields(component);
                }
            }
        }

        public void Sync()
        {
            foreach (var kv in inputs)
            {
                if (outputs.ContainsKey(kv.Key))
                {
                    var value = kv.Value.fieldInfo.GetValue(kv.Value.instance);

                    foreach (var outputVar in outputs[kv.Key])
                    {
                        outputVar.fieldInfo.SetValue(outputVar.instance, value);

                        EditorDebug.LogIf(this.debugLog, $"Value '{value}' set from input {kv.Value.instance.name}.{kv.Key} to {outputVar.instance.name}.{kv.Key}.");
                    }
                }
            }
        }

        private void LinkComponentFields(MonoBehaviour component)
        {
            var fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var inputType = field.GetCustomAttribute<InputAttribute>();
                if (inputType != null)
                {
                    inputs[field.Name] = new LinkedAttributeData(component, field);
                }
                else
                {
                    var outputType = field.GetCustomAttribute<OutputAttribute>();
                    if (outputType != null)
                    {
                        outputs[field.Name] = outputs[field.Name] ?? new List<LinkedAttributeData>();
                        outputs[field.Name].Add(new LinkedAttributeData(component, field));
                    }
                }
            }
        }
    }
}
