using UnityEngine;

namespace Code.Core.GlobalSO {
    [CreateAssetMenu(fileName = "new TagHandle", menuName = "SO/GlobalSO/TagHandle", order = 0)]
    public class TagHandleSO : ScriptableObject {
        [SerializeField] private string tagName;
        private TagHandle _tagHandle;

        private void OnValidate() {
            _tagHandle = TagHandle.GetExistingTag(tagName);
        }

        public static implicit operator TagHandle(TagHandleSO so) {
            return so._tagHandle;
        }
    }
}