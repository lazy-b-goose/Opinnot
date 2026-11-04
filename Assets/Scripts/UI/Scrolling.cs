using UnityEngine;
using System.Collections;

public class scrolling : MonoBehaviour
{
    int XY;
    public GameObject Panel;
    public void Click(){
        StartCoroutine(Scroll());
    }
    IEnumerator Scroll(){
        XY = GameData.XY;
        yield return new WaitForSeconds(0.1f);
        Panel.transform.SetSiblingIndex(2);   
        GameData.XY = XY;
    }
}
