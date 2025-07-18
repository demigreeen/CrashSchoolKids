using UnityEngine;
public class SmoothRotater : RotatePlayer
{
    [Header("SmoothPropertise")]
    [SerializeField] private float _smoothTime;
    [SerializeField] private Transform _horRotHelper;

    private float _verOld;
    private float _vertAngleVel;
    private float _horAngleVel;

    private void Start()
    {
        _horRotHelper.localRotation = transform.localRotation;
    }

    public override void RotateMove()
    {
        _verOld = vertRot;
        base.RotateMove();
    }

    protected override void RotateHorizontal()
    {
        _horRotHelper.Rotate(Vector3.up * GetHorizontalValue(), Space.Self);
        transform.localRotation
            = Quaternion.Euler(
                0f,
                Mathf.SmoothDampAngle(transform.localEulerAngles.y,
                                    _horRotHelper.localEulerAngles.y,
                                    ref _horAngleVel,
                                    _smoothTime),
               0f);
    }

    protected override void RotateVertical()
    {
        vertRot = Mathf.SmoothDampAngle(_verOld, vertRot, ref _vertAngleVel, _smoothTime);

        base.RotateVertical();

    }
}