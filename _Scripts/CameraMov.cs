using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public float cameraSensitivity;

    int leftFingerID, rightFingerID;
    float halfScreenWidth; float halfScreenHeight; public Transform playerBody;

    Vector2 lookInput;
    float cameraPitch;

    void Start()
    {
        if (RBDeviceType.MyIsMobile() == false)
        {
            enabled = false;
        }

        leftFingerID = -1;
        rightFingerID = -1;

        halfScreenWidth = Screen.width / 2.8f;
        halfScreenHeight = Screen.height / 3.9f;
    }

    void Update()
    {
        GetTouchInput();

        if (rightFingerID != -1)
        {
            LookAround();
        }
    }

    void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerID == -1)
                    {
                        leftFingerID = t.fingerId;
                    }
                    else if (t.position.x > halfScreenWidth && t.position.y > halfScreenHeight && rightFingerID == -1)
                    {
                        rightFingerID = t.fingerId;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerID)
                    {
                        leftFingerID = -1;
                    }
                    else if (t.fingerId == rightFingerID)
                    {
                        rightFingerID = -1;
                    }
                    break;

                case TouchPhase.Moved:

                    if (t.fingerId == rightFingerID)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    break;

                case TouchPhase.Stationary:

                    if (t.fingerId == rightFingerID)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {

        // Rotate the camera around the X-axis (up and down)
        cameraPitch -= lookInput.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f); // Clamp the vertical rotation

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(-cameraPitch, 0f, 0f);

        // Rotate the player body around the Y-axis (left and right)
        playerBody.Rotate(Vector3.up * -lookInput.x);
    }
}