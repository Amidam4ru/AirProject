using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ControllerReader))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; // �������� �������� ������
    [SerializeField] private float _rotationSpeed = 100f; // �������� �������� (�������/���)
    [SerializeField] private float _rotationDuration = 0.2f; // ������������ �������� ��������

    private float _targetRotation; // ������� ����
    private ControllerReader _controllerReader; // ���������� �����
    private Tween _rotationTween; // ��� ���������� ������� ��������� ��������

    private void Awake()
    {
        _controllerReader = GetComponent<ControllerReader>();
    }

    private void Update()
    {
        // ���������� �������� ������
        transform.position += transform.up * _speed * Time.deltaTime;

        // ��������� �����
        float turnInput = _controllerReader.MoveDuration;

        // ���� ���� ���� �� �������
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            // ��������� ������� ���� ��������
            _targetRotation -= turnInput * _rotationSpeed * Time.deltaTime;

            // ������� ����������� (���� ����� ���������� ����, ����������� Mathf.Clamp)
            _targetRotation = _targetRotation % 360f;

            // ��������� ������� �������� � �������, ���� ��� �������
            if (_rotationTween != null && _rotationTween.IsActive())
            {
                _rotationTween.Kill();
            }

            // ������� ����� ��������
            _rotationTween = transform.DORotate(new Vector3(0, 0, _targetRotation), _rotationDuration, RotateMode.Fast)
                .SetEase(Ease.OutQuad);
        }
    }
}
