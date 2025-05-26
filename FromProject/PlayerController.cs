using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private MovePlayer _move;
    private CameraControll _rotate;

    Vector3 startPos;
    bool isMobile;

    private void Awake()
    {
        _move = GetComponent<MovePlayer>();
        _rotate = GetComponentInChildren<CameraControll>();

        isMobile = RBDeviceType.MyIsMobile();
        if (isMobile == false )
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        startPos = transform.position;
    }

    private void Update()
    {
        _rotate.Rotate();
        _move.Jump();
        if ( transform.position.y < -5)
        {
            transform.position = startPos;
        }
    }
    private void FixedUpdate()
    {
        _move.PlayerPosition();
        _move.Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) Debug.Log(collision.transform.name);
    }
}
