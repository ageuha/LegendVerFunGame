using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Member.JJW.Code.Intro
{
    public class IntroText : MonoBehaviour
    {
        private TextMeshPro _textMeshPro;
        private RectTransform _rectTransform;
    
        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshPro>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Init(string text, float arriveTime)
        {
            _textMeshPro.text = text;
            _rectTransform.DOMove(new Vector3(0, 25, 23),arriveTime);
            _textMeshPro.DOFade(0, arriveTime);
        }
    }
}
