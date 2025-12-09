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

        public void SetFillAmount(float normalizedTime)
        {
            fillTransform.localScale = new Vector3(normalizedTime,1, 1f);
        }

        /*private IEnumerator StartCoolDown(float startTime, float duration)
        {
            float endTime = startTime + duration;

            if (fillTransform != null)
            {
                fillTransform.localScale = new Vector3(2, 1, 0);
            }

            while (Time.unscaledTime < endTime)
            {
                float remaining = endTime - Time.unscaledTime;
                float progress = Mathf.Clamp01(remaining / duration); 

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
            _currentCoroutine = null;
            gameObject.SetActive(false);
            //이후 풀 메니저 사용.
        }*/

    }
}