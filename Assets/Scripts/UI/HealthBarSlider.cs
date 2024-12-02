using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _fill;

    private Slider _slider;
    private RectTransform _rectTransform;
    private float _maxHealth;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.sizeDelta = new Vector2((_playerHealth.Value * _rectTransform.sizeDelta.x/100), _rectTransform.sizeDelta.y);
        _background.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _background.sizeDelta.y);
        _fill.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _fill.sizeDelta.y);
        _maxHealth = _playerHealth.Value;
    }

    private void OnEnable()
    {
        _playerHealth.DamageTaked += ChangeValue;
    }

    private void OnDisable()
    {
        _playerHealth.DamageTaked -= ChangeValue;
    }

    private void ChangeValue(float value)
    { 
        _slider.value = value/_maxHealth*100;
    }
}
