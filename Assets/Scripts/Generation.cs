using UnityEngine;

public class Generation : MonoBehaviour
{
    public GameObject ocean;            //префабы высот
    public GameObject see;
    public GameObject beach;
    public GameObject plaine;
    public GameObject hill;
    public GameObject mountain;
    [Range(0f, 1f)]                     //настройка размеров высот
    public float oceanL = 0.3f;
    [Range(0f, 1f)]     
    public float seeL = 0.4f;
    [Range(0f, 1f)] 
    public float beachL = 0.45f;
    [Range(0f, 1f)]     
    public float plaineL = 0.65f;
    [Range(0f, 1f)]      
    public float hillL = 0.8f;
  
    public float Scale = 0.1f;          //масштаб(чем больше число тем больше отдаление)
    public float Scale2 = 0.01f;
    public int Height = 50;
    public int Width = 50;

    int seedX;
    int seedY;

    Vector2 a = new Vector2(0f, 0f);
    void Start()
    {
        seedX = Random.Range(0, 99999);
        seedY = Random.Range(0, 99999);

        for(int i = seedY; i < seedY + Height; i++)         // y
        {
            for(int q = seedX; q < seedX + Width; q++)     // x
            {
                transform.position = a;                                                                                             //инициализация памяти
                if(Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f < oceanL)              //Mathf.PerlinNoise() выдает от 0 до 1
                {
                    Instantiate(ocean, transform.position, Quaternion.identity);                                                    //создает копию префаба в определенных кордах          
                }else if(Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f < seeL)
                {
                    Instantiate(see, transform.position, Quaternion.identity);    
                }else if(Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f < beachL)
                {
                    Instantiate(beach, transform.position, Quaternion.identity);    
                }else if(Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f < plaineL)
                {
                    Instantiate(plaine, transform.position, Quaternion.identity);    
                }else if(Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f < hillL)
                {
                    Instantiate(hill, transform.position, Quaternion.identity);    
                }else
                {
                    Instantiate(mountain, transform.position, Quaternion.identity);    
                }
                a.x += 1f;                                                                 //переход на клетку вправо
            }
            if((i-seedY)%2 == 1)
            {
                a.x = 0f;       //сдвиг на пол клетки
            }
            else
            {
                a.x = 0.5f;     //сдвиг на пол клетки
            }
            a.y -= 1f;          //спускаемся на строку ниже
        }
    }
}




