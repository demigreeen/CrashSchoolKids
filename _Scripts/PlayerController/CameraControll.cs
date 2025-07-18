using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float sensitivity = 100f; // Чувствительность мыши
    public float xMultiply = 1f;
    public Transform playerBody; // Тело игрока, к которому будет прикреплена камера

    private float xRotation = 0f; // Угол поворота по оси X

    float mouseX;
        float mouseY;

    public void Rotate()
    {
        if (RBDeviceType.isMobile() == false)
        {
            // Получаем ввод с мыши
            mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            // Вращаем камеру вверх/вниз
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем вращение по оси X

            transform.localRotation = Quaternion.Euler(xRotation * xMultiply, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX); // Вращаем тело игрока влево/вправо
        }
    }
}

