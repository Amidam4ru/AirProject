using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ControllerReader))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; // Скорость движения вперед
    [SerializeField] private float _rotationSpeed = 100f; // Скорость поворота (градусы/сек)
    [SerializeField] private float _rotationDuration = 0.2f; // Длительность анимации поворота

    private float _targetRotation; // Целевой угол
    private ControllerReader _controllerReader; // Обработчик ввода
    private Tween _rotationTween; // Для управления текущей анимацией поворота

    private void Awake()
    {
        _controllerReader = GetComponent<ControllerReader>();
    }

    private void Update()
    {
        // Постоянное движение вперед
        transform.position += transform.up * _speed * Time.deltaTime;

        // Получение ввода
        float turnInput = _controllerReader.MoveDuration;

        // Если есть ввод на поворот
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            // Обновляем целевой угол поворота
            _targetRotation -= turnInput * _rotationSpeed * Time.deltaTime;

            // Убираем ограничения (если нужно ограничить угол, используйте Mathf.Clamp)
            _targetRotation = _targetRotation % 360f;

            // Проверяем текущую анимацию и убиваем, если она активна
            if (_rotationTween != null && _rotationTween.IsActive())
            {
                _rotationTween.Kill();
            }

            // Создаем новую анимацию
            _rotationTween = transform.DORotate(new Vector3(0, 0, _targetRotation), _rotationDuration, RotateMode.Fast)
                .SetEase(Ease.OutQuad);
        }
    }
}
