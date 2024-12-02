using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private float _experience;

    public int Points => _points;
    public float Experience => _experience;

}
