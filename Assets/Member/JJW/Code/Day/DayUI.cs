using System;
using System.Collections;
using DG.Tweening;
using Member.JJW.Code.Day;
using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float fadeSpeed = 0.5f;
    private TextMeshProUGUI _textMeshProUGUI;
    private void Awake()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        WorldTimeManager.Instance.UpdateDay += ChangeDay;
    }

    private void ChangeDay(int value)
    {
        string dayCount = value.ToString();
        _textMeshProUGUI.text = "DAY "+ dayCount;
        _textMeshProUGUI.DOFade(1, fadeSpeed).OnComplete(() =>
        {
            StartCoroutine(DisableText());
        });
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(waitTime);
        _textMeshProUGUI.DOFade(0, fadeSpeed);
    }

    private void OnDestroy()
    {
        WorldTimeManager.Instance.UpdateDay -= ChangeDay;
    }
}
