using Code.Core.GlobalSO;
using DG.Tweening;
using UnityEngine;

namespace Code.UI.SimpleFeedback {
    public class TweenPlayer : MonoBehaviour {
        [SerializeField] private TweeningInfoSO infoSO;
        [SerializeField] private RectTransform rectTransform;

        private Tweener _forwardTween;
        private Tweener _reverseTween;

        private void Awake() {
            _forwardTween =
                rectTransform.DOAnchorPos(rectTransform.anchoredPosition + (Vector2)infoSO.Position, infoSO.Duration)
                    .SetEase(infoSO.EasingType).SetLoops(2, LoopType.Yoyo).SetAutoKill(false).Pause();
            _reverseTween = rectTransform
                .DOAnchorPos(rectTransform.anchoredPosition - (Vector2)infoSO.Position, infoSO.Duration)
                .SetEase(infoSO.EasingType).SetLoops(2, LoopType.Yoyo).SetAutoKill(false).Pause();
        }

        public void PerformTween(bool resetAfterPerform = false) {
            // 이거 매개변수 필요 없는데
            _reverseTween.Pause();
            _forwardTween.Restart();
        }

        public void PerformReverseTween(bool resetAfterPerform = false) {
            _forwardTween.Pause();
            _reverseTween.Restart();
        }

        private void OnDestroy() {
            _forwardTween.Kill();
            _reverseTween.Kill();
        }

        private void Reset() {
            rectTransform = transform as RectTransform;
        }
    }
}