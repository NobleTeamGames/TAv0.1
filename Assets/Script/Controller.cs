using UnityEngine;
using System.Collections;
using System;

public class Controller : MonoBehaviour {
    private CameraController CameraControls; 
    private float StartClickTime;
    private Vector2 StartVec2;
    private bool NowClic;

    private LevelScript LS;

    public enum Action { Remove }
    public Action NowAction;
	// Use this for initialization
	void Start () {

        NowAction = Action.Remove;
        CameraControls = Camera.main.GetComponent<CameraController>();
        LS = this.gameObject.GetComponentInChildren<LevelScript>();
        NowClic = false;

	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            Debug.ClearDeveloperConsole();
            StartClickTime = Time.time;
            StartVec2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            NowClic = true;
        }

        if (NowClic == true)
        {
            if (Vector2.Distance(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), StartVec2) > 0.25f / (CameraControls.Zoom + 1))
                NowClic = false;

        }

        if (Input.GetMouseButtonUp(0) && Time.time - StartClickTime <= 0.15f && NowClic)
        {
            switch (NowAction)
            {
                case Action.Remove:                   
                    Debug.Log(Convert.ToInt16(StartVec2.y) + " " + (Convert.ToInt16(StartVec2.x) + 15));
                    LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x)+15].gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
            //Debug.Log(Convert.ToInt16(StartVec2.x) +" "+ Convert.ToInt16(StartVec2.y));
            NowClic = false;
        }
	}
}
