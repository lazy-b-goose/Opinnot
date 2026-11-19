using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingGame : MonoBehaviour
{
    public GameObject Loading;

    public void Setting(){
        Loading.SetActive(true);
        GameData.InSettingGame = true;
        SceneManager.LoadScene("Setting", LoadSceneMode.Single);
    }
}
