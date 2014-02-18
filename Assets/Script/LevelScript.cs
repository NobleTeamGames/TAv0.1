using UnityEngine;
using System.Collections;

public  class LevelScript : MonoBehaviour {

    public static string Type = "Level";
    public static Grid MainGrid;
    public static float Difficalt;
	// Use this for initialization
    void Start()
    {
        Difficalt = 0.9f;
        GameObject GridObject = Instantiate(Resources.Load("Prefab/Grid", typeof(GameObject))) as GameObject;
        MainGrid = GridObject.GetComponent<Grid>();
        Debug.Log("Lvl");
        MainGrid.GenarateGrid(Type);
       
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
