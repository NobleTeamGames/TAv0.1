using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class CameraController : MonoBehaviour {


    private Camera MainCam; //Игровая камера

    public float Zoom;  //Текущий зум



    private float height;
    private float width;
    private float MaxHeight;
    private float MaxWidht;
    private float MaxSky=3.5f;
    private float MinZoom = 2;
    public float MaxUp = 1f / 3f; //Макс размер камеры от размера экрана(1- MaxUp)


    public float MaxDragSpeed = 20;//CСкорость перемещения мишью


    private float defaultOrtSize; //Изначальный ортогональный размер камеры


    void Awake()
    {


    }

    void Start()
    {

        TextAsset xmlAsset = Resources.Load("LevelStats/Level") as TextAsset; //Считываня xml
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlAsset)
            xmlDoc.LoadXml(xmlAsset.text);

        XmlElement Stats = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("Location");
        MaxHeight = -(Convert.ToInt16(Stats.GetAttribute("height")) + 0.5f);
        MaxWidht = Convert.ToInt16(Stats.GetAttribute("width")) / 2f + 0.5f;

        Debug.Log(MaxWidht + "  " + MaxHeight);
        //Смена размера камеры в зависимости от разрещения
        camera.orthographicSize = -(MaxHeight-MaxSky) / 2f; //(MaxWidht*2 - 1) * (1 - MaxUp) / (camera.aspect * 2);
        defaultOrtSize = camera.orthographicSize;

        height = 2 * camera.orthographicSize;
        width = height * camera.aspect;
        
        Zoom = 0;

        Debug.Log(camera.orthographicSize);
        //Установка камеры в позицию по умолчанию
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, MaxHeight + camera.orthographicSize, this.gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var translation = Vector3.zero;             //Перемещение кнопкой миши
        if (Input.GetMouseButton(0)) // MMB
        {
            // Hold button and drag camera around
            translation -= new Vector3(Input.GetAxis("Mouse X") * MaxDragSpeed * Time.deltaTime,
                               Input.GetAxis("Mouse Y") * MaxDragSpeed * Time.deltaTime, 0);
        }

        /*
         * Перемещение = 0 при преувеличении размеров
         * 
         * */
        cameraCorrect(translation);

        translation = Vector3.zero;
        height = camera.orthographicSize * 2;
        width = height * camera.aspect;
        /*
         * Зумирование колесом миши
         * 
         * И проверка на изменение зума
         * дабы не изменять ортогональный размер лишний раз
         * 
         * 
         * 
         * */
        var Wheel = Input.GetAxis("Mouse ScrollWheel");
        if (Wheel != 0)
        {
            if (!(Wheel > 0 && Zoom + Wheel <= MinZoom) && !(Wheel < 0 && Zoom + Wheel >= 0))
                return;


            Zoom += Wheel;
            camera.orthographicSize = defaultOrtSize * Mathf.Pow(2, (Zoom) * -1);
            if (Wheel > 0)
            {
                var mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.z));
                var dragOrigin = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.transform.position.z));
                translation.y = translation.y + ((mousePos.y - dragOrigin.y) / (camera.orthographicSize * 2));
                translation.x = translation.x + ((mousePos.x - dragOrigin.x) / (camera.orthographicSize * 2));
            }
            else
            {
                var mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.z));
                var dragOrigin = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.transform.position.z));
                translation.y = translation.y - ((mousePos.y - dragOrigin.y) / (camera.orthographicSize * 2));
                translation.x = translation.x - ((mousePos.x - dragOrigin.x) / (camera.orthographicSize * 2));
            }
            cameraCorrect(translation);
        }
        

    }

   /* void LateUpdate()
    {      
        height = 2 * camera.orthographicSize; //Определение новой высоты и ширины камеры
        width = height * camera.aspect;
        Vector3 translation = Vector3.zero;   
        
         * Зумирование колесом миши
         * 
         * И проверка на изменение зума
         * дабы не изменять ортогональный размер лишний раз
         * 
         * 
         * 
         * 
        float Wheel = Input.GetAxis("Mouse ScrollWheel");
        if (Wheel != 0)
        {
            if (Wheel>0 && width >= MinZoom)
            {
                camera.orthographicSize = defaultOrtSize * Mathf.Pow(2, (Zoom + Wheel) * -1);
                Zoom += Wheel;
                translation = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            }
            if (Wheel < 0 && width <= MaxWidht * 2 * (1 - MaxUp))
            {
                camera.orthographicSize = defaultOrtSize * Mathf.Pow(2, (Zoom + Wheel) * -1);
                Zoom += Wheel;
                translation = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            }
            
            float x=0;
            float y;

            if (translation.x <= 0 && gameObject.transform.position.x <= 0)
                if (translation.x > gameObject.transform.position.x)
                    x = translation.x - gameObject.transform.position.x;
                else
                    x = -(gameObject.transform.position.x - translation.x);
            else
                if (translation.x < 0 && gameObject.transform.position.x > 0)                
                        x = -(gameObject.transform.position.x - translation.x);       
                else
                    x = translation.x - gameObject.transform.position.x;
            
            if (translation.y < 0 && gameObject.transform.position.y < 0)
                if (translation.y > gameObject.transform.position.y)
                    y = translation.y - gameObject.transform.position.y;
                else
                    y = -(gameObject.transform.position.y - translation.y);
            else
                if (translation.x < 0 && gameObject.transform.position.y > 0)
                    y = -(gameObject.transform.position.y - translation.y);
                else
                    y = translation.y - gameObject.transform.position.x;

            camera.transform.position += new Vector3(x,y,0)*Time.deltaTime*MaxDragSpeed / (Zoom + 1);
        }



        
         * Ниже идет смещение камеры при преувеличении границ экрана
         * 
         * * MaxDragSpeed * Time.deltaTime
         * С верхней границей баг
         * 
         * 
         * 
         * 
         * 
        if (camera.transform.position.x - width / 2 < -MaxWidht)
        {
            camera.transform.position += new Vector3(-MaxWidht - (camera.transform.position.x - width / 2), 0, 0) * Time.deltaTime  * 10;
            //ChangeZoom(-0.01f);
        }
        if (camera.transform.position.x + width / 2 > MaxWidht-1)
        {
            camera.transform.position += new Vector3(MaxWidht - 1 - (camera.transform.position.x + width / 2), 0, 0) * Time.deltaTime  * 10;
            //ChangeZoom(-0.01f);
        }
        if (camera.transform.position.y - height / 2 < MaxHeight && camera.transform.position.y + height / 2 > MaxSky)
        {
            camera.orthographicSize = defaultOrtSize * Mathf.Pow(2, (Zoom + 0.006f) * -1);
            Zoom += 0.006f;
        }
        else
        {
            if (camera.transform.position.y - height / 2 < MaxHeight)
            {
                camera.transform.position += new Vector3(0, MaxHeight - (camera.transform.position.y - height / 2), 0) * Time.deltaTime * 10;                
            }
            if (camera.transform.position.y + height / 2 > MaxSky)
            {
                camera.transform.position += new Vector3(0, MaxSky - (camera.transform.position.y + height / 2), 0) * Time.deltaTime * 10;
                
            }
        }


    }*/

    private void cameraCorrect(Vector3 translation)
    {
        var _position = camera.transform.position;
        if (_position.x + translation.x + width / 2 >= MaxWidht - 1)
            translation.x = MaxWidht - 1 - _position.x - width / 2;
        else if (_position.x + translation.x - width / 2 <= -MaxWidht)
            translation.x = -MaxWidht - _position.x + width / 2;

        if (_position.y + translation.y + height / 2 > MaxSky)
        {
            Debug.Log("1");
            translation.y = MaxSky - _position.y - height / 2;
        }
        else if (_position.y + translation.y - height / 2 < MaxHeight)
        {
            Debug.Log("2");
            translation.y = MaxHeight - _position.y + height / 2;
        }
        _position += translation / (Zoom + 1);
        camera.transform.position = _position;
    }
    
}
