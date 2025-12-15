using Code.AnimatorSystem;
using UnityEngine;

namespace Member.KJW.Code.Player
{
    public class PlayerRenderer : AnimatorCompo
    {
        public void SetFlip(float xVelocity) {
            transform.localRotation = Quaternion.Euler(0f, xVelocity > 0 ? 180f : 0f, 0f);
        }
    }
}