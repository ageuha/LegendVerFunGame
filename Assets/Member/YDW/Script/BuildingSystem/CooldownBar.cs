using System;
using System.Collections;
using Code.Core.Utility;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class CooldownBar : MonoBehaviour , ICooldownBar
    {
        [SerializeField] private Transform fillTransform;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
            if (fillTransform == null)
            {
                Logging.Log("fillTransform is null");
            }

            if (fillTransform != null)
            {
                fillTransform.localScale = new Vector3(2, 1, 0f);
            }
            
        }

        public void SetActiveBar(float startTime,float time)
        {
            if(_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            gameObject.SetActive(true);
            
            _currentCoroutine = StartCoroutine(StartCoolDown(startTime, time));
        }

        private IEnumerator StartCoolDown(float startTime, float duration)
        {
            Logging.Log("바 쿨타임 시작.");
            float endTime = startTime + duration;

            if (fillTransform != null)
            {
                fillTransform.localScale = new Vector3(2, 1, 0); // 시작 시 풀 게이지
            }

            while (Time.unscaledTime < endTime)
            {
                float remaining = endTime - Time.unscaledTime;
                float progress = Mathf.Clamp01(remaining / duration);   // 1 → 0

                float newScaleX = Mathf.Lerp(0f, 2f, progress);

                if (fillTransform != null)
                {
                    fillTransform.localScale = new Vector3(newScaleX, 1, 0);
                }

                yield return null;
            }
            
            if (fillTransform != null)
            {
                fillTransform.localScale = new Vector3(0, 1, 0);
            }

            Logging.Log($"바 쿨타임 종료. {endTime}");
            _currentCoroutine = null;
            Destroy(transform.parent.gameObject);
            //이후 풀 메니저 사용.
        }

    }
}