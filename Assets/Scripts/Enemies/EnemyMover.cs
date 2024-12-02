using DG.Tweening;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{      // ������ �� ������
    [SerializeField] private float _speed = 5f;        // �������� ��������
    [SerializeField] private float _turnDuration = 0.5f; // ����� �������� ��� �����������
    [SerializeField] private float _avoidanceRadius = 2f; // ������ ��������� �� ������ ������
    [SerializeField] private float _avoidanceStrength = 1f; // ���� ���������

    private Vector3 _targetDirection; // �������� ����������� ��������
    private Transform _player;

    private void Start()
    {
        _player = FindAnyObjectByType<PlayerMover>().GetComponent<Transform>();
        // ����������� ����������� �� ������
        _targetDirection = (_player.position - transform.position).normalized;
    }

    private void Update()
    {
        // ������������ ����������� � ������
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

        // ��������� ���������
        Vector3 avoidanceDirection = GetAvoidanceDirection();
        _targetDirection = (directionToPlayer + avoidanceDirection).normalized;

        // ������������ ����� � ������ ����������� � ������� DoTween
        RotateTowardsTarget(_targetDirection);

        // ��������� ������
        MoveForward();
    }

    private Vector3 GetAvoidanceDirection()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, _avoidanceRadius);
        Vector3 avoidance = Vector3.zero;

        foreach (var enemy in nearbyEnemies)
        {
            if (enemy.transform == transform) // ���������� ����
                continue;

            // ����������� ���������
            Vector3 toEnemy = transform.position - enemy.transform.position;
            avoidance += toEnemy.normalized / toEnemy.magnitude;
        }

        return avoidance * _avoidanceStrength;
    }

    private void RotateTowardsTarget(Vector3 targetDirection)
    {
        // ��������� ���� ����� ������� ������������ � �����
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

        // ������ ������������ ������ � �������������� DoTween
        transform.DORotate(new Vector3(0, 0, angle), _turnDuration, RotateMode.Fast);
    }

    private void MoveForward()
    {
        // ������� ����� ������ � ����������� transform.up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // ������������ ������� ��������� � ���������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _avoidanceRadius);
    }
}
