using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private MovePlayer _move;
    private CameraControll _rotate;

    bool isMobile;

    private void Awake()
    {
        _move = GetComponent<MovePlayer>();
        _rotate = GetComponentInChildren<CameraControll>();

        isMobile = RBDeviceType.isMobile();
        if (isMobile == false )
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        _move.PlayerPosition();
        if (isMobile == false)
        {
            _rotate.Rotate();
        }
        _move.Jump();

    }
    private void FixedUpdate()
    {
        _move.Move();

    }
}
