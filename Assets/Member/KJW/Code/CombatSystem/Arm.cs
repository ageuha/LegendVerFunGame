using System;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Arm : MonoBehaviour
    {
        [SerializeField] private float firstAngle;
        [SerializeField] private float secondAngle;
        [SerializeField] private float time;
        private float _timer = 0;
        
        private void OnEnable()
        {
            transform.rotation = Quaternion.Euler(0, 0, firstAngle);
        }

        private void Update()
        {
            if (!gameObject.activeSelf) return;
            
            _timer +=  Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0,
                Mathf.Lerp(firstAngle, secondAngle, Mathf.InverseLerp(0, time, _timer)));

            if (!(_timer > time)) return;
            
            _timer = 0;
            transform.rotation = Quaternion.Euler(0, 0, secondAngle);
            gameObject.SetActive(false);
        }

        [ContextMenu("Swing")]
        public void Swing()
        {
            gameObject.SetActive(true);
        }
    }
}