using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Home : MonoBehaviour
{
    [System.Serializable]       // загрузка данных
    public class HexseHome
    {
        public int xy;
        public int Type;
        public string ppl;
    }

    [System.Serializable]       // загрузка данных
    public class DataHexsesHome
    {
        public List<HexseHome> hexses;
    }

    private DataHexsesHome data;    // загрузка данных

    int XY;
    string ppl;

    Vector2 a;

    void Start()
    {
        a = gameObject.transform.position;
        XY = 1000000 + System.Math.Abs((int)a.x) * 100 + System.Math.Abs((int)a.y);

        JsonDownload();

        if ((a.x / 1) == 0)     
        {
            //Sphere0(XY);
        }
        else
        {
            //Sphere1(XY);
        }
    }

    void Update()
    {
        
    }

    void JsonDownload()
    {
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");        // получение "ссылки" на файл
        string json = File.ReadAllText(DataLocation);               // "запись" всего файла
        data = JsonConvert.DeserializeObject<DataHexsesHome>(json);     // систематизация текста
    }
    int Found(int xy)
    {
        HexseHome hex = data.hexses.FirstOrDefault(h => h.xy == xy);    // поиск Type с подходящим xy
        return hex.Type;                                        // берем значение Type
    }

    public void SaveHexs(int xy, string newType)
    {
        HexseHome hex = data.hexses.FirstOrDefault(h => h.xy == xy);

        if (hex == null)
        {
            hex = new HexseHome();
            hex.xy = xy;
            data.hexses.Add(hex);
        }

        hex.ppl = newType;
    }

    public void SaveToFile()
    {
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(DataLocation, json);
    }

    public void Sphere0(int XY, string ppl)         // [
    {
        int pplHome = 100;
        int pplWorkDelt = 0;
        int a = (Found(XY + 1) / 1000) % 10;
        if (a == 2 || a == 3 || a == 4 || a == 6)
        {
            switch (a)
            {
                case 2: pplWorkDelt = 100; break;
                case 3: pplWorkDelt = 300; break;
                case 4: pplWorkDelt = 30; break;
                case 6: pplWorkDelt = 50; break;
            }
            if(pplHome >= pplWorkDelt)
            {
                pplHome -= pplWorkDelt;
            }
            else
            {

            }
        }

    }
    public void Sphere1(int XY)         // x дробный  ]
    {

    }
}
