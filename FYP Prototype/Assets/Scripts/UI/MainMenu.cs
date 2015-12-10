using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	
    void StartButtonOnClick ()
    {
        Application.LoadLevel (1);
    }

    void QuitButtonOnClick()
    {
        Application.Quit();
    }
}
