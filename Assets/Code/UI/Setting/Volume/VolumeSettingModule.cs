using System.Collections.Generic;
using Code.UI.Elements;
using TMPro;
using UnityEngine;

namespace Code.UI.Setting.Volume {
    public class VolumeSettingModule : SettingModule<float> {
        [SerializeField] private List<OnOffElement> elements;
        [SerializeField] private TextMeshProUGUI tmp;

        private sbyte _idx;

        protected override void AfterAwake() {
            base.AfterAwake();
            _idx = 0; // 나중에 세이브 시스템 구축하면 값 가져와야 함.
            AfterInteract();
            InitializeElements();
        }

        private void InitializeElements() {
            for (int i = 0; i < elements.Count; i++) {
                elements[i].EnableFor(i <= _idx);
            }
        }

        private void AfterInteract() {
            leftButton.interactable = _idx > -1;
            rightButton.interactable = _idx < elements.Count - 1;
            float percentage = (_idx + 1) / (float)elements.Count;
            SettingValue.Value = percentage;
            percentage *= 100f;
            tmp.text = $"{percentage}%";
        }

        protected override void OnLeftButtonClicked() {
            if (!leftButton.interactable) return;
            base.OnLeftButtonClicked();
            elements[_idx--].EnableFor(false);
            AfterInteract();
        }

        protected override void OnRightButtonClicked() {
            if (!rightButton.interactable) return;
            base.OnRightButtonClicked();
            elements[++_idx].EnableFor(true);

            AfterInteract();
        }
    }
}