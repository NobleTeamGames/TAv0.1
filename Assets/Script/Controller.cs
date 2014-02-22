using UnityEngine;
using System.Collections;
using System;

public class Controller : MonoBehaviour {
    private CameraController CameraControls; 
    private float StartClickTime;
    private Vector2 StartVec2;
    private Vector2 SetWallVect;
    private bool NowClic;
    private GameObject Frame;
    private LevelScript LS;

    public enum Action { Nothing, SetWall, Remove }
    public Action NowAction;
	// Use this for initialization
	void Start () {
        Frame = Instantiate(Resources.Load("Prefab/Frame"), Vector3.zero, Quaternion.identity) as GameObject;
        Frame.transform.parent = gameObject.transform;
        Frame.SetActive(false);

        CameraControls = Camera.main.GetComponent<CameraController>();
        LS = this.gameObject.GetComponentInChildren<LevelScript>();
        NowClic = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            if (NowAction == Action.Remove)
                NowAction = Action.Nothing;
            else
            {
                if (NowAction == Action.SetWall)
                    Frame.SetActive(false);
                NowAction = Action.Remove;
            }

        if(Input.GetKeyDown(KeyCode.W))
            if (NowAction == Action.SetWall)
            {
                Frame.SetActive(false);
                NowAction = Action.Nothing;
            }
            else
                NowAction = Action.SetWall;

        switch (NowAction)
        {
            case Action.Remove:
               Remove();
                break;
            case Action.SetWall:
                SetWall();
                break;
            default:
                break;
        }
    
	}

    private void Remove()
    {
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

                    if (Convert.ToInt16(-StartVec2.y) <= LevelScript.MainGrid.Height && Convert.ToInt16(-StartVec2.y) >= 0)
                        LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15].DestroyBlock();


                    //Debug.Log(Convert.ToInt16(StartVec2.x) +" "+ Convert.ToInt16(StartVec2.y));
                    NowClic = false;

                }
    }

    private void SetWall()
    {
        SetWallVect = new Vector2(Convert.ToInt16(Camera.main.ScreenToWorldPoint(Input.mousePosition).x), Convert.ToInt16(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
        if(!Frame.active)
            Frame.SetActive(true);
        if (new Vector2(Frame.transform.position.x, Frame.transform.position.y) != SetWallVect)
            Frame.transform.position = new Vector3(SetWallVect.x, SetWallVect.y, 0);

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

            if (Convert.ToInt16(-StartVec2.y) <= LevelScript.MainGrid.Height && Convert.ToInt16(-StartVec2.y) >= 0)
                LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15].ChangeWall();


            //Debug.Log(Convert.ToInt16(StartVec2.x) +" "+ Convert.ToInt16(StartVec2.y));
            NowClic = false;

        }

    }
}