using UnityEngine;
using System.Collections;

public class BackLighting : MonoBehaviour
{
    public GameObject Panel;

    public void OnMouseDown(){
        Panel.SetActive(false);
        Vector3 pos = gameObject.transform.position;
        pos.z = 2f;
        gameObject.transform.position = pos;
    }
}