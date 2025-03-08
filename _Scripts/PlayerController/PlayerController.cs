using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private MovePlayer _move;
    private CameraControll _rotate;

    private void Awake()
    {
        _move = GetComponent<MovePlayer>();
        _rotate = GetComponentInChildren<CameraControll>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _rotate.Rotate();
        _move.Jump();
        _move.PlayerPosition();
    }
    private void FixedUpdate()
    {
        _move.Move();
    }
}
