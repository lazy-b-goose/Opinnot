using UnityEngine;

public class Muvement : MonoBehaviour
{
    Vector2 MouseXY;
    Vector2 MouseXYDelta;
    Vector3 a;

    public float Sensitivity = 0.01f;

    void Update(){

        if(Input.GetMouseButtonDown(0)){
            MouseXY = Input.mousePosition;
            a = transform.position;
        }
        if(Input.GetMouseButton(0)){
            a.x += (MouseXY.x - Input.mousePosition.x) * Sensitivity;
            a.y += (MouseXY.y - Input.mousePosition.y) * Sensitivity;
            MouseXY = Input.mousePosition;
            transform.position = a;
        }
    }
    
}
