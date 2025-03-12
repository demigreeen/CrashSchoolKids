using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float sensitivity = 100f; // ���������������� ����
    public float xMultiply = 1f;
    public Transform playerBody; // ���� ������, � �������� ����� ����������� ������

    private float xRotation = 0f; // ���� �������� �� ��� X

    float mouseX;
        float mouseY;

    public void Rotate()
    {
        if (RBDeviceType.isMobile() == false)
        {
            // �������� ���� � ����
            mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            // ������� ������ �����/����
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ������������ �������� �� ��� X

            transform.localRotation = Quaternion.Euler(xRotation * xMultiply, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX); // ������� ���� ������ �����/������
        }
    }
}

