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

        //Debug.Log(MaxWidht + "  " + MaxHeight);
        //Смена размера камеры в зависимости от разрещения
        camera.orthographicSize = -(MaxHeight-MaxSky) / 2f; //(MaxWidht*2 - 1) * (1 - MaxUp) / (camera.aspect * 2);
        defaultOrtSize = camera.orthographicSize;

        height = 2 * camera.orthographicSize;
        width = height * camera.aspect;
        
        Zoom = 0;

       // Debug.Log(camera.orthographicSize);
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


    private void cameraCorrect(Vector3 translation)
    {
        var _position = camera.transform.position;
        if (_position.x + translation.x + width / 2 >= MaxWidht - 1)
            translation.x = MaxWidht - 1 - _position.x - width / 2;
        else if (_position.x + translation.x - width / 2 <= -MaxWidht)
            translation.x = -MaxWidht - _position.x + width / 2;

        if (_position.y + translation.y + height / 2 > MaxSky)
            translation.y = MaxSky - _position.y - height / 2;
        else if (_position.y + translation.y - height / 2 < MaxHeight)
            translation.y = MaxHeight - _position.y + height / 2;

        _position += translation / (Zoom + 1);
        camera.transform.position = _position;
    }
    
}
