using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Text.RegularExpressions;

public class Grid : MonoBehaviour
{


    public int Height;
    public int Widht;
    public Block[,] BlockGrid;
    public Resource[] TreesGrid;



    private GameObject Background;

    // Use this for initialization
    void Start()
    {
        Background = Instantiate(Resources.Load("Prefab/Forest"), new Vector3(0, 2.5f, 0), Quaternion.identity) as GameObject;     
    }

    // Update is called once per frame
    void Update()
    {
        Background.transform.position = new Vector3(Camera.main.transform.position.x, 2.5f, 0f);
    }

    public void GenarateGrid(string Type ,float Difficalt)
    {

        float Rand = 0f;                                        //Рандомная величина
        GameObject Now;                                        //Переменная для поиска спаунящегося блока

        

        TextAsset xmlAsset = Resources.Load("LevelStats/" + Type) as TextAsset; //Считываня xml
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlAsset)
            xmlDoc.LoadXml(xmlAsset.text);

        XmlElement Stats = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("Location");
        Height = Convert.ToInt16(Stats.GetAttribute("height"));
        Widht = Convert.ToInt16(Stats.GetAttribute("width"));

        List<Vector2> TreesCoords = new List<Vector2>();
        int IndentTree = Convert.ToInt16(xmlDoc.DocumentElement.SelectSingleNode("//Tree").Attributes[0].Value);

        BlockGrid = new Block[Height + 2, Widht];
       // BlockGrid[0, 30] = new Block();
        

        XmlElement Lvl = xmlDoc.GetElementById("1");
        for (int i = -1; i <= Height; i++)
        {
            for (int x = -Widht / 2; x <= Widht / 2 - 1; x++)
            {
                Rand = UnityEngine.Random.value;
                switch (i)
                {
                    case -1:
                        if (UnityEngine.Random.value < Convert.ToSingle(xmlDoc.DocumentElement.SelectSingleNode("//Tree").Attributes[1].Value))
                        {

                            if (x <= 0)
                            {
                                if (TreesCoords.Count > 0)
                                {
                                    if (TreesCoords[TreesCoords.Count - 1].x + IndentTree <= x)
                                        TreesCoords.Add(new Vector2(x, 1));
                                }
                                else
                                    TreesCoords.Add(new Vector2(x, 1));
                            }
                            else
                            {
                                if (UnityEngine.Random.value < 0.5f)
                                    if (TreesCoords.Count > 0)
                                    {
                                        if (TreesCoords[TreesCoords.Count - 1].x + IndentTree <= x)
                                            TreesCoords.Add(new Vector2(x, 1));
                                    }
                                    else
                                        TreesCoords.Add(new Vector2(x, 1));
                            }
                                


                        }
                       
                        break;


                    case 0:
                        Now = Instantiate(Resources.Load("Prefab/GroundUp"), new Vector3(x, -i, 0), Quaternion.identity) as GameObject;
                        BlockGrid[i, x + 15] = Now.GetComponent<Ground>();
                        BlockGrid[i, (x + 15)].gameObject.transform.parent = this.gameObject.transform;
                        break;

                    default:
                        if (UnityEngine.Random.value <= Difficalt)
                        {
                            if (!BlockGrid[i, x + 15])
                            {
                                Now = Instantiate(Resources.Load("Prefab/Ground"), new Vector3(x, -i, 0), Quaternion.identity) as GameObject;
                                BlockGrid[i, x + 15] = Now.GetComponent<Ground>();
                                BlockGrid[i, (x + 15)].gameObject.transform.parent = this.gameObject.transform;
                            }
                        }
                        else
                        {
                            bool Spawn = false;
                            XmlNodeList dataList = xmlDoc.GetElementsByTagName("level")[0].ChildNodes;
                            for (int j = 0; j < dataList.Count; j++)
                            {
                                string[] substrings = Regex.Split(dataList[j].Attributes[1].Value, "(-)");
                                if (i >= Convert.ToInt16(substrings[0]) && i <= Convert.ToInt16(substrings[2]))
                                {
                                    Rand = UnityEngine.Random.value;
                                    if (Rand <= Convert.ToSingle(dataList[j].Attributes[0].Value) && !BlockGrid[i, x + 15])
                                    {
                                        if (!BlockGrid[i, x + 15])
                                            GenerateResourse(i, x, dataList[j].Attributes[2].Value, dataList[j].Name);
                                        Spawn = true;
                                        break;
                                    }
                                }
                            }
                            if (!Spawn)
                            {
                                if (!BlockGrid[i, x + 15])
                                {
                                    Now = Instantiate(Resources.Load("Prefab/Ground"), new Vector3(x, -i, 0), Quaternion.identity) as GameObject;
                                    BlockGrid[i, x + 15] = Now.GetComponent<Ground>();
                                    BlockGrid[i, (x + 15)].gameObject.transform.parent = this.gameObject.transform;
                                }
                            }
                        }
                        break;
                }

            }
            GenerateTree(TreesCoords);
            GenerateZone();

        }

        
    }

    private void GenerateResourse(int _y, int _x, string _Size, string _Name)
    {
        bool NoWay = false;
        GameObject Now;
        int Size = 0;
        string[] substrings = Regex.Split(_Size, "(,)");
        if (UnityEngine.Random.value <= 0.5)
            Size = Convert.ToInt16(substrings[0]);
        else
            Size = Convert.ToInt16(substrings[2]);

        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (y + _y <= Height && _x + x + 15 < Widht)
                {
                   
                    if (BlockGrid[y + _y, _x + x + 15])
                    {
                        NoWay = true;
                        CleanResourse(Size, _y, _x, y, x);
                        break;
                    }
                    else
                    {                      
                        Now = Instantiate(Resources.Load("Prefab/" + _Name), new Vector3(_x + x, -(y + _y), 0), Quaternion.identity) as GameObject;
                        BlockGrid[y + _y, _x + x + 15] = Now.GetComponent<Resource>();
                        BlockGrid[y + _y, _x + x + 15].gameObject.transform.parent = this.gameObject.transform;
                    }
                }
                else
                {
                    //Debug.Log((y + _y) + " <=" + Height);
                    NoWay = true;
                    ClenResoursesOverZone(Size, _y, _x, y, x);
                    break;
                }
            }

            if (NoWay)
                break;
        }

       


    }

    private void CleanResourse(int Size, int _y, int _x, int _yEnd, int _xEnd)
    {
        for (int y = 0; y <= _yEnd; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (y <= _yEnd && x < _xEnd)
                {
                    if (BlockGrid[y + _y, _x + x + 15])
                    {
                        //Debug.Log("del");
                        Destroy(BlockGrid[y + _y, _x + x + 15].gameObject);
                        GameObject Now = Instantiate(Resources.Load("Prefab/Ground"), new Vector3(_x + x, -(y + _y), 0), Quaternion.identity) as GameObject;
                        BlockGrid[y + _y, _x + x + 15] = Now.GetComponent<Ground>();
                        BlockGrid[y + _y, _x + x + 15].gameObject.transform.parent = this.gameObject.transform;
                    }                   
                    //if (BlockGrid[y + _y, _x + x + 15])
                    //    Destroy(BlockGrid[y + _y, _x + x + 15].gameObject);
                    // BlockGrid[y + _y, _x + x + 15].gameObject.SetActive(false);

                    /*Now = Instantiate(Resources.Load("Prefab/Ground"), new Vector3(_x + x, -(y + _y), 0), Quaternion.identity) as GameObject;
                    BlockGrid[y + _y, _x + x + 15] = Now.GetComponent<Ground>();
                    BlockGrid[y + _y, _x + x + 15].gameObject.transform.parent = this.gameObject.transform;*/
                }
                //else
                   // Debug.Log("Notdel");
            }
        }
        //sDebug.Log("2");
    }

    private void ClenResoursesOverZone(int Size, int _y, int _x, int _yEnd, int _xEnd)
    {
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
               // Debug.Log((y + _y) + ">=" + Height + "-" + (_x + x + 15) + ">=" + Widht);
                if (y + _y > Height || _x + x + 15 >= Widht)
                {
                    //Debug.Log((y + _y) + ">=" + Height + "-" + (_x + x + 15) + ">=" + Widht);  || x + 15 >= Widht
                   // Debug.Log("Out");
                    //   if (BlockGrid[y + _y, _x + x + 15])
                    //  {
                    //Debug.Log("del");
                    //      Destroy(BlockGrid[y + _y, _x + x + 15].gameObject);
                    //GameObject Now = Instantiate(Resources.Load("Prefab/Ground"), new Vector3(_x + x, -(y + _y), 0), Quaternion.identity) as GameObject;
                    //BlockGrid[y + _y, _x + x + 15] = Now.GetComponent<Ground>();
                    //BlockGrid[y + _y, _x + x + 15].gameObject.transform.parent = this.gameObject.transform;
                    //  }

                }
                else
                {
                    if (BlockGrid[y + _y, _x + x + 15])
                    {
                      //  Debug.Log((y + _y) + "<" + Height + "-" + (_x + x + 15) + "<" + Widht);
                     //   Debug.Log("del");
                        Destroy(BlockGrid[y + _y, _x + x + 15].gameObject);
                        GameObject Now = Instantiate(Resources.Load("Prefab/Ground"), new Vector3(_x + x, -(y + _y), 0), Quaternion.identity) as GameObject;
                        BlockGrid[y + _y, _x + x + 15] = Now.GetComponent<Ground>();
                        BlockGrid[y + _y, _x + x + 15].gameObject.transform.parent = this.gameObject.transform;
                    }
                }
            }
        }
    }

    private void GenerateTree(List<Vector2> TreeCoords)
    {
        Debug.Log(TreeCoords.Count);
        GameObject Now;
        TreesGrid = new Resource[TreeCoords.Count];
        for (int i = 0; i < TreeCoords.Count; i++)
        {
            Now = Instantiate(Resources.Load("Prefab/Tree"), TreeCoords[i], Quaternion.identity) as GameObject;
            TreesGrid[i] = Now.GetComponent<Resource>();
            TreesGrid[i].gameObject.transform.parent = this.gameObject.transform;
        }
        /*GameObject Now;
        bool CanCreate = false;
        if (x + 15 - Indent >= 0)
            for (int j = -Indent; j <= Indent; j++)
            {
                if (x + 15 + j < 30 && !TreesGrid[x + 15 + j])
                    CanCreate = true;
                else
                {
                    CanCreate = false;
                    break;
                }
            }

        if (CanCreate)
        {
            Now = Instantiate(Resources.Load("Prefab/Tree"), new Vector3(x, -i, 0), Quaternion.identity) as GameObject;
            TreesGrid[x + 15] = Now.GetComponent<Resource>();
            TreesGrid[x + 15].gameObject.transform.parent = this.gameObject.transform;
        }*/
    }


    private void GenerateZone()
    {
        Instantiate(Resources.Load("Prefab/GroundUp"), new Vector3(Widht / 2, 0, 0), Quaternion.identity);
        Instantiate(Resources.Load("Prefab/GroundUp"), new Vector3(Widht / 2 + 1, 0, 0), Quaternion.identity);

        Instantiate(Resources.Load("Prefab/GroundUp"), new Vector3(-Widht / 2 - 1, 0, 0), Quaternion.identity);
        Instantiate(Resources.Load("Prefab/GroundUp"), new Vector3(-Widht / 2 - 2, 0, 0), Quaternion.identity);
        for (int i = 0; i < Height; i++)
        {
            for (int j = -Widht / 2 - 2; j <= -Widht / 2 - 1; j++)
                Instantiate(Resources.Load("Prefab/Ground"), new Vector3(j, -i - 1, 0), Quaternion.identity);
            for (int j = Widht / 2; j <= Widht / 2 + 1; j++)
                Instantiate(Resources.Load("Prefab/Ground"), new Vector3(j, -i - 1, 0), Quaternion.identity);
        }
        for (int i = -Widht / 2 - 4; i <= Widht / 2 + 3; i++)
            for (int j = Height + 1; j <= Height + 2; j++)
                Instantiate(Resources.Load("Prefab/Ground"), new Vector3(i, -j, 0), Quaternion.identity);
    }
}