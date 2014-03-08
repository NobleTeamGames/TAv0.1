using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.z = 0;
        pos.y = 2.5f;
       // pos.y = Camera.main.transform.position.y;
        transform.position = pos;
       
	}


    public void SetUpBackGround(Sprite _sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprite;
    }
}
