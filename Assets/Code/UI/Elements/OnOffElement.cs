using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements {
    [RequireComponent(typeof(Image))]
    public class OnOffElement : MonoBehaviour, IUIElement<bool> {
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        [SerializeField] private Image visual;

        private void OnValidate() {
            visual ??= GetComponent<Image>();
        }

        public void EnableFor(bool item) {
            gameObject.SetActive(true);
            visual.sprite = item ? onSprite : offSprite;
        }

        public void Disable() {
            gameObject.SetActive(false);
        }
    }
}