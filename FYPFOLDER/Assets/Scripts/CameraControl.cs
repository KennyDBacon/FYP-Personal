using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    private Vector3 upperCamDefaultPos = new Vector3(-0.5f, 50.0f, -0.5f);
    private Vector3 moveToPos;
    private Vector2 maxDeviation;

    void Awake ()
    {
        moveToPos = upperCamDefaultPos;
        CalculateMaxDeviation(50.0f);
    }
    
    void OnEnable ()
    {
        transform.position = upperCamDefaultPos;
        moveToPos = upperCamDefaultPos;
        CalculateMaxDeviation(50.0f);
    }

    void Update ()
    {
        if (Input.mouseScrollDelta.y != 0.0f)
        {
            moveToPos.y -= Input.mouseScrollDelta.y;
            if (moveToPos.y > 50.0f)
            {
                moveToPos.y = 50.0f;
            }
            else if (moveToPos.y < 15.0f)
            {
                moveToPos.y = 15.0f;
            }
            CalculateMaxDeviation(moveToPos.y);
        }
        moveToPos.x += Time.deltaTime * 25.0f * (1.0f/Time.timeScale) * (int)(Input.mousePosition.x / (Screen.width - 1) * 2 - 1);
        moveToPos.z += Time.deltaTime * 25.0f * (1.0f/Time.timeScale) * (int)(Input.mousePosition.y / (Screen.height - 1) * 2 - 1);
        if (moveToPos.x > upperCamDefaultPos.x + maxDeviation.x)
        {
            moveToPos.x = upperCamDefaultPos.x + maxDeviation.x;
        }
        else if (moveToPos.x < upperCamDefaultPos.x - maxDeviation.x)
        {
            moveToPos.x = upperCamDefaultPos.x - maxDeviation.x;
        }
        if (moveToPos.z > upperCamDefaultPos.z + maxDeviation.y)
        {
            moveToPos.z = upperCamDefaultPos.z + maxDeviation.y;
        }
        else if (moveToPos.z < upperCamDefaultPos.z - maxDeviation.y)
        {
            moveToPos.z = upperCamDefaultPos.z - maxDeviation.y;
        }
        transform.position = Vector3.Lerp(transform.position, moveToPos, Time.deltaTime * 10.0f * (1.0f / Time.timeScale));
    }

    private void CalculateMaxDeviation (float camHeight)
    {
        maxDeviation.x = (camHeight - 83.48f) * (-23.0f / 35.0f);
        maxDeviation.y = (camHeight - 82.375f) * (-20.0f / 35.0f);
    }
}