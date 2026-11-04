using UnityEngine;
using System.Collections;

public class On : MonoBehaviour
{
    public GameObject Panel;

    public void Start(){
        StartCoroutine(Off());
    }

    IEnumerator Off(){
        yield return new WaitForSeconds(0.1f);  // ждём чтобы задний гекс не открывал панель опять
        Panel.SetActive(false);
    }
}
