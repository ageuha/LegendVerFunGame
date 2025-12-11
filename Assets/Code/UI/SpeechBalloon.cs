using System;
using Code.Core.GlobalSO;
using DG.Tweening;
using UnityEngine;

namespace Code.UI {
    public class SpeechBalloon : MonoBehaviour, IUIElement<Sprite> {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private TweeningInfoSO info;

        private Tweener _tweener;

        private void Awake() {
            transform.localScale = new Vector3(1, 0, 1);
            _tweener = transform.DOScaleY(1, info.Duration).SetEase(info.EasingType).SetAutoKill(false).Pause();
            gameObject.SetActive(false);
        }

        public void EnableFor(Sprite item) {
            gameObject.SetActive(true);
            transform.localScale = new Vector3(1, 0, 1);
            icon.sprite = item;
            _tweener.PlayForward();
        }

        public void Disable() {
            _tweener.PlayBackwards();
        }
    }
}