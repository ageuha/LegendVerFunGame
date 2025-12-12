using System;
using System.Collections;
using UnityEngine;

namespace Member.JJW.Code.Intro
{
    public class IntroTextConstructor : MonoBehaviour
    {
        [SerializeField] private IntroText introTextPrefab;
        [SerializeField] private string[] introTexts;
        [SerializeField] private float textArriveTime;
        [SerializeField] private float waitTime;
        
        private int _index = 0;

        private void Start()
        {
            StartCoroutine(CreateText());
        }

        private IEnumerator CreateText()
        {
            if (introTexts[_index] == null)
                yield break;
            
            
            
            introTextPrefab.Init(introTexts[_index],textArriveTime);
            _index++;
            StartCoroutine(CreateText());
        }
    }
}