using Member.JJW.Code.Interface;
using Member.JJW.Code.SO;
using UnityEngine;

namespace Member.JJW.Code.InteractableObject
{
    public abstract class InteractableObject : MonoBehaviour,IInteractable
    {
        public abstract int Hp { get; set; }
        public abstract void Interaction(int  decreaseAmount);
        public abstract void SpawnItem();
    }
}