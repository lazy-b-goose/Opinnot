using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class HexsesClick : MonoBehaviour
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
    int xy;
    int Type;

    void OnMouseDown()
    {
        JsonDownload();
        xy = System.Math.Abs((int)transform.position.x) * 100 + System.Math.Abs((int)transform.position.y);
        Type = FoundType(xy)%10;
        if(Type == 1){
            Debug.Log("Океан");
        }
        if(Type == 2){
            Debug.Log("Море");
        }
        if(Type == 3){
            Debug.Log("Пляж");
        }
        if(Type == 4){
            Debug.Log("Равнина");
        }
        if(Type == 5){
            Debug.Log("Холм");
        }
        if(Type == 6){
            Debug.Log("Гора");
        }

        
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