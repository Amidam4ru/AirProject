using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretRotation : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main; // �������� ������ �� ������
    }

    private void Update()
    {
        // �������� ������� ���� � �������� (�� ������)
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        // ����������� ������� ���� �� �������� ��������� � �������
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0f));

        // ��������� ����������� �� ������� � ������� ����
        Vector3 direction = mouseWorldPosition - transform.position;

        // ������������ ����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������������� ������� �������
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
}
