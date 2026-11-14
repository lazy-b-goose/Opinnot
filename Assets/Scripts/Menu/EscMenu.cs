using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public GameObject Esc;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Esc.activeSelf){
                Esc.SetActive(false);
            }else{
                Esc.SetActive(true);
            }
        }
    }
}
