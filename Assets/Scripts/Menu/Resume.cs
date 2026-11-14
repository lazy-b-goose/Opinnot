using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    public GameObject Loading;

    public void ScGame(){
        Loading.SetActive(true);    // включение экрана загрузки
        GameData.Generic = false;    // отмечаем что не нужно генерировать мир
        SceneManager.LoadScene("Game", LoadSceneMode.Single);   // переключаемся на сцену игры
        }
}
