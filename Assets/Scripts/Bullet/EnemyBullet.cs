using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private float _damage;

    private void Start()
    {
        _damage = 10;
    }

    private void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Health player))
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
}
