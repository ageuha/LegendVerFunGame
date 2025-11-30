using System;
using UnityEditor;
using UnityEngine;

namespace Code.Core.Utility {
    [Serializable]
    public class SerializeHelper<T> : ISerializationCallbackReceiver where T : class {
        [SerializeField] private MonoBehaviour value;
      
        private T _valueAsT;

        public T Value {
            get => _valueAsT ??= value as T ?? value.GetComponent<T>();
            set => _valueAsT = value;
        }

        public static implicit operator T(SerializeHelper<T> value) => value.Value;

        public void OnBeforeSerialize() {
            if (!value) return;
            if (value is not T) {
                if (!value.TryGetComponent<T>(out var component)) {
                    Debug.LogError($"Serialized MonoBehaviour does not implement interface {typeof(T).Name}");
                    value = null;
                }
            }
        }

        public void OnAfterDeserialize() {
        }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SerializeHelper<>))]
    public class SerializableInterfaceDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty valueProp = property.FindPropertyRelative("value");
            EditorGUI.PropertyField(position, valueProp, label);
        }
    }
#endif
}