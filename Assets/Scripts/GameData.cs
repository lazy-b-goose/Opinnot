using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;


public class GameData : MonoBehaviour
{
    public static int XY;
    public static bool Generic = true;
    public static bool InSettingGame;

    public static int Gold;
    public static int Iron;
    public static int Stone;
    public static int Food;
    public static int People;
    public static string Langage = "en-US";

    [System.Serializable]       // загрузка данных
    public class Resource
    {
        public int Gold;
        public int Iron;
        public int Stone;
        public int Food;
        public int People; 
        public string Langage;
    }
    
    [System.Serializable]       // загрузка данных
    public class DataResources
    {
        public List<Resource> Resources;
    }

    private DataResources data;    // загрузка данных

    /*IEnumerator Start()
    {
        JsonDownload();

        Resource res = data.Resources[0];

        Gold = res.Gold;
        Iron = res.Iron;
        Stone = res.Stone;
        Food = res.Food;
        People = res.People;
        Langage = res.Langage;

        var initialization = LocalizationSettings.InitializationOperation;
        yield return initialization;

        if (initialization.Status !=
            UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.LogError(
                "Ошибка загрузки Localization:\n" +
                initialization.OperationException
            );

            yield break;
        }

        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(Langage);

        if (locale == null)
        {
            Debug.LogError("Локаль не найдена: " + Langage);
            yield break;
        }

        LocalizationSettings.SelectedLocale = locale;

    }*/


    IEnumerator Start(){
        JsonDownload();
        Resource res = data.Resources[0];

        Gold = res.Gold;
        Iron = res.Iron;
        Stone = res.Stone;
        Food = res.Food;
        People = res.People;
        Langage = res.Langage;

        yield return LocalizationSettings.InitializationOperation;

        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(GameData.Langage);
        LocalizationSettings.SelectedLocale = locale;
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