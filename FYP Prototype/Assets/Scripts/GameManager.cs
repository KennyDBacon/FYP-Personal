using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static Unit endPoint;
    public static Unit player;

	void Awake ()
    {
        endPoint = GameObject.FindGameObjectWithTag("EndPoint").GetComponent<Unit>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
	}
}
