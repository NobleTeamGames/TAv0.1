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

    private GameObject BildingObject;
    private Camera UICam;
    public enum Action { Nothing, SetWall, Remove, Bilding }
    public Action NowAction;
	// Use this for initialization
	void Start () {
        Frame = Instantiate(Resources.Load("Prefab/Frame"), Vector3.zero, Quaternion.identity) as GameObject;
        Frame.transform.parent = gameObject.transform;
        Frame.SetActive(false);

        CameraControls = Camera.main.GetComponent<CameraController>();
        LS = this.gameObject.GetComponentInChildren<LevelScript>();
        NowClic = false;
        UICam = NGUITools.FindCameraForLayer(8);
       // Debug.Log(UICam.name);
	}
	
	// Update is called once per frame
	void Update () {


       Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;
 
       // Raycast
      
       if (Physics.Raycast(ray,out hit))
       {
           //Debug.DrawRay(ray.origin, ray.direction);
           Debug.Log(hit.collider.gameObject.layer);
           Destroy(hit.collider.gameObject);
           /* if (hit.transform.gameObject.layer == LayerGround) {
             Debug.Log("Ground");
             // Make a path
            } else {
             Debug.Log("Other Objects");
             // Do whatever you want*/
       }

        if (NowAction == Action.Bilding)
        {
            if(Frame.active)
                Frame.SetActive(false);

            if (BildingObject == null || BildingObject.GetComponent<Sawmill>()._builded)
            {
                NowAction = Action.Nothing;
                BildingObject = null;
            }

            
        }


            

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


   /* void OnGUI()
    {
        if (GUI.Button(new Rect(10, 30, 150, 20), "Удалить блок"))
        {
            if (NowAction == Action.Remove)
                NowAction = Action.Nothing;
            else
            {
                if (NowAction == Action.SetWall)
                    Frame.SetActive(false);
                NowAction = Action.Remove;
            }
        }

        if (GUI.Button(new Rect(160, 30, 150, 20), "Установить стену"))
        {
            if (NowAction == Action.SetWall)
            {
                Frame.SetActive(false);
                NowAction = Action.Nothing;
            }
            else
                NowAction = Action.SetWall;
        }

        if (GUI.Button(new Rect(310, 30, 150, 20), "Добавить Здание"))
        {
            if (BildingObject == null)
            {
                NowAction = Action.Bilding;
                BildingObject = Instantiate(Resources.Load("Prefab/SawMill Fabric")) as GameObject;
                BildingObject.name = "Sawmill";
                BildingObject.transform.parent = GameObject.Find("Gamer").transform;
            }
            else
            {
                Destroy(BildingObject);
                BildingObject = null;
            }
        }
    
    }*/

    private void Remove()
    {
         if (Input.GetMouseButtonDown(0))
                {
                    //Debug.ClearDeveloperConsole();
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
                    //Debug.Log(Convert.ToInt16(-StartVec2.y) + ":::" + Convert.ToInt16(StartVec2.x));
                    if (Convert.ToInt16(-StartVec2.y) <= LevelScript.MainGrid.Height && Convert.ToInt16(-StartVec2.y) >= 0)
                    {
                        if (Convert.ToInt16(StartVec2.x)+15 < LevelScript.MainGrid.Widht && Convert.ToInt16(StartVec2.x)+15 >= 0)
                        {
                            //LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15].BlocksArownd(new Vector2(Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15));
                            LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15].DestroyBlock();
                            //LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15].ChangeWall();
                       }
                    }


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
        {
            //Frame.transform.position = Vector3.Lerp(Frame.transform.position, new Vector3(SetWallVect.x, SetWallVect.y, 0), 10f);
            Frame.transform.position = new Vector3(SetWallVect.x, SetWallVect.y, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
           // Debug.ClearDeveloperConsole();
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
                //LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15].SetWall();
                LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-StartVec2.y), Convert.ToInt16(StartVec2.x) + 15].ChangeWall();


            //Debug.Log(Convert.ToInt16(StartVec2.x) +" "+ Convert.ToInt16(StartVec2.y));
            NowClic = false;

        }

    }

    public void Exit()
    {
        Debug.Log(this.gameObject.name);
        Application.Quit();
    }

    public void OnButtonClick(GameObject itemGO)
    {
        Debug.Log(itemGO.name);
    }

    public void SetRemove()
    {
        if (NowAction == Action.Remove)
            NowAction = Action.Nothing;
        else
        {
            if (NowAction == Action.SetWall)
                Frame.SetActive(false);
            NowAction = Action.Remove;
        }
    }

    public void SetWalls()
    {
        if (NowAction == Action.SetWall)
        {
            Frame.SetActive(false);
            NowAction = Action.Nothing;
        }
        else
            NowAction = Action.SetWall;
    }
}