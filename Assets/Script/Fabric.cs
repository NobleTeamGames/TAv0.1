using UnityEngine;
using System.Collections;
using Mod = Assets.Model;
using System.Timers;
using System;
using Assets.Utilities;

public class Fabric : MonoBehaviour {

    public int SecondsWait;
    public int MaxHealth;
    public int MaxArmor;
    public int HeightGabarits = 2;
    public int WidhtGabarits = 3;
    public int ResourseZone=1;

    private Mod.Resource _plus = new Mod.Resource(5, 0, 0, 0, 0, 0, 0);
    private int _worker;
    private GamerStatistick _stat;
    private bool _builded = false;
    private bool NowClic;
    private float StartClickTime;
    private Timer _startTimer;
    private Mod.Resource _price = new Mod.Resource(0, 0, 100, 0, 0, 300, 0);
    private int _health;
    private int _armor;

	// Use this for initialization
    void Start()
    {
        _worker = 2;
        _stat = gameObject.transform.parent.gameObject.GetComponent<GamerStatistick>();
        _startTimer = new Timer(SecondsWait * 1000);
        _startTimer.Elapsed += (s, e) => { ReCalculate(); };
        _health = MaxHealth;
        _armor = MaxArmor;
    }

    void Update()
    {
        if (_builded)
            return;

        CreateBilding();

    }


    private void CreateBilding()
    {
        bool CanBild = true;
        var position = Input.mousePosition;
        var newPosition = new Vector3(Convert.ToInt16(Camera.main.ScreenToWorldPoint(position).x), Convert.ToInt16(Camera.main.ScreenToWorldPoint(position).y));
       /* var oldPosition = this.transform.position;
        if (oldPosition != newPosition)
        {*/
       // if(gameObject.transform.parent.transform.childCount>0)
        foreach (var Cild in gameObject.transform.parent.GetChild(0))
            {
               // Debug.Log((Cild as Transform).position);
                if ((Cild as Transform).position == gameObject.transform.position)
                {
                    CanBild = false;
                }
            }
        if(CanBild)
            for (int y = 0; y < HeightGabarits; y++)
            {
               // Debug.Log("1");
                for (int x = -1; x < WidhtGabarits-1; x++)
                {
                    //Проверь на верхний игрик
                    if (-newPosition.y - y >= 0 && -newPosition.y - y <= LevelScript.MainGrid.Height && newPosition.x + x + 15 >= 0 && newPosition.x + x + 15 < LevelScript.MainGrid.Widht)
                    {
                        //Debug.Log((Convert.ToInt16(-newPosition.y) - y) + "," + (Convert.ToInt16(newPosition.x) + x + 15));
                        if (LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-newPosition.y) - y, Convert.ToInt16(newPosition.x) + x + 15].NowType == Block.Type.Front || LevelScript.MainGrid.BlockGrid[Convert.ToInt16(-newPosition.y) - y, Convert.ToInt16(newPosition.x) + x + 15].NowType == Block.Type.Resource)
                        {
                            CanBild = false;
                            break;
                        }
                    }
                    else
                    {
                        CanBild = false;
                        break;
                    }
                    if (!CanBild)
                        break;
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
                _stat.MinusPrice(_price);
                _startTimer.Start();
                _builded = true;
                _plus.Multiply(_worker);
                NowClic = false;
                Debug.Log(CalculateResourse());
            }
            else
                Destroy(gameObject);
        }
        

    }

    private int CalculateResourse()
    {
        int ResCol=0;
        for (int y = -ResourseZone; y < HeightGabarits + ResourseZone; y++)
        {
            for (int x = -ResourseZone - (int)(WidhtGabarits / 2f); x <= ResourseZone + (int)(WidhtGabarits / 2f); x++)
            {
                if (-(gameObject.transform.position.y + y) <= LevelScript.MainGrid.Height && -(gameObject.transform.position.y + y) >= 0)
                {
                    if ((gameObject.transform.position.x + x) + 15 < LevelScript.MainGrid.Widht && (gameObject.transform.position.x + x) + 15 >= 0)
                    {
                        if (LevelScript.MainGrid.BlockGrid[-((int)gameObject.transform.position.y + y), ((int)gameObject.transform.position.x + x + 15)].NowType == Block.Type.Resource)
                            ResCol++;
                       // Debug.Log(-(gameObject.transform.position.y + y) + ";" + (gameObject.transform.position.x + x));
                       // if(
                    }
                }
            }
        }
        return ResCol;

    }
    void ReCalculate()
    {
        _stat.SetResources(_plus);
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
