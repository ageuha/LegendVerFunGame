using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;

namespace Code.UI.Tab {
    public class TabGroup : MonoBehaviour {
        [SerializeField] private int defaultPageIdx;

        private List<ITabButton> _tabButtons = new();
        private ITabButton _selectedTab;

        private void Start() {
            if (_selectedTab == null) {
                _selectedTab = _tabButtons.Find(btn => btn.Index == defaultPageIdx);
                _selectedTab?.ActivateTab();
            }
        }

        public void ResisterTab(ITabButton tabButton) {
            if (tabButton == null) {
                Logging.LogError("tabButton이 null입니다.");
                return;
            }

            if (_tabButtons.Contains(tabButton)) {
                Logging.LogError("같은 tabButton이 두 번 등록 되었습니다.");
                return;
            }

            if (_tabButtons.Exists(btn => btn.Index == tabButton.Index)) {
                Logging.LogError($"tabButton의 인덱스가 {tabButton.Index}로 겹칩니다.");
                return;
            }

            _tabButtons.Add(tabButton);
        }

        public void OnTabSelected(ITabButton tabButton) {
            if (tabButton == null || tabButton == _selectedTab)
                return;
            _selectedTab = tabButton;
            foreach (var tab in _tabButtons) {
                tab.DeactivateTab();
            }

            _selectedTab.ActivateTab();
        }
    }
}