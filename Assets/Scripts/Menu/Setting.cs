using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject Loading;

    public void Settings(){
        Loading.SetActive(true);
        GameData.InSettingGame = false;
        SceneManager.LoadScene("Setting", LoadSceneMode.Single);
    }
}
