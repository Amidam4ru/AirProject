using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TurretShooter : MonoBehaviour
{
    [SerializeField] private ControllerReader _controller;
    [SerializeField] private PlayerBullet _bulletPrefab;
    [SerializeField] private float _shotDelay = 1.0f;

    private WaitForSeconds _shotWait;
    private Coroutine _delayShotCorutine;
    private PlayerBullet _bullet;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _shotWait = new WaitForSeconds(_shotDelay);
        _delayShotCorutine = StartCoroutine(DelayShot());
    }

    private void OnDisable()
    {
        StopCoroutine(DelayShot());
    }

    private void Shot()
    {
        _bullet = Instantiate(_bulletPrefab, transform);
        _bullet.transform.SetParent(null);
    }

    private IEnumerator DelayShot()
    {
        while (true)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            Shot();
            yield return _shotWait;
        }
    }
}
