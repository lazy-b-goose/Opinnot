using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Random = UnityEngine.Random;      // тк 2 библиотеки конфликтуют

[Serializable]
public class DataHexsesGen
{
    public int xy;
    public int Type;
 /* [постройка(0 - нету; 1 - дом; 2 - завод; 3 - шахта; 4 - поле; 5 - окоп; 6 - авиапорт; 10 - дорога)] 
    [тип территории(0 - ничейная)]
    [руды(0 - нету; 1 - камень; 2 - железо; 3 - золото)]
    [тип рельефа(1 - океан; 2 - море; 3 - пляж; 4 - равнина; 5 - холм; 6 - гора)]    */
}

[Serializable]
public class HexsesList
{
    public List<DataHexsesGen> hexses = new List<DataHexsesGen>();    // посредник для сохранения
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

    public GameObject Home;
    public GameObject Factory;
    public GameObject Mine;
    public GameObject Farm;
    public GameObject Trench;
    public GameObject Airport;
    public GameObject Road;
    
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
    int OneOre;
    public int k = 500;
    
    Vector3 a = new Vector3(0f, 0f, 0f);

    [System.Serializable]       // загрузка данных
    public class Hexse
    {
        public int xy;
        public int Type;
    }

    [System.Serializable]       // загрузка данных
    public class DataHexses
    {
        public List<Hexse> hexses;
    }

    private DataHexses data;    // загрузка данных





    void Awake()
    {
        if(GameData.Generic){
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
                    OneOre = Random.Range(0, k);
                    Debug.Log(OneOre);

                    if(Mathf.PerlinNoise((q + 1000f)*ScaleOre, (i + 1000f)*ScaleOre) > 0.9f || OneOre < 105)
                    {
                        Type += 30;
                        beach = beachGold;
                        plaine = plaineGold;
                        hill = hillGold;
                    }
                    if(Mathf.PerlinNoise((q + 2000f)*ScaleOre, (i + 2000f)*ScaleOre) > 0.86f || OneOre < 90)
                    {
                        Type += 20;
                        beach = beachIron;
                        plaine = plaineIron;
                        hill = hillIron;
                    }
                    if(Mathf.PerlinNoise((q + 3000f)*ScaleOre, (i + 3000f)*ScaleOre) > 0.83f || OneOre < 60)
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
                    xy = 1000000 + System.Math.Abs((int)a.x) * 100 + System.Math.Abs((int)a.y);
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
        
        
        
        
        
        else{
            JsonDownload(); 
            for(int i = seedY; i < seedY + Height; i++)         // y
            {
                for(int q = seedX; q < seedX + Width; q++)      // x
                {
                    beach = beachF;
                    plaine = plaineF;
                    hill = hillF;
                    a.z = 0;
                    xy = 1000000 + System.Math.Abs((int)a.x) * 100 + System.Math.Abs((int)a.y);
                    Type = Found(xy);  // ищем тип гекса

                    if((Type/10)%10 == 3)       // проверяем наличие руд
                    {
                        beach = beachGold;
                        plaine = plaineGold;
                        hill = hillGold;
                    }
                    if((Type/10)%10 == 2)       // проверяем наличие руд
                    {
                        beach = beachIron;
                        plaine = plaineIron;
                        hill = hillIron;
                    }
                    if((Type/10)%10 == 1)       // проверяем наличие руд
                    {
                        beach = beachStone;
                        plaine = plaineStone;
                        hill = hillStone;
                    }

                    if(Type % 10 == 1){
                        Instantiate(ocean, a, Quaternion.identity);         // создает копию префаба в определенных кордах     
                    }else if(Type % 10 == 2){
                        Instantiate(see, a, Quaternion.identity);    
                    }else if(Type % 10 == 3){
                        Instantiate(beach, a, Quaternion.identity);  
                    }else if(Type % 10 == 4){
                        Instantiate(plaine, a, Quaternion.identity);   
                    }else if(Type % 10 == 5){
                        Instantiate(hill, a, Quaternion.identity);   
                    }else{
                        Instantiate(mountain, a, Quaternion.identity);    
                    }

                    a.z = - 1f;
                    if((Type / 1000) % 10 == 1){
                        Home.name = xy.ToString(); 
                        Instantiate(Home, a, Quaternion.identity);         // здания     
                    }else if((Type / 1000) % 10 == 2){
                        Factory.name = xy.ToString(); 
                        Instantiate(Factory, a, Quaternion.identity);    
                    }else if((Type / 1000) % 10 == 3){
                        Mine.name = xy.ToString(); 
                        Instantiate(Mine, a, Quaternion.identity);  
                    }else if((Type / 1000) % 10 == 4){
                        Farm.name = xy.ToString(); 
                        Instantiate(Farm, a, Quaternion.identity);   
                    }else if((Type / 1000) % 10 == 5){
                        Trench.name = xy.ToString(); 
                        Instantiate(Trench, a, Quaternion.identity);   
                    }else if((Type / 1000) % 10 == 6){
                        Airport.name = xy.ToString(); 
                        Instantiate(Airport, a, Quaternion.identity);    
                    }
                    if(Type / 10000 == 1){
                        a.z = - 1.5f;
                        Road.name = 'R' + xy.ToString(); 
                        Instantiate(Road, a, Quaternion.identity);
                        a.z = - 1f;
                    }
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
        }
    }





    float Noize(int q, int i)
    {
    return Mathf.PerlinNoise(q*Scale, i*Scale)*0.8f + Mathf.PerlinNoise(q*Scale2, i*Scale2)*0.2f;
    }

    void JsonDownload(){
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");        // получение "ссылки" на файл
        string json = File.ReadAllText(DataLocation);               // "запись" всего файла
        data = JsonConvert.DeserializeObject<DataHexses>(json);     // систематизация текста
    }
    int Found(int xy){
        Hexse hex = data.hexses.FirstOrDefault(h => h.xy == xy);    // поиск Type с подходящим xy
        return hex.Type;                                        // берем значение Type
    }

    public void SaveHexs(int xy, int Type, HexsesList dataList){
        DataHexsesGen data = new DataHexsesGen();
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