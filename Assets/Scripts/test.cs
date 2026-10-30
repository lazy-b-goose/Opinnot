using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class test : MonoBehaviour
{
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

    int xy = 300;

    void Start()
    {
        JsonDownload();
        
        int f = Found(xy);
        Debug.Log("Type для xy = 300: " + f);
    }

    void JsonDownload(){
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");        // получение "ссылки" на файл
        string json = File.ReadAllText(DataLocation);               // "запись" всего файла
        data = JsonConvert.DeserializeObject<DataHexses>(json);     // систематизация текста
    }
    int Found(int xy){
        Hexse hex = data.hexses.FirstOrDefault(h => h.xy == xy);    // поиск Type с подходящим xy
        return hex.Type;                                        // берем значение Type
    }
}
/*
Slider Num;
Mun.value
*/