using Code.Core.GlobalSO;
using DG.Tweening;
using UnityEngine;

namespace Code.UI.SimpleFeedback {
    public class TweenPlayer : MonoBehaviour {
        [SerializeField] private TweeningInfoSO infoSO;
        [SerializeField] private RectTransform rectTransform;

        private Tween _tween;

        public void PerformTween(bool resetAfterPerform = false) {
            // 이거 매개변수 이름이 이상한데 그 유니티 이벤트에서 싹 다 빠질까봐 무서워서 못 바꾸는 중. 그냥 봐
            _tween?.Complete();
            _tween =
                rectTransform.DOAnchorPos(rectTransform.anchoredPosition + (Vector2)infoSO.Position, infoSO.Duration)
                    .SetEase(infoSO.EasingType);
            if (resetAfterPerform) _tween.SetLoops(2, LoopType.Yoyo);
        }

        public void PerformReverseTween(bool resetAfterPerform = false) {
            _tween?.Complete();
            _tween =
                rectTransform.DOAnchorPos(rectTransform.anchoredPosition - (Vector2)infoSO.Position, infoSO.Duration)
                    .SetEase(infoSO.EasingType);
            if (resetAfterPerform) _tween.SetLoops(2, LoopType.Yoyo);
        }

        private void Reset() {
            rectTransform = transform as RectTransform;
        }
    }
}