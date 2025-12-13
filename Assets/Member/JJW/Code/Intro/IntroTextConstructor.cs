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
            if (_index >= introTexts.Length)
            {
                Debug.Log("인트로 끝남");
                yield break;
            }
            
            yield return new WaitForSeconds(waitTime);
            IntroText introText = Instantiate(introTextPrefab,transform.position,Quaternion.Euler(40,0,0));
            introText.Init(introTexts[_index],textArriveTime);
            _index++;
            StartCoroutine(CreateText());
        }
    }
}