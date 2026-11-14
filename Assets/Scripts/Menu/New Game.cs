using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public GameObject Loading;

    public void ScGame(){
        Loading.SetActive(true);    // включение экрана загрузки
        GameData.Generic = true;    // отмечаем что нужно генерировать мир
        SceneManager.LoadScene("Game", LoadSceneMode.Single);   // переключаемся на сцену игры
        }
}
