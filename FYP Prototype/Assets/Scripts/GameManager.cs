using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameObject endPoint;
    public static GameObject player;

	void Awake ()
    {
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
        player = GameObject.FindGameObjectWithTag("Player");
	}
}
