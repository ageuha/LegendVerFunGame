using System;
using Member.JJW.EventChannel;
using TMPro;
using UnityEngine;

namespace Member.JJW
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField] private FloatEventChannel floatEventChannel;
        private TextMeshProUGUI _textMeshProUGUI;
        private void Awake()
        {
            floatEventChannel.OnEvent += Change;
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void Change(float value)
        {
            string temperature = value.ToString("F1");
            _textMeshProUGUI.text = temperature + "도"; 
        }
    }
}