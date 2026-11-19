using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class DataResourcesLang
{
    public string Langage;
}
[Serializable]
public class ResourcesListLang
{
    public List<DataResourcesLang> Resources = new List<DataResourcesLang>();    // посредник для сохранения
}

public class ExitforSetting : MonoBehaviour
{
    ResourcesListLang dataList = new ResourcesListLang();
    public GameObject Loading;

    public void Exit(){
        if(GameData.InSettingGame){
            Loading.SetActive(true);
            GameData.Generic = false;
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }else{
            Loading.SetActive(true);
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        
        SaveResources(dataList);
        SaveToFile(dataList);
    }

    public void SaveResources(ResourcesListLang dataList){
        DataResourcesLang data = new DataResourcesLang();
        data.Langage = GameData.Langage;

        dataList.Resources.Add(data);
    }

    public void SaveToFile(ResourcesListLang dataList){
        string DataLocation;
        DataLocation = Path.Combine(Application.dataPath, "DataResources.json");
        File.WriteAllText(DataLocation, JsonUtility.ToJson(dataList, true));
    }
}
