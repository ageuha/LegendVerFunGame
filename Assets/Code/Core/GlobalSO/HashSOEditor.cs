#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Code.Core.GlobalSO {
    [CustomEditor(typeof(HashSO), true)]
    public class HashSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var hashSO = target as HashSO;
            if (GUILayout.Button("SetName")) hashSO.SetName();
        }
    }
}
#endif