using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

public class HexsesClick : MonoBehaviour
{
    GameObject Panel;

    int xy;
    int Type;

    void Awake(){
        Panel = GameObject.Find("Bilding");     // сохранение строительного интерфейса
    }

    void OnMouseDown()
    {
        StartCoroutine(InterfaceOn());          // срабатывает при нажатии кнопки
    }

    IEnumerator InterfaceOn(){
        yield return new WaitForSeconds(0.14f);     // даем время отпустить кнопку
        if(!Input.GetMouseButton(0)){               // если ЛКМ не зажата
            Panel.SetActive(true);                  // активация строительного интерфейса
            xy = 1000000 + System.Math.Abs((int)transform.position.x) * 100 + System.Math.Abs((int)transform.position.y);     // координаты для JSON
            GameData.XY = xy;                       // передаем данные для строительства            
        }
    }
}