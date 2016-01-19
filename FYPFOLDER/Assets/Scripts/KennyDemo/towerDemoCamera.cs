using UnityEngine;
using System.Collections;

public class towerDemoCamera : MonoBehaviour {

    private Vector3[] locations;

	// Use this for initialization
	void Start () {
        locations = new Vector3[5];

        locations[0] = new Vector3(-72, 9.5f, 17);
        locations[1] = new Vector3(-43, 9.9f, 14.8f);
        locations[2] = new Vector3(-13.9f, 6.8f, 6.4f);
        locations[3] = new Vector3(22.4f, 7.3f, 8.9f);
        locations[4] = new Vector3(45.4f, 6.5f, 13.9f);

        this.gameObject.transform.position = locations[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.gameObject.transform.position = locations[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.gameObject.transform.position = locations[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.gameObject.transform.position = locations[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.gameObject.transform.position = locations[3];
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.gameObject.transform.position = locations[4];
        }
	}
}
