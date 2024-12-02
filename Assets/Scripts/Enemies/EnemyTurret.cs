using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] private EnemyBullet _bulletPrefab;
    [SerializeField] private float _rayLength = 10f;  // Длина луча
    [SerializeField] private float _shotDelay = 5f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _damage;

    private float _timeCounter;

    private void Awake()
    {
        _timeCounter = 0;
    }

    private void Update()
    {
        _timeCounter += Time.deltaTime;

        // Получаем позицию и направление для луча (например, вперед из позиции объекта)
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = transform.up;

        // Визуализация луча (для дебага, можно убрать, если не нужно)
        Debug.DrawRay(rayOrigin, rayDirection * _rayLength, Color.red);

        // Выполняем Raycast
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, _rayLength, _targetLayer); ;

        if (hit.collider != null)
        {
            Health health = hit.collider.GetComponent<Health>();

            if (health != null && _timeCounter >= _shotDelay)
            {
                EnemyBullet bullet = Instantiate(_bulletPrefab, transform);
                bullet.SetDamage(_damage);
                bullet.transform.SetParent(null);

                _timeCounter = 0;
            }
        }
    }
}
