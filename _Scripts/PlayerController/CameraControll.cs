using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float sensitivity = 100f; // ���������������� ����
    public float xMultiply = 1f;
    public Transform playerBody; // ���� ������, � �������� ����� ����������� ������

    private float xRotation = 0f; // ���� �������� �� ��� X

    private void Start()
    {
        if (RBDeviceType.isMobile() == true)
        {
            sensitivity = sensitivity / 10F;
        }
    }
    public void Rotate()
    {
        // �������� ���� � ����
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // ������������ ���� � ������� �� ��������� �����������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 3 && touch.position.y > Screen.height / 4.5f)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    mouseX = touch.deltaPosition.x * sensitivity * Time.deltaTime;
                    mouseY = touch.deltaPosition.y * sensitivity * Time.deltaTime;
                }
            }
            if (Input.touchCount > 1)
            {
                Touch touch1 = Input.GetTouch(1);
                if (touch1.position.x > Screen.width / 3 && touch1.position.y > Screen.height / 4.5f)
                {
                    if (touch1.phase == TouchPhase.Moved)
                    {
                        mouseX = touch1.deltaPosition.x * sensitivity * Time.deltaTime;
                        mouseY = touch1.deltaPosition.y * sensitivity * Time.deltaTime;
                    }
                }
                if (Input.touchCount > 2)
                {
                    Touch touch2 = Input.GetTouch(2);
                    if (touch2.position.x > Screen.width / 3 && touch2.position.y > Screen.height / 4.5f)
                    {
                        if (touch2.phase == TouchPhase.Moved)
                        {
                            mouseX = touch2.deltaPosition.x * sensitivity * Time.deltaTime;
                            mouseY = touch2.deltaPosition.y * sensitivity * Time.deltaTime;
                        }
                    }
                }
            }
        }

        // ������� ������ �����/����
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ������������ �������� �� ��� X

        transform.localRotation = Quaternion.Euler(xRotation * xMultiply, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); // ������� ���� ������ �����/������

    }



}

