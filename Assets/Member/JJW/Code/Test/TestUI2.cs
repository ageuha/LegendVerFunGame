using System;
using Member.JJW.Code.Day;
using Member.JJW.Code.Weather;
using TMPro;
using UnityEngine;

namespace Member.JJW
{
    public class TestUI2 : MonoBehaviour
    {
        private TextMeshProUGUI  _textMeshProUGUI;
        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            WorldTimeManager.Instance.UpdateHour += Change;
        }

        private void Change(int hour)
        {
            string a = hour.ToString();
            _textMeshProUGUI.text = a + "시";
        }
    }
}