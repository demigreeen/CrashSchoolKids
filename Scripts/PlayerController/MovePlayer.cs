using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _jumpPower = 1;
    [SerializeField] LayerMask groundMask;

    private float currentSpeed = 0;

    private Rigidbody rb;
    private Vector3 _moveDir;
    [SerializeField]private bool isGrounded = false;

    private void Awake(){ rb = GetComponent<Rigidbody>(); }

    private void Update()
    {
        CheckGround();
    }

    public void PlayerPosition()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, .75f, transform.localScale.z);
            currentSpeed = _speed / 2;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            currentSpeed = _speed;
        }
    }

    public void Move()
    {
        _moveDir = ((rb.transform.right * Input.GetAxis("Horizontal")) + (rb.transform.forward * Input.GetAxis("Vertical")));

        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed += _speed / 2; // Увеличиваем скорость при беге
        }

        rb.MovePosition( rb.transform.position + _moveDir * currentSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(0f, _jumpPower, 0f, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y * 0.5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = 0.75f;

        Debug.DrawRay(origin, direction * distance, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

}
