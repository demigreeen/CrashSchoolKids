using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovePlayer), typeof(RotatePlayer))]
public class PlayerController : MonoBehaviour
{

    private MovePlayer _move;
    private RotatePlayer _rotate;
    private RotatePlayer _rotateSmooth;
    private RotatePlayer _currentRotate;
    private DragAndDropItem _dragAndDropItem;

    private void Awake()
    {
        _move = GetComponent<MovePlayer>();
        _rotate = GetComponents<RotatePlayer>()[0];
        _rotateSmooth = GetComponents<RotatePlayer>()[1];
        _currentRotate = _rotateSmooth;
        _dragAndDropItem = GetComponentInChildren<DragAndDropItem>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _dragAndDropItem.CheckInput();
        _move.Jump();
        _move.PlayerPosition();
        _currentRotate.RotateMove();
    }
    private void FixedUpdate()
    {
        _move.Move();
    }
}
