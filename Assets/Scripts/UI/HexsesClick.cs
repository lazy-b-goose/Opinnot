using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class HexsesClick : MonoBehaviour
{
    GameObject Panel;

    [System.Serializable]
    public class Hexse
    {
        public int xy;
        public int Type;
    }

    [System.Serializable]
    public class DataHexses
    {
        public List<Hexse> hexses;
    }

    private DataHexses data;
    int xy;
    int Type;

    void Awake(){
        Panel = GameObject.Find("Bilding");     // сохранение строительного интерфейса
    }

    void OnMouseDown()
    {
        Panel.SetActive(true);                  // активация строительного интерфейса
        xy = System.Math.Abs((int)transform.position.x) * 100 + System.Math.Abs((int)transform.position.y);     // координаты для JSON
        GameData.XY = xy;
        GameData.Hexs = gameObject;
    }
}