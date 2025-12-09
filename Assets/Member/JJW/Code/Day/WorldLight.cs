using System;
using Member.JJW.Code.Day;
using Member.JJW.Code.Weather;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Gradient gradient;

    [Header("Intensity")]
    [SerializeField] private float minIntensity = 0.1f;   // 가장 어두운 값
    [SerializeField] private float maxIntensity = 1.3f;   // 가장 밝은 값 (정오)
    [SerializeField] private AnimationCurve intensityCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Transition")]
    [SerializeField] private float transitionSpeed = 1f; // 클수록 빠름

    private Light2D _light;
    private Color _targetColor;
    private Color _currentColor;
    private float _targetIntensity;
    private float _currentIntensity;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _currentColor = _light.color;
        _currentIntensity = _light.intensity;
        _targetColor = _currentColor;
        _targetIntensity = _currentIntensity;
    }

    private void OnEnable()
    {
        if (WorldTimeManager.Instance != null)
            WorldTimeManager.Instance.UpdateHour += OnUpdateHour;
    }

    private void OnDisable()
    {
        if (WorldTimeManager.Instance != null)
            WorldTimeManager.Instance.UpdateHour -= OnUpdateHour;
    }

    private void Update()
    {
        // 색과 인텐시티를 부드럽게 보간
        _currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * transitionSpeed);
        _currentIntensity = Mathf.Lerp(_currentIntensity, _targetIntensity, Time.deltaTime * transitionSpeed);

        _light.color = _currentColor;
        _light.intensity = _currentIntensity;
    }

    private void OnUpdateHour(int hour)
    {
        // hour (0~23)을 0~1로 매핑. 23으로 나누면 0~1에 고르게 분포
        float t = hour / 23f;

        // 그라디언트에서 색 뽑기
        _targetColor = gradient.Evaluate(t);

        // 인텐시티는 curve로 조절한 후 min~max 범위로 매핑
        float curveVal = Mathf.Clamp01(intensityCurve.Evaluate(t)); // 0~1
        _targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, curveVal);
    }
}
