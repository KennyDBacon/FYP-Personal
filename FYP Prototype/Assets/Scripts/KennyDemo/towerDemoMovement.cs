using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class towerDemoMovement : MonoBehaviour {

    public List<GameObject> destinations;
    private int index;
    public float speed;

    public int mode;

	void Start () {
        //destinations = new List<GameObject>();
        index = 0;
        this.gameObject.GetComponent<Animator>().SetBool("Walk", true);
	}

    void Update()
    {
        if (mode == 0)
        {
            speed = this.gameObject.GetComponent<NavMeshAgent>().speed;
            this.gameObject.transform.LookAt(destinations[index].transform);
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, destinations[index].transform.position, Time.deltaTime * speed);

            if (Vector3.Distance(this.gameObject.transform.position, destinations[index].transform.position) <= 0.5f)
            {
                index++;

                if (index >= destinations.Count)
                {
                    index = 0;
                }
            }
        }
        else if (mode == 1)
        {
            this.gameObject.transform.LookAt(destinations[0].transform);
            this.gameObject.transform.position = destinations[0].transform.position;
        }
	}
}
