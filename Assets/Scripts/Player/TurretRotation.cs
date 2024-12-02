using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretRotation : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main; // Получаем ссылку на камеру
    }

    private void Update()
    {
        // Получаем позицию мыши в пикселях (на экране)
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        // Преобразуем позицию мыши из экранных координат в мировые
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0f));

        // Вычисляем направление от объекта к позиции мыши
        Vector3 direction = mouseWorldPosition - transform.position;

        // Рассчитываем угол
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Устанавливаем поворот объекта
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
}
