using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    //FPS Camera
    private GameObject playerCam;

    //Topdown Camera
    private GameObject upperCam;
    private Vector3 upperCamDefaultPos;
    private Vector3 moveToPos;
    private float maxDeviation = (50 - 15) * (-3.0f/6.0f) + 25;

    public GameObject TowerTest;

    void Start()
    {
        playerCam = Camera.main.gameObject;

        upperCam = GameObject.FindGameObjectWithTag("SubCamera");
        upperCamDefaultPos = new Vector3(30, 50, 30);
        moveToPos = upperCamDefaultPos;
        upperCam.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            playerCam.SetActive(!playerCam.activeSelf);
            upperCam.SetActive(!upperCam.activeSelf);
            if (upperCam.activeSelf)
            {
                upperCam.transform.position = upperCamDefaultPos;
                moveToPos = upperCamDefaultPos;
            }
        }
        if (upperCam.activeSelf)
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
                maxDeviation = (moveToPos.y - 15) * (-3.0f/6.0f) + 25;
            }
            moveToPos.x += Time.deltaTime * 25.0f * (int)(Input.mousePosition.x / Screen.width * 2 - 1);
            moveToPos.z += Time.deltaTime * 25.0f * (int)(Input.mousePosition.y / Screen.height * 2 - 1);
            if (moveToPos.x > 30 + maxDeviation)
            {
                moveToPos.x = 30 + maxDeviation;
            }
            else if (moveToPos.x < 30 - maxDeviation)
            {
                moveToPos.x = 30 - maxDeviation;
            }
            if (moveToPos.z > 30 + maxDeviation)
            {
                moveToPos.z = 30 + maxDeviation;
            }
            else if (moveToPos.z < 30 - maxDeviation)
            {
                moveToPos.z = 30 - maxDeviation;
            }
            upperCam.transform.position = Vector3.Lerp(upperCam.transform.position, moveToPos, Time.deltaTime * 10.0f);
        }
    }

    public GameObject UpperCam
    {
        get { return upperCam; }
    }
}