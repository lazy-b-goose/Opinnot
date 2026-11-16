using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class ExittoMenu : MonoBehaviour
{
    public GameObject Loading;
    ResourcesList dataList = new ResourcesList();

    public void ScGame(){
        SaveResources(dataList);
        SaveToFile(dataList);
        
        Loading.SetActive(true);                                // включение экрана загрузки
        GameData.Generic = false;                               // отмечаем что не нужно генерировать мир
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);   // переключаемся на сцену игры
        }

    public void SaveResources(ResourcesList dataList){
        DataResources data = new DataResources();
        data.Gold = GameData.Gold;
        data.Iron = GameData.Iron;
        data.Stone = GameData.Stone;
        data.Food = GameData.Food;
        data.People = GameData.People;

        dataList.Resources.Add(data);
    }

    public void SaveToFile(ResourcesList dataList){
        string DataLocation;
        DataLocation = Path.Combine(Application.dataPath, "DataResources.json");
        File.WriteAllText(DataLocation, JsonUtility.ToJson(dataList, true));
    }
}
