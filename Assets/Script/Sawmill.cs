using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class Sawmill : MonoBehaviour {
    public int SecondsWait=2;
    public int MaxHealth;
    public int MaxArmor;
    public int HeightGabarits = 2;
    public int WidhtGabarits = 3;
    public int ResourseZone = 1;
    public string FoundingResourse;

    private int ResourseArownd;
    private int _worker;
    private GamerStatistick _stat;
    public bool _builded = false;
    private bool NowClic;
    private float StartClickTime;
    private Timer _startTimer;
    
    private int _health;
    private int _armor;
	// Use this for initialization
	void Start () {
        _worker = 2;
        _stat = gameObject.transform.parent.gameObject.GetComponent<GamerStatistick>();
        _startTimer = new Timer(SecondsWait * 1000);
        _startTimer.Elapsed += (s, e) => { ReCalculate(); };
        _health = MaxHealth;
        _armor = MaxArmor;
	}
	
	// Update is called once per frame
	void Update () {
        if (_builded)
            return;
        else
            CreateBilding();
	}


    private void CreateBilding()
    {
        bool CanBild = true;
        var position = Input.mousePosition;
        var newPosition = new Vector3(Convert.ToInt16(Camera.main.ScreenToWorldPoint(position).x), Convert.ToInt16(Camera.main.ScreenToWorldPoint(position).y));

       
        if (newPosition.y != 1)
            CanBild = false;
        //Debug.Log(CanBild);

        if (CanBild)
        {
            for (int i = 0; i < LevelScript.MainGrid.TreesGrid.Length; i++)
            {
                //Debug.Log(newPosition + "As " + LevelScript.MainGrid.TreesGrid[i].transform.position);
                if (LevelScript.MainGrid.TreesGrid[i].transform.position == gameObject.transform.position)
                {
                    CanBild = true;
                    break;
                }
                else
                    CanBild = false;
            }
        }
       

        if (CanBild)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 1f, 0.3f);
        else
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.1f, 0.1f);
        this.transform.position = newPosition;
        // }

        if (Input.GetMouseButtonDown(0))
        {
            // Debug.ClearDeveloperConsole();
            StartClickTime = Time.time;
            var StartVec2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            NowClic = true;
        }

        if (Input.GetKey(KeyCode.Escape))
            Destroy(gameObject);


        if (Input.GetMouseButtonUp(0) && Time.time - StartClickTime <= 0.15f && NowClic)
        {
            if (CanBild)
            {
                this.gameObject.transform.parent = this.gameObject.transform.parent.GetChild(0);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
               // _stat.MinusPrice(_price);
              //  _startTimer.Start();
                _builded = true;
             //   _plus.Multiply(_worker);
                NowClic = false;         

            }
            else
                Destroy(gameObject);
        }


    }

    void ReCalculate()
    {
      //  _stat.SetResources(_plus);
    }

    public void AddHealth(int heal)
    {
        if (MaxHealth - _health > heal)
            _health += heal;
        else
            _health = MaxHealth;
    }

    public void AddArmor(int armor)
    {
        if (MaxArmor - _armor > armor)
            _armor += armor;
        else
            _armor = MaxArmor;
    }

    public void RemHealth(int damage)
    {
        if (_armor > 0)
            RemArmor(damage);
        else if (_health > damage)
            _health -= damage;
        else
            _health = 0;

    }
    public void RemArmor(int damage)
    {
        if (_armor > damage)
            _armor -= damage;
        else
        {
            damage -= _armor;
            _armor = 0;
            RemHealth(damage);
        }
    }
}
