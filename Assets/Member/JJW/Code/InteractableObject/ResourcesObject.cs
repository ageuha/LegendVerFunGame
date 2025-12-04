using Member.JJW.Code.Interface;
using Member.JJW.Code.SO;
using UnityEngine;

namespace Member.JJW.Code.InteractableObject
{
    public abstract class ResourcesObject : MonoBehaviour,IInteractable<float>
    {
        public abstract void SpawnItem();
        public abstract void Interaction(float value);
    }
}