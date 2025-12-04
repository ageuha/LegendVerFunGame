using Member.JJW.Code.Interface;
using Member.JJW.Code.SO;
using UnityEngine;

namespace Member.JJW.Code.CollectableObject
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollectableObject : MonoBehaviour, ICollectable
    {
        [SerializeField] private float speed;
        [SerializeField] private CollectableObjectSO collectableObjectSO;

        private bool _isCanGetThisItem;

        public CollectableObjectSO GetThisItem()
        {
            if (_isCanGetThisItem)
            {
                return collectableObjectSO;
                Debug.Log(collectableObjectSO.name + "얻음");
            }
            return null;
        }
    }
}