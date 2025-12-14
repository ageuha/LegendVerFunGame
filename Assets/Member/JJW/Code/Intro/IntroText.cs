using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Member.JJW.Code.Intro
{
    public class IntroText : MonoBehaviour
    {
        [SerializeField] private Vector3 arrivePoint;
        [SerializeField] private Ease fadeEase = Ease.Linear;
        [SerializeField] private float arriveTime = 10f;
        private TextMeshPro _textMeshPro;
        private RectTransform _rectTransform;
    
        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshPro>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _textMeshPro.DOFade(0, arriveTime).SetEase(fadeEase);
            _rectTransform.DOMove(arrivePoint,arriveTime).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Debug.Log("인트로 끝");
                    //메인 게임씬으로 넘어가기
                });
        }
    }
}
