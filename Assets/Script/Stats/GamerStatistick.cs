using UnityEngine;
using System.Collections;
using System.Threading;

//Singletone, dontdestroyonload
public class GamerStatistick : MonoBehaviour {

    private GameObject _gamer;
    private UILabel Money;
    private UILabel Eat;
    private UILabel Water;
    private UILabel Energy;
    private UILabel Fassils;
    private UILabel Wood;
    private UILabel Genetics;

    public struct ResList
    {
        public int Money;
        public int Eat;
        public int Water;
        public int Energy;
        public int Fassils;
        public int Wood;
        public int Genetics;

        public ResList(int _Money, int _Eat, int _Water, int _Energy, int _Fassils, int _Wood, int _Genetics)
        {
            Money = _Money;
            Eat = _Eat;
            Water = _Water;
            Energy = _Energy;
            Fassils = _Fassils;
            Wood = _Wood;
            Genetics = _Genetics;
        }

        public void SetRes(int _Money, int _Eat, int _Water, int _Energy, int _Fassils, int _Wood, int _Genetics)
        {
            Money = _Money;
            Eat = _Eat;
            Water = _Water;
            Energy = _Energy;
            Fassils = _Fassils;
            Wood = _Wood;
            Genetics = _Genetics;
            
        }

        public bool RemoveRes(int _Money, int _Eat, int _Water, int _Energy, int _Fassils, int _Wood, int _Genetics)
        {
            if (Money - _Money >= 0 && Eat - _Eat >= 0 && Water - _Water >= 0 && Energy - _Energy >= 0 && Fassils - _Fassils >= 0 && Wood - _Wood >= 0 && Genetics - _Genetics >= 0)
            {
                Money -= _Money;
                Eat -= _Eat;
                Water -= _Water;
                Energy -= _Energy;
                Fassils -= _Fassils;
                Wood -= _Wood;
                Genetics -= _Genetics;
                return true;
            }
            else
                return false;
        }

        public void AddRes(int _Money, int _Eat, int _Water, int _Energy, int _Fassils, int _Wood, int _Genetics)
        {
            Money += _Money;
            Eat += _Eat;
            Water += _Water;
            Energy += _Energy;
            Fassils += _Fassils;
            Wood += _Wood;
            Genetics += _Genetics;
        }
    }

    private ResList Stats;
	// Use this for initialization
	void Start () {
        Stats = new ResList(100, 200, 300, 400, 500, 600, 0);
      //  _resources = new Mod.Resource();
        //Money = GameObject.Find("UI Root/Stats/Labels/Money").GetComponent<UILabel>();
        Money = GameObject.Find("Money").GetComponent<UILabel>();
        Eat = GameObject.Find("Eat").GetComponent<UILabel>();
        Water = GameObject.Find("Water").GetComponent<UILabel>();
        Energy = GameObject.Find("Energy").GetComponent<UILabel>();
        Fassils = GameObject.Find("Fassils").GetComponent<UILabel>();
        Wood = GameObject.Find("Wood").GetComponent<UILabel>();
        Genetics = GameObject.Find("Genetics").GetComponent<UILabel>();
        UpdateRes();
	}
	
	// Update is called once per frame
	void Update () {

	}

   
    /*public void SetResources(Mod.Resource resource)
    {
       // _resources.Inc(resource);
    }

    public void MinusPrice(Mod.Resource price)
    {
       // _resources.Dec(price);
    }*/

    private void UpdateRes()
    {
        Money.text = Stats.Money.ToString();
        Eat.text = Stats.Eat.ToString();
        Water.text = Stats.Water.ToString();
        Energy.text = Stats.Energy.ToString();
        Fassils.text = Stats.Fassils.ToString();
        Wood.text = Stats.Wood.ToString();
        Genetics.text = Stats.Genetics.ToString();
    }
}
