using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{

    protected SpriteRenderer[] TextureBlock;
    public enum Type { Front, Back, Wall, Resource }
    public Type NowType;

    // Use this for initialization
    protected void Start()
    {
        //NowType = Type.Front;
        TextureBlock = new SpriteRenderer[4];
        TextureBlock[0] = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        TextureBlock[1] = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        TextureBlock[2] = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        TextureBlock[3] = gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>();
        if (gameObject.transform.position.y == 0)
        {
            TextureBlock[0].color = new Color(0.3f, 0.3f, 0.3f);
            TextureBlock[1].color = new Color(0.3f, 0.3f, 0.3f);
            TextureBlock[2].color = new Color(0.05f, 0.05f, 0.05f);
            TextureBlock[3].color = new Color(0.05f, 0.05f, 0.05f);
        }
        else
        {
            TextureBlock[0].color = new Color(0.05f, 0.05f, 0.05f);
            TextureBlock[1].color = new Color(0.05f, 0.05f, 0.05f);
            TextureBlock[2].color = new Color(0.05f, 0.05f, 0.05f);
            TextureBlock[3].color = new Color(0.05f, 0.05f, 0.05f);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyBlock()
    {
        bool near = false;
        Vector2[] BlockAro = new Vector2[8];
        BlockAro = BlocksArownd();

        for (int i = 0; i < BlockAro.Length; i++)
        {
            if (BlockAro[i].x >= 0 && BlockAro[i].x < LevelScript.MainGrid.Widht && BlockAro[i].y >= 0 && BlockAro[i].y <= LevelScript.MainGrid.Height)
            {
                if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Back ||
                    LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)
                {
                    near = true;
                    break;
                }
            }
            else
            {
                if (BlockAro[i].y < 0)
                {
                    near = true;
                    break;
                }
            }
        }

        if (near)
        {
            //// ЗАМЕНИТ!!!! ////
            /*
             * 
             * Очень плохо
             * 
             * */

            for (int x = -2; x <= 2; x++)
            {
                foreach (var Cild in GameObject.Find("Gamer").transform.GetChild(0))
                {

                    if ((Cild as Transform).position == new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y + 1, 0))
                    {                       
                        near = false;
                    }
                }
            }
        }

        if (NowType == Type.Front && near)
        {
            NowType = Type.Back;
            //TextureBlock[0].color = Color.Lerp(TextureBlock[0].color, new Color(0.5f, 0.5f, 0.5f), 0.1f);
            TextureBlock[0].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[1].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[2].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[3].color = new Color(0.5f, 0.5f, 0.5f);
            UpdateWall();

            BlockAro = BlocksArownd();

            for (int i = 0; i < BlockAro.Length; i++)
            {
                if (BlockAro[i].x >= 0 && BlockAro[i].x < LevelScript.MainGrid.Widht && BlockAro[i].y >= 0 && BlockAro[i].y <= LevelScript.MainGrid.Height)
                {
                    LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].UpdateLight();
                }
            }
            //BlocksArownd(new Vector2(gameObject.transform.position.x + 15, gameObject.transform.position.y));
        }
    }

    public void ChangeWall()
    {
        //UpdateLight();
        
        if (NowType == Type.Back)
        {
            /*TextureBlock[0].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[1].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[2].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[3].color = new Color(0.5f, 0.5f, 0.5f);*/
            Sprite[] AllSprite = new Sprite[9];
            AllSprite = Resources.LoadAll<Sprite>("Sprites/Atlas/terrain");
            TextureBlock[0].sprite = AllSprite[6];
            TextureBlock[1].sprite = AllSprite[6];
            TextureBlock[2].sprite = AllSprite[6];
            TextureBlock[3].sprite = AllSprite[6];
            NowType = Type.Wall;
            Vector2[] BlockAro = new Vector2[8];
            BlockAro = BlocksArownd();

            for (int i = 0; i < BlockAro.Length; i++)
            {
                if (BlockAro[i].x >= 0 && BlockAro[i].x < LevelScript.MainGrid.Widht && BlockAro[i].y >= 0 && BlockAro[i].y <= LevelScript.MainGrid.Height)
                {

                    if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType != Type.Back)
                    {
                        LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].SetWall();

                        LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].UpdateLight();
                    }

                    
                }
            }
            UpdateWall();
           // UpdateLight();
        }
        
    }

    private void UpdateWall()
    {
        Vector2[] BlockAro = new Vector2[8];
        BlockAro = BlocksArownd();

        for (int j = 0; j < 8; j++)
        {
            RemoveWall(j);
        }

        //Debug.Log(LevelScript.MainGrid.Widht - 1);

        if (NowType == Type.Wall)
        {
            if (this.transform.position.x + 15 == 0)
            {
                if (this.transform.position.y + 1 <= 0)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y + 1), (int)this.transform.position.x + 15].AddWall(5);
                if (this.transform.position.y - 1 >= -LevelScript.MainGrid.Height)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y - 1), (int)this.transform.position.x + 15].AddWall(0);
                AddWall(0);
                AddWall(3);
                AddWall(5);
            }
            if (this.transform.position.x + 15 == (LevelScript.MainGrid.Widht - 1))
            {
                if (this.transform.position.y + 1 <= 0)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y + 1), (int)this.transform.position.x + 15].AddWall(7);
                if (this.transform.position.y - 1 >= -LevelScript.MainGrid.Height)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y - 1), (int)this.transform.position.x + 15].AddWall(2);
                AddWall(2);
                AddWall(4);
                AddWall(7);
            }

            if (this.transform.position.y == -LevelScript.MainGrid.Height)
            {
                if (this.transform.position.x + 16 < LevelScript.MainGrid.Widht)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y), (int)this.transform.position.x + 16].AddWall(5);
                if (this.transform.position.x + 14 >= 0)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y), (int)this.transform.position.x + 14].AddWall(7);
                AddWall(5);
                AddWall(6);
                AddWall(7);
            }
            if (this.transform.position.y == 0)
            {

                if (this.transform.position.x + 16 < LevelScript.MainGrid.Widht && LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y), (int)this.transform.position.x + 16].NowType == Type.Front)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y), (int)this.transform.position.x + 16].AddWall(0);

                if (this.transform.position.x + 14 >= 0 && LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y), (int)this.transform.position.x + 14].NowType == Type.Front)
                    LevelScript.MainGrid.BlockGrid[-(int)(this.transform.position.y), (int)this.transform.position.x + 14].AddWall(2);               
            }
        }


        for (int i = 0; i < BlockAro.Length; i++)
        {
            //if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)

            if (BlockAro[i].x >= 0 && BlockAro[i].x < LevelScript.MainGrid.Widht && BlockAro[i].y >= 0 && BlockAro[i].y <= LevelScript.MainGrid.Height)
                if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall || LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Back)
                    LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].RemoveWall(7 - i);

        }
    }

    protected Vector2[] BlocksArownd()
    {
        /*
         * 
         * Return array
         * [1][2][3]
         * [4][ ][5]
         * [6][7][8]
         * 
         * 
         * Нужен учет, если за границей
         * */
        //DestroyBlock();
        Vector2[] BlockArw = new Vector2[8];
        int j = 0;
        for (int y = 1; y >= -1; y--)
            for (int x = -1; x <= 1; x++)
            {
                if (y != 0 || x != 0)
                {
                    BlockArw[j] = new Vector2(gameObject.transform.position.x + x + 15, -(gameObject.transform.position.y + y));
                    j++;
                }
            }


        return BlockArw;

    }

    public void SetWall()
    {       
        //Debug.Log(1);
        Vector2[] BlockAro = new Vector2[8];
        BlockAro = BlocksArownd();

        for (int i = 0; i < BlockAro.Length; i++)
        {
            if (BlockAro[i].x >= 0 && BlockAro[i].x < LevelScript.MainGrid.Widht && BlockAro[i].y >= 0 && BlockAro[i].y <= LevelScript.MainGrid.Height)
            {
                if (NowType != Type.Wall)
                    switch (i)
                    {
                        case 0:
                            if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i + 3].y), System.Convert.ToInt16(BlockAro[i + 3].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i + 1].y), System.Convert.ToInt16(BlockAro[i + 1].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)
                                AddWall(i);
                            break;
                        case 2:
                            if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i - 1].y), System.Convert.ToInt16(BlockAro[i - 1].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i + 2].y), System.Convert.ToInt16(BlockAro[i + 2].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)
                                AddWall(i);
                            break;
                        case 5:
                            if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i + 1].y), System.Convert.ToInt16(BlockAro[i + 1].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i - 2].y), System.Convert.ToInt16(BlockAro[i - 2].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)
                                AddWall(i);
                            break;
                        case 7:
                            if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i - 1].y), System.Convert.ToInt16(BlockAro[i - 1].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i - 3].y), System.Convert.ToInt16(BlockAro[i - 3].x)].NowType == Type.Wall ||
                                LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)
                                AddWall(i);

                            break;
                        default:
                            if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)
                                if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType != NowType)
                                    AddWall(i);
                            break;

                    }
            }
            else
            {
                if (BlockAro[i].x < 0)
                {

                }
                else if (BlockAro[i].x < LevelScript.MainGrid.Widht)
                {

                }
                else if (BlockAro[i].y >= 0)
                {
                }
                else if (BlockAro[i].y <= LevelScript.MainGrid.Height)
                {
                }

            }
            /* if (i == 1 || i == 3 || i == 6 || i == 8)
             {

             }
             else*/

        }
    }

    private void AddWall(int Place)
    {
        Place += 1;
        /*
         * Place  -
         * [1][  2  ][3]
         * [   ]   [   ]
         * [ 4 ]   [ 5 ]
         * [___]   [___]
         * [6][  7  ][8]
         * */
        if (NowType != Type.Resource)
            gameObject.transform.FindChild("Wall" + Place).gameObject.SetActive(true);

    }

    private void RemoveWall(int Place)
    {
        Place += 1;
        /*
         * Place  -
         * [1][  2  ][3]
         * [   ]   [   ]
         * [ 4 ]   [ 5 ]
         * [___]   [___]
         * [6][  7  ][8]
         * */
        if (NowType != Type.Resource)
            gameObject.transform.FindChild("Wall" + Place).gameObject.SetActive(false);

    }

    public virtual void UpdateLight()
    {
       
        if (NowType == Type.Front)
        {
            Vector2[] BlockAro = new Vector2[8];
            BlockAro = BlocksArownd();
            for (int i = 0; i < BlockAro.Length; i++)
            {
                if (BlockAro[i].y < 0)
                {

                    TextureBlock[0].color = new Color(0.3f, 0.3f, 0.3f);
                    TextureBlock[1].color = new Color(0.3f, 0.3f, 0.3f);
                }

                if (BlockAro[i].x >= 0 && BlockAro[i].x < LevelScript.MainGrid.Widht && BlockAro[i].y >= 0 && BlockAro[i].y <= LevelScript.MainGrid.Height)
                {
                    if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Back || LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)
                    {
                        switch (i)
                        {
                            case 0:
                                TextureBlock[1].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            case 2:
                                TextureBlock[0].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            case 5:
                                TextureBlock[2].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            case 7:
                                TextureBlock[3].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            case 1:
                                TextureBlock[0].color = new Color(0.3f, 0.3f, 0.3f);
                                TextureBlock[1].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            case 3:
                                TextureBlock[1].color = new Color(0.3f, 0.3f, 0.3f);
                                TextureBlock[2].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            case 4:
                                TextureBlock[0].color = new Color(0.3f, 0.3f, 0.3f);
                                TextureBlock[3].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            case 6:
                                TextureBlock[2].color = new Color(0.3f, 0.3f, 0.3f);
                                TextureBlock[3].color = new Color(0.3f, 0.3f, 0.3f);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
    
}
