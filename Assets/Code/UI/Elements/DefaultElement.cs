using Code.Core.GlobalStructs;
using UnityEngine;

namespace Code.UI.Elements {
    public class DefaultElement : MonoBehaviour, IUIElement<Empty> {
        public void EnableFor(Empty item) {
            gameObject.SetActive(true);
        }

        public void Disable() {
            gameObject.SetActive(false);
        }
    }
}