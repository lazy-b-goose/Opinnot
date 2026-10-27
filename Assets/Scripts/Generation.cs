using UnityEngine;

public class Generation : MonoBehaviour
{
    public GameObject ocean;            //префабы высот
    public GameObject see;
    public GameObject beachF;
    public GameObject plaineF;
    public GameObject hillF;
    public GameObject mountain;

    GameObject beach;
    GameObject plaine;
    GameObject hill;

    public GameObject beachStone;           //префабы камня
    public GameObject plaineStone;
    public GameObject hillStone;
    public GameObject beachIron;            //префабы железа
    public GameObject plaineIron;
    public GameObject hillIron;
    public GameObject beachGold;            //префабы золота
    public GameObject plaineGold;
    public GameObject hillGold;
    
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
    public float ScaleOre = 0.2f;
    public int Height = 50;
    public int Width = 50;

    int seedX;
    int seedY;

    int HexsNum = 0;
    public struct grid{                 //БД гексов
        public GameObject Hexs;
        public Vector2 xy;
        public int Type;                
        /*[тип территории(0 - ничейная)]*100 +
        [руды(0 - нету; 1 - камень; 2 - железо; 3 - золото)] +
        [тип рельефа(1 - океан; 2 - море; 3 - пляж; 4 - равнина; 5 - холм; 6 - гора)]                                                */
    }
    public grid[] HexsesData = new grid[10000];   //инициализация (значение нужно указывать вручную)            !!!

    Vector2 a = new Vector2(0f, 0f);
    void Start()
    {
        seedX = Random.Range(0, 99999);
        seedY = Random.Range(0, 99999);

        for(int i = seedY; i < seedY + Height; i++)         // y
        {
            for(int q = seedX; q < seedX + Width; q++)      // x
            {
                beach = beachF;
                plaine = plaineF;
                hill = hillF;

                if(Mathf.PerlinNoise((q + 1000f)*ScaleOre, (i + 1000f)*ScaleOre) > 0.9f)
                {
                    HexsesData[HexsNum].Type += 30;
                    beach = beachGold;
                    plaine = plaineGold;
                    hill = hillGold;
                }
                if(Mathf.PerlinNoise((q + 2000f)*ScaleOre, (i + 2000f)*ScaleOre) > 0.86f)
                {
                    HexsesData[HexsNum].Type += 20;
                    beach = beachIron;
                    plaine = plaineIron;
                    hill = hillIron;
                }
                if(Mathf.PerlinNoise((q + 3000f)*ScaleOre, (i + 3000f)*ScaleOre) > 0.83f)
                {
                    HexsesData[HexsNum].Type += 10;
                    beach = beachStone;
                    plaine = plaineStone;
                    hill = hillStone;
                }

                transform.position = a;                                                 //инициализация памяти
                if(Noize(q, i) < oceanL)                                                //Mathf.PerlinNoise() выдает от 0 до 1
                {
                    Instantiate(ocean, transform.position, Quaternion.identity);        //создает копию префаба в определенных кордах     
                    HexsesData[HexsNum].xy = a;  
                    HexsesData[HexsNum].Type += 1;
                }else if(Noize(q, i) < seeL)
                {
                    Instantiate(see, transform.position, Quaternion.identity);    
                    HexsesData[HexsNum].xy = a;  
                    HexsesData[HexsNum].Type += 2;
                }else if(Noize(q, i) < beachL)
                {
                    Instantiate(beach, transform.position, Quaternion.identity);  
                    HexsesData[HexsNum].xy = a;  
                    HexsesData[HexsNum].Type += 3;  
                }else if(Noize(q, i) < plaineL)
                {
                    Instantiate(plaine, transform.position, Quaternion.identity);   
                    HexsesData[HexsNum].xy = a;  
                    HexsesData[HexsNum].Type += 4; 
                }else if(Noize(q, i) < hillL)
                {
                    Instantiate(hill, transform.position, Quaternion.identity);   
                    HexsesData[HexsNum].xy = a;  
                    HexsesData[HexsNum].Type += 5; 
                }else
                {
                    Instantiate(mountain, transform.position, Quaternion.identity);    
                    HexsesData[HexsNum].xy = a;  
                    HexsesData[HexsNum].Type += 6;
                }
                a.x += 1f;                                                                 //переход на клетку вправо
                HexsNum++;
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

    float Noize(int q, int i)
    {
    return Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f;
    }
}






