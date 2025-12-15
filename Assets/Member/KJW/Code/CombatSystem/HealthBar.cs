using System;
using Code.Core.Utility;
using Code.EntityScripts;
using Member.KJW.Code.CombatSystem.DamageSystem;
using Member.KJW.Code.EventChannel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KJW.Code.CombatSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HsEventChannel hsEventChannel;
        private Slider _slider;
        private TextMeshProUGUI _text;
        private HealthSystem _healthSystem;
        private IDamageable _damageable;
        private float _maxHp;
        private float Hp => _healthSystem.Hp.Value;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            hsEventChannel.OnEvent += SetHealthSystem;
        }

        public void SetHealthSystem(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
            _healthSystem.OnDamaged += SetSlider;
            _healthSystem.OnInitMaxHp += SetMaxHp;
        }

        private void SetSlider(float value)
        {
            Logging.Log(value);
            _slider.value = value;
            _text.text = $"{Hp}/{_maxHp}";
        }
        
        private void SetMaxHp(float value)
        {
            Logging.Log(value);
            _maxHp = value;
        }
    }
}