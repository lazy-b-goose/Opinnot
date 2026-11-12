using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    public TMP_Text scoreText;     // сюда перетащите текст в Inspector

    void Update()
    {
        scoreText.text = GameData.Gold.ToString();
    }
}
