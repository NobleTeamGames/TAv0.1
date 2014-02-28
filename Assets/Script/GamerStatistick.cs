using UnityEngine;
using System.Collections;
using Mod = Assets.Model;
using System.Threading;
using Assets.Utilities;

public class GamerStatistick : MonoBehaviour {

    private Mod.Resource _resources;
    private GameObject _gamer;




	// Use this for initialization
	void Start () {
        _resources = new Mod.Resource();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10,10,70,20),_resources.Money.ToString());
        GUI.Label(new Rect(110, 10, 70, 20), _resources.Eat.ToString());
        GUI.Label(new Rect(210, 10, 70, 20), _resources.Water.ToString());
        GUI.Label(new Rect(310, 10, 70, 20), _resources.Energy.ToString());
        GUI.Label(new Rect(410, 10, 70, 20), _resources.Fassils.ToString());
        GUI.Label(new Rect(510, 10, 70, 20), _resources.Wood.ToString());
        GUI.Label(new Rect(610, 10, 70, 20), _resources.Genetics.ToString());


        if (GUI.Button(new Rect(10, 30, 150, 20), "Добавить Здание"))
        {
            GameObject child = Instantiate(Resources.Load("Prefab/WaterFabric")) as GameObject;
            child.name = "WaterFabric";
            child.transform.parent = this.transform;
        }
    }

    public void SetResources(Mod.Resource resource)
    {
        _resources.Inc(resource);
    }

    public void MinusPrice(Mod.Resource price)
    {
        _resources.Dec(price);
    }
}
