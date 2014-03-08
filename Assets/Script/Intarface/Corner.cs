using UnityEngine;
using System.Collections;

public class Corner : MonoBehaviour {

    public enum height { top, middle, bottom }
    public enum width { left, center, right }

    public int WidthLeng;
    public bool UseWidthLengh = false;
    public height HeightAlign = height.middle;
    public width WidthAlign = width.center;

    public bool ResBar;
	// Use this for initialization
	void Start () {
        // swith widht align
        Vector3 Trans = Vector3.zero;
       // Debug.Log(Screen.width + "/" + Screen.height + "=" + (Screen.width*1f / Screen.height*1f));
        switch (WidthAlign)
        {
            case width.center:
                Trans.x = 0;
                break;

            case width.left:
                Trans.x = -(Screen.width * 1f / Screen.height) / gameObject.transform.parent.transform.localScale.x * 1f;
                break;

            case width.right:
                Trans.x = (Screen.width * 1f / Screen.height) / gameObject.transform.parent.transform.localScale.x * 1f;
                break;

            default:
                break;
        }

        switch (HeightAlign)
        {
            case height.middle:
                Trans.y = 0;
                break;

            case height.bottom:
                Trans.y = -1 / gameObject.transform.parent.transform.localScale.y * 1f;
                break;

            case height.top:
                Trans.y = 1 / gameObject.transform.parent.transform.localScale.y * 1f;
                break;

            default:
                break;
        }
        gameObject.transform.localPosition = Trans;

        if (UseWidthLengh)
        {

            UISprite UI = gameObject.GetComponent<UISprite>();
     //       Debug.Log(UI.name);
            UI.SetDimensions(Screen.width, 30); //drawingDimensions.Set(1, UI.drawingDimensions.y, UI.drawingDimensions.z, UI.drawingDimensions.w);
        }

        if (ResBar)
        {
            UISprite UI = gameObject.GetComponent<UISprite>();
            Transform Bar = gameObject.transform.GetChild(0);
           // Debug.Log(UI.drawingDimensions.x);
            Bar.localPosition = new Vector3( UI.drawingDimensions.x,0,0);

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
