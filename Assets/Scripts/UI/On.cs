using UnityEngine;
using System.Collections;

public class On : MonoBehaviour
{
    public GameObject Panel;

    public void Start(){
        StartCoroutine(Off());
    }

    IEnumerator Off(){
        yield return new WaitForSeconds(0.05f);  // ждём чтобы задний гекс не открывал панель опять
        Panel.SetActive(false);
        GameObject BackLight = GameObject.Find("BackLighting");
        Vector3 pos = BackLight.transform.position;
        pos.z = 1f;
        BackLight.transform.position = pos;
    }
}
