using Code.Core.Utility;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class CooldownBar : MonoBehaviour , ICooldownBar
    {
        [SerializeField] private Transform parent;
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

        public void SetBuildingSize(Vector2Int size)
        {
            parent.transform.localScale = new Vector3(size.x, size.y, 1f);
        }

        public void SetFillAmount(float normalizedTime, bool active)
        {
            if (normalizedTime < 0.01f)
            {
                parent.gameObject.SetActive(false);
            }
            else if (!parent.gameObject.activeSelf && active)
            {
                parent.gameObject.SetActive(true);
            }
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