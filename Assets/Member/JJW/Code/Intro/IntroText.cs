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
            _rectTransform.DOMove(arrivePoint,arriveTime).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _textMeshPro.DOFade(0, 2).SetEase(fadeEase).OnComplete(() =>
                    {
                        Debug.Log("인트로 끝");
                        //씬바꾸기
                    });
                });
        }
    }
}
