using UnityEngine;
using System.Collections;

public class towerDemoCamera : MonoBehaviour {

    private Vector3[] locations;

	// Use this for initialization
	void Start () {
        locations = new Vector3[5];

        locations[0] = new Vector3(-72, 7, 11);
        locations[1] = new Vector3(-42, 7, 11);
        locations[2] = new Vector3(-14, 5, 4.4f);
        locations[3] = new Vector3(23.5f, 5.8f, 7.9f);
        locations[4] = new Vector3(44.6f, 4.5f, 8.4f);

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
