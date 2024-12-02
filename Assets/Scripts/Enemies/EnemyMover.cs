using DG.Tweening;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{      // Ссылка на игрока
    [SerializeField] private float _speed = 5f;        // Скорость движения
    [SerializeField] private float _turnDuration = 0.5f; // Время поворота для сглаживания
    [SerializeField] private float _avoidanceRadius = 2f; // Радиус уклонения от других врагов
    [SerializeField] private float _avoidanceStrength = 1f; // Сила уклонения

    private Vector3 _targetDirection; // Итоговое направление движения
    private Transform _player;

    private void Start()
    {
        _player = FindAnyObjectByType<PlayerMover>().GetComponent<Transform>();
        // Изначальное направление на игрока
        _targetDirection = (_player.position - transform.position).normalized;
    }

    private void Update()
    {
        // Рассчитываем направление к игроку
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

        // Учитываем уклонение
        Vector3 avoidanceDirection = GetAvoidanceDirection();
        _targetDirection = (directionToPlayer + avoidanceDirection).normalized;

        // Поворачиваем врага в нужное направление с помощью DoTween
        RotateTowardsTarget(_targetDirection);

        // Двигаемся вперед
        MoveForward();
    }

    private Vector3 GetAvoidanceDirection()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, _avoidanceRadius);
        Vector3 avoidance = Vector3.zero;

        foreach (var enemy in nearbyEnemies)
        {
            if (enemy.transform == transform) // Игнорируем себя
                continue;

            // Направление уклонения
            Vector3 toEnemy = transform.position - enemy.transform.position;
            avoidance += toEnemy.normalized / toEnemy.magnitude;
        }

        return avoidance * _avoidanceStrength;
    }

    private void RotateTowardsTarget(Vector3 targetDirection)
    {
        // Вычисляем угол между текущим направлением и целью
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

        // Плавно поворачиваем объект с использованием DoTween
        transform.DORotate(new Vector3(0, 0, angle), _turnDuration, RotateMode.Fast);
    }

    private void MoveForward()
    {
        // Двигаем врага вперед в направлении transform.up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса уклонения в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _avoidanceRadius);
    }
}
