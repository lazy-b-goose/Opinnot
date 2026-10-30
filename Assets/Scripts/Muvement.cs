using UnityEngine;
using UnityEngine.UI;


public class Muvement : MonoBehaviour
{
    Vector2 MouseXY;
    Vector2 MouseXYDelta;
    Vector3 a;
    public Slider zoom;
    

    float Sensitivity;

    void Update(){
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = zoom.value*20f + 1f;         // задаем размер камеры
        Sensitivity = cam.orthographicSize * 0.00496f;      // задаем чувствительность

        if(Input.GetMouseButtonDown(0)){
            MouseXY = Input.mousePosition;  // записываем положение мыши при нажатии
            a = transform.position;         // записываем положение камеры
        }
        if(Input.GetMouseButton(0)){
            a.x += (MouseXY.x - Input.mousePosition.x) * Sensitivity;   // перемещение мыши с противополжным знаком 
            a.y += (MouseXY.y - Input.mousePosition.y) * Sensitivity;   // умноженым на чувствительность
            MouseXY = Input.mousePosition;
            transform.position = a;         // перемещаем мышь
        }
    }
    
}
