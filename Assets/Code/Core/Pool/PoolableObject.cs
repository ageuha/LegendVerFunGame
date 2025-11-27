using System;
using UnityEngine;

namespace Code.Core.Pool {
    public abstract class PoolableObject : MonoBehaviour
    {
        public event Action<PoolableObject> YouOut;

        protected void ReturnToPool()
        {
            YouOut?.Invoke(this);
        }

        public virtual void OnPopFromPool()
        {
        }

        public virtual void OnReturnToPool()
        {
        }
    }
}
