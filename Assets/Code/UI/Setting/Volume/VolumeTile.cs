using Code.Core.GlobalInterfaces;
using Code.Core.Utility;
using Code.UI.SimpleFeedback;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.Setting.Volume {
    public class VolumeTile : MonoBehaviour, IPointerClickHandler {
        [SerializeField] private SerializeHelper<ISubscribable<bool>> toggleEvent;
        [SerializeField] private TweenPlayer tweenPlayer;
        [SerializeField] private SerializeHelper<SettingModule<float>> settingModule;

        private float ReciprocalOfSiblingCount => 1f / transform.parent.childCount;

        private void Reset() {
            toggleEvent ??= new SerializeHelper<ISubscribable<bool>>();
            toggleEvent.SetEditorValue(GetComponent<ISubscribable<bool>>());
            tweenPlayer = GetComponent<TweenPlayer>();
        }

        private void Awake() {
            toggleEvent.Value.Subscribe(HandleToggleEvent);
        }

        private void OnDestroy() {
            toggleEvent.Value.Unsubscribe(HandleToggleEvent);
        }

        private void HandleToggleEvent(bool value) {
            if (value) tweenPlayer.PerformTween(true);
            else tweenPlayer.PerformReverseTween(true);
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            if (!Mathf.Approximately(settingModule.Value.ExposedValue.Value,
                    transform.GetSiblingIndex() * ReciprocalOfSiblingCount + ReciprocalOfSiblingCount)) {
                settingModule.Value.SetSettingValue(transform.GetSiblingIndex() * ReciprocalOfSiblingCount);
                Logging.Log("1번");
            }
            else {
                settingModule.Value.SetSettingValue(transform.GetSiblingIndex() * ReciprocalOfSiblingCount -
                                                    ReciprocalOfSiblingCount);
                Logging.Log("2번");
            }
        }
    }
}