using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void ExitGame()
    {
        Application.Quit();
    }

    void NewGame()
    {
        Application.LoadLevel(1);
    }
}
