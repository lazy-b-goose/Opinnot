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
        JsonDownload();
        xy = System.Math.Abs((int)transform.position.x) * 100 + System.Math.Abs((int)transform.position.y);     // координаты для JSON
    }

    void JsonDownload(){        // перед началом работы один раз, загружает JSON 
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");        // получение "ссылки" на файл
        string json = File.ReadAllText(DataLocation);               // "запись" всего файла
        data = JsonConvert.DeserializeObject<DataHexses>(json);     // систематизация текста
    }
    int FoundType(int xy){
        Hexse hex = data.hexses.FirstOrDefault(h => h.xy == xy);    // поиск Type с подходящим xy
        return hex.Type;                                        // берем значение Type
    }
}