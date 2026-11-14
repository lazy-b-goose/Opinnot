using UnityEngine;

public class OnObj : MonoBehaviour
{
    public GameObject Obj;

    public void ObjOn(){
        Obj.SetActive(false);
    }
}
