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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyBlock()
    {
        if (NowType == Type.Front)
        {
            NowType = Type.Back;
            TextureBlock[0].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[1].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[2].color = new Color(0.5f, 0.5f, 0.5f);
            TextureBlock[3].color = new Color(0.5f, 0.5f, 0.5f);
            UpdateWall();
            //BlocksArownd(new Vector2(gameObject.transform.position.x + 15, gameObject.transform.position.y));
        }
    }

    public void ChangeWall()
    {
        if (NowType == Type.Back)
        {
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
                        LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].SetWall();
                }
            }
            UpdateWall();
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
        for (int i = 0; i < BlockAro.Length; i++)
        {
            //if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall)

            if (BlockAro[i].x >= 0 && BlockAro[i].x < LevelScript.MainGrid.Widht && BlockAro[i].y >= 0 && BlockAro[i].y <= LevelScript.MainGrid.Height)
                if (LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Wall || LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].NowType == Type.Back)
                    LevelScript.MainGrid.BlockGrid[System.Convert.ToInt16(BlockAro[i].y), System.Convert.ToInt16(BlockAro[i].x)].RemoveWall(7 - i);

        }
    }

    private Vector2[] BlocksArownd()
    {
        /*
         * 
         * Return array
         * [1][2][3]
         * [4][ ][5]
         * [6][7][8]
         * 
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
                if(NowType!=Type.Wall)            
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

    public void Col()
    {
        TextureBlock[0].color = new Color(0.1f, 0.5f, 0.5f);
        TextureBlock[1].color = new Color(0.5f, 0.1f, 0.5f);
        TextureBlock[2].color = new Color(0.5f, 0.5f, 0.1f);
        TextureBlock[3].color = new Color(0.5f, 0.1f, 0.5f);
    }
    
}
