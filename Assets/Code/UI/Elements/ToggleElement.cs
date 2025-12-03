using System;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Elements {
    [RequireComponent(typeof(Image))]
    public class ToggleElement : MonoBehaviour, IUIElement<bool> {
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        [SerializeField] private Image visual;

        private bool _isOn;

        private void OnValidate() {
            visual ??= GetComponent<Image>();
        }

        private void Awake() {
            visual.sprite = _isOn ? onSprite : offSprite;
        }

        public void EnableFor(bool item) {
            gameObject.SetActive(true);
            if (_isOn == item) return;
            _isOn = item;
            visual.sprite = _isOn ? onSprite : offSprite;
            AfterEnable(item);
        }

        protected virtual void AfterEnable(bool item) {
        }

        public virtual void Disable() {
            gameObject.SetActive(false);
        }
    }
}