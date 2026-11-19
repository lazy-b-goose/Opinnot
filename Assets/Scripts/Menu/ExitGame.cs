using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class DataResources
{
    public int Gold;
    public int Iron;
    public int Stone;
    public int Food;
    public int People; 
    public string Langage;
}

[Serializable]
public class ResourcesList
{
    public List<DataResources> Resources = new List<DataResources>();    // посредник для сохранения
}

public class ExitGame : MonoBehaviour
{
    ResourcesList dataList = new ResourcesList();
    public void Quit()
    {
        SaveResources(dataList);
        SaveToFile(dataList);

        Application.Quit();
        Debug.Log("Exit");
    }

    public void SaveResources(ResourcesList dataList){
        DataResources data = new DataResources();
        data.Gold = GameData.Gold;
        data.Iron = GameData.Iron;
        data.Stone = GameData.Stone;
        data.Food = GameData.Food;
        data.People = GameData.People;
        data.Langage = GameData.Langage;

        dataList.Resources.Add(data);
    }

    public void SaveToFile(ResourcesList dataList){
        string DataLocation;
        DataLocation = Path.Combine(Application.dataPath, "DataResources.json");
        File.WriteAllText(DataLocation, JsonUtility.ToJson(dataList, true));
    }
}
