using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class GameData : MonoBehaviour
{
    public static int XY;
    public static bool Generic = true;
    public static int Gold;
    public static int Iron;
    public static int Stone;
    public static int Food;
    public static int People;

    [System.Serializable]       // загрузка данных
    public class Resource
    {
        public int Gold;
        public int Iron;
        public int Stone;
        public int Food;
        public int People; 
    }
    
    [System.Serializable]       // загрузка данных
    public class DataResources
    {
        public List<Resource> Resources;
    }

    private DataResources data;    // загрузка данных


    void Start(){
        JsonDownload();
        Resource res = data.Resources[0];

        GameData.Gold = res.Gold;
        GameData.Iron = res.Iron;
        GameData.Stone = res.Stone;
        GameData.Food = res.Food;
        GameData.People = res.People;

        if(Generic){
            GameData.Gold = 500;
            GameData.Iron = 200;
            GameData.Stone = 500;
            GameData.Food = 1000;
            GameData.People = 10;
        }
    }

    void JsonDownload(){
        string DataLocation = Path.Combine(Application.dataPath, "DataResources.json");        // получение "ссылки" на файл
        string json = File.ReadAllText(DataLocation);               // "запись" всего файла
        data = JsonConvert.DeserializeObject<DataResources>(json);     // систематизация текста
    }

}




 /* постройка       (0 - нету; 1 - дом; 2 - завод; 3 - шахта; 4 - поле; 5 - окоп; 6 - авиапорт; 10 - дорога)
    тип территории  (0 - ничейная)
    руды            (0 - нету; 1 - камень; 2 - железо; 3 - золото)
    тип рельефа     (1 - океан; 2 - море; 3 - пляж; 4 - равнина; 5 - холм; 6 - гора)    */

/*

[Serializable]
public class DataResources
{
    public int Gold;
    public int Iron;
    public int Stone;
    public int Food;
    public int People; 
}

[Serializable]
public class ResourcesList
{
    public List<DataResources> Resources = new List<DataResources>();    // посредник для сохранения
}

ResourcesList dataList = new ResourcesList();

public void SaveResources(int xy, int Type, ResourcesList dataList){
        DataResources data = new DataResources();
        data.xy = xy;
        data.Type = Type;
        dataList.hexses.Add(data);
    }

    public void SaveToFile(ResourcesList dataList){
        string DataLocation;
        DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");
        File.WriteAllText(DataLocation, JsonUtility.ToJson(dataList, true));
     }

    */