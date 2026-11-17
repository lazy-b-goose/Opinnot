using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    public TMP_Text Gold;     // сюда перетащите текст в Inspector
    public TMP_Text Iron;
    public TMP_Text Stone;
    public TMP_Text Food;
    public TMP_Text People;

    void Update()
    {
        Gold.text = GameData.Gold.ToString();
        Iron.text = GameData.Iron.ToString();
        Stone.text = GameData.Stone.ToString();
        Food.text = GameData.Food.ToString();
        People.text = GameData.People.ToString() + '0';
    }
}
