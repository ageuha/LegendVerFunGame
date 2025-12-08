using Code.Core.Pool;
using Code.UI.Setting;
using UnityEngine;

namespace Code.Tests {
    public class TestSaveSystem : MonoBehaviour {
        [SerializeField] private TestPoolable poolable;
        
        [ContextMenu("Save")]
        public void Save() => SettingSaveManager.Instance.SaveSetting();

        [ContextMenu("Pop")]
        public void Pop() => PoolManager.Instance.Factory<TestPoolable>().Pop();
        
        [ContextMenu("Push")]
        public void Push() => PoolManager.Instance.Factory<TestPoolable>().Push(poolable);
    }
}