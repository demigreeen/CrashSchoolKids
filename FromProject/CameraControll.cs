using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public float sensitivity = 100f; // Чувствительность мыши
    public float sensitivityPanelRotate = 1; // Чувствительность мыши
    public float xMultiply = 1f;
    public Transform playerBody; // Тело игрока, к которому будет прикреплена камера
    public CameraControllerPanel cameraControllerPanel;

    private float xRotation = 0f; // Угол поворота по оси X


    public void Rotate()
    {
        float mouseX = 0;
        float mouseY = 0;

        if (RBDeviceType.MyIsMobile() == false)
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
        else
        {
            if (cameraControllerPanel.pressed)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.fingerId == cameraControllerPanel.fingerId)
                    {
                        if (touch.phase == TouchPhase.Moved)
                        {
                            mouseY = touch.deltaPosition.y * sensitivityPanelRotate;
                            mouseX = touch.deltaPosition.x * sensitivityPanelRotate;
                        }

                        if (touch.phase == TouchPhase.Stationary)
                        {
                            mouseY = 0;
                            mouseX = 0;
                        }
                    }
                }
            }

            // Вращаем персонажа в горизонтальной плоскости
            playerBody.Rotate(Vector3.up * -mouseX * sensitivityPanelRotate);

            // Вращаем камеру в вертикальной плоскости
            xRotation -= mouseY * sensitivityPanelRotate;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(-xRotation, 0.0f, 0.0f);
        }

    }
}


