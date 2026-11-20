using UnityEngine;

public class Test : MonoBehaviour
{

    public GameObject Black;
    public GameObject White;

    Vector2 a;  // координаты на которых заспавнить наш квадрат

    void Start(){
        for(int y = 0; y < 100; y++){
            for(int x = 0; x < 100; x++){
                a.x = x;
                a.y = y;
                if(Mathf.PerlinNoise(x * 0.1f, y * 0.1f) > 0.5f){
                    Instantiate(Black, a, Quaternion.identity);
                }else{
                    Instantiate(White, a, Quaternion.identity);
                }
                
            }
            Debug.Log("\n");
        }
    }
}
