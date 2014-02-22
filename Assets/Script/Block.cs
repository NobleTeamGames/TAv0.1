using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    protected SpriteRenderer[] TextureBlock;
    private enum Type {Front, Back, Wall }
    private Type NowType;
	// Use this for initialization
    protected void Start() 
    {
        NowType = Type.Front;
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
        }
    }
}
