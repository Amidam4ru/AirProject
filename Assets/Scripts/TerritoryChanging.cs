using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TerritoryChanging : MonoBehaviour
{
    [SerializeField] private int _level;

    private int _unitOfAddition;
    private CircleCollider2D _circleCollider;

    public int Level => _level;

    private void Awake()
    {
        _unitOfAddition = 5;
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.radius += _unitOfAddition * _level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Cloud cloud))
        {
            Destroy(cloud.gameObject);
        }
    }
}
