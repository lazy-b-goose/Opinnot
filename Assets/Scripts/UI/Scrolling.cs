using UnityEngine;

public class scrolling : MonoBehaviour
{
    public GameObject Panel;
    public void Click(){
        Panel.transform.SetSiblingIndex(2);     
    }
}
