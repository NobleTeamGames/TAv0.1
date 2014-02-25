using UnityEngine;
using System.Collections;
using Mod = Assets.Model;
using System.Timers;
using System;
using Assets.Utilities;

public class Fabric : MonoBehaviour {

    public int SecondsWait;

    private Mod.Resource _plus = new Mod.Resource(5, 0, 0, 0, 0, 0, 0);
    private int _worker;
    private GamerStatistick _stat;
    private bool _builded = false;
    private bool NowClic;
    private float StartClickTime;
    private Timer _startTimer;
    private Mod.Resource _price = new Mod.Resource(0, 0, 100, 0, 0, 300, 0);

	// Use this for initialization
    void Start()
    {
        _worker = 2;
        _stat = gameObject.transform.parent.gameObject.GetComponent<GamerStatistick>();
        _startTimer = new Timer(SecondsWait * 1000);
        _startTimer.Elapsed += (s, e) => { ReCalculate(); };
    }

    void Update()
    {
        if (_builded)
            return;
        var position = Input.mousePosition;
        var newPosition = new Vector3(Convert.ToInt16(Camera.main.ScreenToWorldPoint(position).x), Convert.ToInt16(Camera.main.ScreenToWorldPoint(position).y));
        var oldPosition = this.transform.position;
        if (oldPosition != newPosition)
            this.transform.position = newPosition;
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
            _stat.MinusPrice(_price);
            _startTimer.Start();
            _builded = true;
            _plus.Multiply(_worker);
            NowClic = false;
        }
    }
    void ReCalculate()
    {
        _stat.SetResources(_plus);
    }
}
