using UnityEngine;
using System.Collections;

public class Resource : Block
{
    //private bool Visible = false;
	// Use this for initialization
    public int DefaultLook;
    private bool Visual;
	void Start () {

        base.Start();
        NowType = Type.Resource;
        Sprite[] AllSprite = new Sprite[9];
        AllSprite = Resources.LoadAll<Sprite>("Sprites/Atlas/terrain");
        TextureBlock[0].sprite = AllSprite[7];
        TextureBlock[1].sprite = AllSprite[7];
        TextureBlock[2].sprite = AllSprite[7];
        TextureBlock[3].sprite = AllSprite[7];
        /*TextureBlock[0].color = new Color(0.3f, 0.05f, 0.05f);
        TextureBlock[1].color = new Color(0.3f, 0.05f, 0.05f);
        TextureBlock[2].color = new Color(0.05f, 0.3f, 0.05f);
        TextureBlock[3].color = new Color(0.05f, 0.05f, 0.3f);*/
        Visual = false;
	}
	
	// Update is called once per frame
	void Update () {

    
	}

    public override void UpdateLight()
    {
        if (!Visual)
        {
            Sprite[] AllSprite = new Sprite[9];
            AllSprite = Resources.LoadAll<Sprite>("Sprites/Atlas/terrain");
            TextureBlock[0].sprite = AllSprite[DefaultLook];
            TextureBlock[1].sprite = AllSprite[DefaultLook];
            TextureBlock[2].sprite = AllSprite[DefaultLook];
            TextureBlock[3].sprite = AllSprite[DefaultLook];
            Visual = true;


        }

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