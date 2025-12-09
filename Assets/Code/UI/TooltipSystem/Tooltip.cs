using System;
using Code.Core.Utility;
using Member.KJW.Code.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.TooltipSystem {
    public class Tooltip : MonoBehaviour, IUIElement<TooltipContext> {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Graphic background;
        [SerializeField] private Graphic border;
        [SerializeField] private Graphic outline;

        private RectTransform _rectTransform;
        private bool _followMouse;


        private void Awake() {
            _rectTransform = transform as RectTransform;
            if (!_rectTransform)
                Logging.LogError($"{name}가 RectTransform을 가지고 있지 않다네요.");
        }

        public void EnableFor(TooltipContext item) {
            gameObject.SetActive(true);
            titleText.text = item.TitleText;
            titleText.color = item.TitleColor;
            descriptionText.text = item.DescriptionText;
            descriptionText.color = item.DescriptionColor;
            background.color = item.BackgroundColor;
            border.color = item.BorderColor;
            outline.color = item.OutlineColor;
            _rectTransform.position = item.Position;

            // _followMouse = item.FollowMouse;
            // _rectTransform.anchoredPosition = item.FollowMouse switch {
            // true => inputReader.MousePos,
            // false => item.Position
            // };
        }

        public void Disable() {
            gameObject.SetActive(false);
        }
    }
}