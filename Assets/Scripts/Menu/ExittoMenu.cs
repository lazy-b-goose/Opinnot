using UnityEngine;
using UnityEngine.SceneManagement;

public class ExittoMenu : MonoBehaviour
{
    public GameObject Loading;

    public void ScGame(){
        Loading.SetActive(true);    // включение экрана загрузки
        GameData.Generic = false;    // отмечаем что не нужно генерировать мир
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);   // переключаемся на сцену игры
        }
}
