using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateLimit; // Ёто будет максимальный угол отклонени€ от 0

    [Header("Vertical Rotation Limits")]
    [SerializeField] private float _minVerticalAngle = -80f; // ћинимальный угол
    [SerializeField] private float _maxVerticalAngle = 80f;  // ћаксимальный угол

    protected float vertRot;

    public virtual void RotateMove()
    {
        vertRot -= GetVerticalValue();

        // ќграничение вертикального поворота
        vertRot = Mathf.Clamp(vertRot, _minVerticalAngle, _maxVerticalAngle);

        RotateVertical();
        RotateHorizontal();
    }

    protected float GetVerticalValue() => Input.GetAxis("Mouse Y") * _speed * Time.deltaTime;
    protected float GetHorizontalValue() => Input.GetAxis("Mouse X") * _speed * Time.deltaTime;
    protected virtual void RotateVertical() => _cameraHolder.localRotation = Quaternion.Euler(vertRot, 0f, 0f);
    protected virtual void RotateHorizontal() => transform.Rotate(Vector3.up * GetHorizontalValue());
}