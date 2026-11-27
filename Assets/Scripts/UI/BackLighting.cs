using UnityEngine;
using System.Collections;

public class BackLighting : MonoBehaviour
{
    public GameObject Panel;

    public void OnMouseDown(){
        Panel.SetActive(false);
        GameObject BackLight = GameObject.Find("BackLighting");
        Vector3 pos = BackLight.transform.position;
        pos.z = 2f;
        BackLight.transform.position = pos;
    }
}