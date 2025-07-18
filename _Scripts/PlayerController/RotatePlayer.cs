using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateLimit; // ��� ����� ������������ ���� ���������� �� 0

    [Header("Vertical Rotation Limits")]
    [SerializeField] private float _minVerticalAngle = -80f; // ����������� ����
    [SerializeField] private float _maxVerticalAngle = 80f;  // ������������ ����

    protected float vertRot;

    public virtual void RotateMove()
    {
        vertRot -= GetVerticalValue();

        // ����������� ������������� ��������
        vertRot = Mathf.Clamp(vertRot, _minVerticalAngle, _maxVerticalAngle);

        RotateVertical();
        RotateHorizontal();
    }

    protected float GetVerticalValue() => Input.GetAxis("Mouse Y") * _speed * Time.deltaTime;
    protected float GetHorizontalValue() => Input.GetAxis("Mouse X") * _speed * Time.deltaTime;
    protected virtual void RotateVertical() => _cameraHolder.localRotation = Quaternion.Euler(vertRot, 0f, 0f);
    protected virtual void RotateHorizontal() => transform.Rotate(Vector3.up * GetHorizontalValue());
}