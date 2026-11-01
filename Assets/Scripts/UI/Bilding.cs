using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

[Serializable]
public class DataHexsesInst
{
    public int xy;
    public int Type;
 /* [постройка(0 - нету; 1 - дом; 2 - завод; 3 - шахта; 4 - поле; 5 - окоп; 6 - авиапорт; 10 - дорога)] * 1000 + 
    [тип территории(0 - ничейная)]*100 +
    [руды(0 - нету; 1 - камень; 2 - железо; 3 - золото)] +
    [тип рельефа(1 - океан; 2 - море; 3 - пляж; 4 - равнина; 5 - холм; 6 - гора)]    */
}

[Serializable]
public class HexsesListInst
{
    public List<DataHexses> hexses = new List<DataHexses>();    // посредник для сохранения
}

public class Bilding : MonoBehaviour
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
    public int a;
    int Type;

    public void Bild(){
        JsonDownload();
        Type = Found(GameData.XY) + a * 1000; 
        Destroy(GameData.Hexs);
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
