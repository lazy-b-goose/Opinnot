using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

using Random = UnityEngine.Random;      // тк 2 библиотеки конфликтуют

[Serializable]
public class DataHexses
{
    public int xy;
    public int Type;
    /*[тип территории(0 - ничейная)]*100 +
    [руды(0 - нету; 1 - камень; 2 - железо; 3 - золото)] +
    [тип рельефа(1 - океан; 2 - море; 3 - пляж; 4 - равнина; 5 - холм; 6 - гора)]     */
}

[Serializable]
public class HexsesList
{
    public List<DataHexses> hexses = new List<DataHexses>();    // посредник для сохранения
}

public class Generation : MonoBehaviour
{
    public GameObject ocean;            // префабы высот
    public GameObject see;
    public GameObject beachF;
    public GameObject plaineF;
    public GameObject hillF;
    public GameObject mountain;

    GameObject beach;                   // буферные префабы
    GameObject plaine;
    GameObject hill;
    int xy;
    int Type;

    public GameObject beachStone;           // префабы камня
    public GameObject plaineStone;
    public GameObject hillStone;
    public GameObject beachIron;            // префабы железа
    public GameObject plaineIron;
    public GameObject hillIron;
    public GameObject beachGold;            // префабы золота
    public GameObject plaineGold;
    public GameObject hillGold;
    
    [Range(0f, 1f)]                     // настройка размеров высот
    public float oceanL = 0.3f;
    [Range(0f, 1f)]     
    public float seeL = 0.4f;
    [Range(0f, 1f)] 
    public float beachL = 0.45f;
    [Range(0f, 1f)]     
    public float plaineL = 0.65f;
    [Range(0f, 1f)]      
    public float hillL = 0.8f;
  
    public float Scale = 0.1f;          // масштаб(чем больше число тем больше отдаление)
    public float Scale2 = 0.01f;
    public float ScaleOre = 0.2f;
    public int Height = 50;
    public int Width = 50;

    int seedX;
    int seedY;
    
    Vector2 a = new Vector2(0f, 0f);
    void Start()
    {
        HexsesList dataList = new HexsesList();             // данные расположения файла
        seedX = Random.Range(0, 99999);
        seedY = Random.Range(0, 99999);

        for(int i = seedY; i < seedY + Height; i++)         // y
        {
            for(int q = seedX; q < seedX + Width; q++)      // x
            {
                beach = beachF;
                plaine = plaineF;
                hill = hillF;
                xy = 0;
                Type = 0;

                if(Mathf.PerlinNoise((q + 1000f)*ScaleOre, (i + 1000f)*ScaleOre) > 0.9f)
                {
                    Type += 30;
                    beach = beachGold;
                    plaine = plaineGold;
                    hill = hillGold;
                }
                if(Mathf.PerlinNoise((q + 2000f)*ScaleOre, (i + 2000f)*ScaleOre) > 0.86f)
                {
                    Type += 20;
                    beach = beachIron;
                    plaine = plaineIron;
                    hill = hillIron;
                }
                if(Mathf.PerlinNoise((q + 3000f)*ScaleOre, (i + 3000f)*ScaleOre) > 0.83f)
                {
                    Type += 10;
                    beach = beachStone;
                    plaine = plaineStone;
                    hill = hillStone;
                }

                if(Noize(q, i) < oceanL){                               // Mathf.PerlinNoise() выдает от 0 до 1
                    Instantiate(ocean, a, Quaternion.identity);         // создает копию префаба в определенных кордах     
                    Type += 1;
                }else if(Noize(q, i) < seeL){
                    Instantiate(see, a, Quaternion.identity);    
                    Type += 2;
                }else if(Noize(q, i) < beachL){
                    Instantiate(beach, a, Quaternion.identity);  
                    Type += 3;
                }else if(Noize(q, i) < plaineL){
                    Instantiate(plaine, a, Quaternion.identity);   
                    Type += 4;
                }else if(Noize(q, i) < hillL){
                    Instantiate(hill, a, Quaternion.identity);   
                    Type += 5;
                }else{
                    Instantiate(mountain, a, Quaternion.identity);    
                    Type += 6;
                }
                xy = System.Math.Abs(((int)a.x)) * 100 + System.Math.Abs((int)a.y);
                SaveHexs(xy, Type, dataList);

                a.x += 1f;                                                                 // переход на клетку вправо
            }
            if((i-seedY)%2 == 1)
            {
                a.x = 0f;       // сдвиг на пол клетки
            }
            else
            {
                a.x = 0.5f;     // сдвиг на пол клетки
            }
            a.y -= 1f;          // спускаемся на строку ниже
        }
        SaveToFile(dataList);
    }

    float Noize(int q, int i)
    {
    return Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f;
    }

    public void SaveHexs(int xy, int Type, HexsesList dataList){
        DataHexses data = new DataHexses();
        data.xy = xy;
        data.Type = Type;
        dataList.hexses.Add(data);
    }

    public void SaveToFile(HexsesList dataList){
        string DataLocation;
        DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");
        File.WriteAllText(DataLocation, JsonUtility.ToJson(dataList, true));
    }
}






