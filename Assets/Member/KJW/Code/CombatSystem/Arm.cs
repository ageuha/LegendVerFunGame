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
        private float _angle = 0;
        
        private void OnEnable()
        {
            transform.rotation = Quaternion.Euler(0, 0, _angle - 90 + firstAngle);
        }

        private void Update()
        {
            if (!gameObject.activeSelf) return;
            
            _timer +=  Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0,
                Mathf.Lerp(_angle - 90 + firstAngle, _angle - 90 + secondAngle, Mathf.InverseLerp(0, time, _timer)));

            if (!(_timer > time)) return;
            
            _timer = 0;
            transform.rotation = Quaternion.Euler(0, 0, _angle - 90 + secondAngle);
            gameObject.SetActive(false);
        }

        public Arm Init(float angle, float speed)
        {
            if (gameObject.activeSelf) return this;
            _angle = angle;
            time = speed;
            return this;
        }

        public void Swing()
        {
            if (gameObject.activeSelf) return;
            gameObject.SetActive(true);
        }
    }
}