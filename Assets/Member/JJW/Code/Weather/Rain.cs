using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Member.JJW.Code.Weather
{
    public class Rain : Weather
    {
        [SerializeField] private GameObject rainPanel;
        private Image _rainPanelImage;
        private void Awake()
        {
            _rainPanelImage = rainPanel.GetComponent<Image>();
        }
        public override void OnStart()
        {
            _rainPanelImage.DOFade(0.5f, 1);
            //파티클 시작
        }

        public override void OnStop()
        {
            _rainPanelImage.DOFade(0, 1);
            //파티클 끝
        }
    }
}