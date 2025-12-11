using Code.Core.Utility;
using Unity.Behavior;
using Unity.Behavior.GraphFramework;
using UnityEngine;

namespace Code.Core.GlobalSO {
    [CreateAssetMenu(fileName = "GUID", menuName = "SO/GlobalSO/BTGUIDSO", order = 0)]
    public class BlackBoardGUIDSO : ScriptableObject {
        [SerializeField] private string variableName;
        [SerializeField] private BehaviorGraph graph;
        [HideInInspector] [SerializeField] private SerializableGUID guid;

        public string VariableName => variableName;
        public BehaviorGraph Graph => graph;
        public SerializableGUID Guid => guid;
        public bool HasValidGuid => guid != default;

        private void OnValidate() {
#if UNITY_EDITOR
            UpdateGuid();
#endif
        }

#if UNITY_EDITOR
        [ContextMenu("Refresh GUID")]
        private void RefreshGuidContextMenu() {
            UpdateGuid();
        }

        private void UpdateGuid() {
            if (!graph) {
                ResetGuid();
                Logging.LogWarning($"[{name}] BehaviorGraph 가 설정되지 않았습니다.");
                return;
            }

            var bbRef = graph.BlackboardReference;
            if (bbRef == null) {
                ResetGuid();
                Logging.LogError($"[{name}] Graph '{graph.name}' 에 BlackboardReference 가 없습니다.");
                return;
            }

            if (string.IsNullOrEmpty(variableName)) {
                ResetGuid();
                return;
            }

            if (bbRef.GetVariableID(variableName, out guid)) {
                return;
            }

            ResetGuid();
            Logging.LogWarning(
                $"[{name}] Graph '{graph.name}' 의 Blackboard 에 " +
                $"변수 '{variableName}' 를 찾을 수 없습니다.");
        }

        private void ResetGuid() {
            guid = default;
        }
#endif

        public static implicit operator SerializableGUID(BlackBoardGUIDSO so) {
            if (!so) {
                Logging.LogError("BlackBoardGUIDSO 참조가 null 입니다.");
                return default;
            }

            if (so.guid == default) {
                Logging.LogWarning(
                    $"BlackBoardGUIDSO '{so.name}' 에 유효한 GUID가 없습니다. " +
                    $"Graph='{so.graph?.name}', Variable='{so.variableName}'");
            }

            return so.guid;
        }
    }
}