using System.Collections;
using Code.Core.Utility;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KJW.Code.SceneManagement
{
    public class SceneManager : MonoSingleton<SceneManager>
    {
        private Image _fade;
        private Coroutine _fadeCoroutine;

        protected override void Awake()
        {
            _fade = GetComponentInChildren<Image>(true);
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            DoFade(1, 0, 1, 0);
        }
        
        public void DoFade(float startAlpha, float endAlpha, float duration, float delay)
        {
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }

            _fadeCoroutine = StartCoroutine(FadeCoroutine(startAlpha, endAlpha, duration, delay));
        }

        private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration, float delay)
        {
            _fade.enabled = true;
            _fade.color = new Color(0, 0, 0, startAlpha);
            yield return null;
            yield return new WaitForSeconds(delay);
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime;
                float percentage = t/ duration;
                percentage = Mathf.Clamp01(percentage);
                _fade.color = new Color(0, 0, 0, Mathf.Lerp(startAlpha, endAlpha, percentage));
                yield return null;
            }

            _fade.color = new Color(0, 0, 0, endAlpha);

            if (endAlpha <= 0)
            {
                _fade.enabled = false;
            }
        }
        
        private IEnumerator LoadSceneWithFade(SceneID sceneId)
        {
            yield return StartCoroutine(FadeCoroutine(0, 1, 1, 0));

            sceneId.Load();
        }

        public void LoadScene(SceneID sceneId)
        {
            StartCoroutine(LoadSceneWithFade(sceneId));
        }
    }
}