using Member.JJW.Code.Interface;
using UnityEngine;

namespace Member.JJW.Code.ResourceObject
{
    public abstract class ResourcesObject : MonoBehaviour,IInteractable<float>
    {
        public abstract void SpawnItem();
        public abstract void Interaction(float value);
    }
}