using UnityEngine;
using System.Collections;

public class firetowerdemo : MonoBehaviour {
	void Update () {
        this.gameObject.transform.Rotate(0, Time.deltaTime * 14.0f, 0);
	}
}
