using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void ScGame(){
        GameData.Generic = true;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
}
