using System;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private float _updateTime = 10f;

    private float _timer;

    public event Action<float> TimeUpdated;

    private void Start()
    {
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _updateTime)
        {
            TimeUpdated?.Invoke(_updateTime);

            _timer = 0;
        }
    }
}
