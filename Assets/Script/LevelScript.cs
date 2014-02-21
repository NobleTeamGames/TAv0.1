<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;

public  class LevelScript : MonoBehaviour {

    public static string Type = "Level";
    public static Grid MainGrid;
    public float Difficalt;
	// Use this for initialization
    void Start()
    {
        Difficalt = 0.9f;
        GameObject GridObject = Instantiate(Resources.Load("Prefab/Grid", typeof(GameObject))) as GameObject;
        MainGrid = GridObject.GetComponent<Grid>();
        MainGrid.GenarateGrid(Type, Difficalt);
       
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
=======
﻿using UnityEngine;
using System.Collections;

public  class LevelScript : MonoBehaviour {

    public static string Type = "Level";
    public static Grid MainGrid;
    public float Difficult;
	// Use this for initialization
    void Start()
    {
        GameObject GridObject = Instantiate(Resources.Load("Prefab/Grid", typeof(GameObject))) as GameObject;
        MainGrid = GridObject.GetComponent<Grid>();
        //Debug.Log("Lvl");
        MainGrid.GenarateGrid(Type, Difficult);
       
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
>>>>>>> a5951c37d21f8da47a75245de54e720ec3040c2d
