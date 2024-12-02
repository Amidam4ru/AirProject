using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _value = 100;
    [SerializeField] private Color _damageColor = Color.red;
    [SerializeField] private float _colorChangeDuration = 1f;
    [SerializeField] private CinemachineVirtualCamera _cinemachine;
    [SerializeField] private float _shakeTime;
    [SerializeField] private float _shakeDuration = 3;

    private SpriteRenderer _spriteRenderer;
    private CinemachineBasicMultiChannelPerlin _channels;
    private WaitForSeconds _delay;
    private Coroutine _shakeCorutine;
    private float _startShakeDuration;

    public event Action<float> DamageTaked;

    public float Value => _value;

    private void Awake()
    {
        _delay = new WaitForSeconds(_shakeTime);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _channels = _cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _startShakeDuration = _channels.m_AmplitudeGain;
    }

    private void Start()
    {
        DamageTaked?.Invoke(_value);
    }

    public void TakeDamage(float damage)
    {
        AnimateDamage();

        if (_shakeCorutine != null)
        {
            StopCoroutine(_shakeCorutine);
        }

        _shakeCorutine = StartCoroutine(Shake());

        _value -= damage;
        DamageTaked?.Invoke(_value);

        if (_value <= 0)
        {
            Die();
        }
    }

    private void AnimateDamage()
    {
        Color originalColor = _spriteRenderer.color;

        _spriteRenderer.DOColor(_damageColor, _colorChangeDuration).OnKill(() =>
        {
            _spriteRenderer.DOColor(originalColor, _colorChangeDuration);
        });
    }

    private IEnumerator Shake()
    {
        _channels.m_AmplitudeGain = _shakeDuration;
        _channels.m_FrequencyGain = _shakeDuration;

        yield return _delay;

        _channels.m_AmplitudeGain = _startShakeDuration;
        _channels.m_FrequencyGain = _startShakeDuration;
    }

    private void Die()
    {
        Debug.Log(1);
    }
}
