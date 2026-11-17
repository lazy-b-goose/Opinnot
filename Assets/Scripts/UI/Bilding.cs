using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Bilding : MonoBehaviour
{
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

    public int a;               // номер постройки
    int Type;
    public GameObject Obj;      // требуемый объект
    int x;
    int y;
     Vector3 Vect;


    public void Bild(){

        JsonDownload();                 // инициализируем данные
        Vect.z = - 1.5f;
        x = (GameData.XY / 100) % 1000;          // конвертируем XY в Vector2
        y = GameData.XY % 100;
        Vect.x = x;
        Vect.y = -y;
        if(y % 2 == 1){                 // смещение на пол клетки
            Vect.x += 0.5f;
        }
        Type = Found(GameData.XY);

        switch (a){
            case 1:
                if((Type / 1000) % 10 == 0){
                    Obj.name = GameData.XY.ToString();                  // называем объект его же координатой
                    Instantiate(Obj, Vect, Quaternion.identity);
                    Type += 1000;                                       // дом
                    GameData.Stone -= 20;
                }else{
                    Debug.Log("Тут уже что-то построено");
                }break;

            case 2:
                if((Type / 1000) % 10 == 0){
                    Obj.name = GameData.XY.ToString();                  // называем объект его же координатой
                    Instantiate(Obj, Vect, Quaternion.identity);
                    Type += 2000;                                       // завод
                    GameData.Stone -= 60;
                    GameData.Iron -= 30;
                }else{
                    Debug.Log("Тут уже что-то построено");
                }break;

            case 3:
                if((Type / 1000) % 10 == 0){
                    if((Type / 10) % 10 != 0){
                        Obj.name = GameData.XY.ToString();              // называем объект его же координатой
                        Instantiate(Obj, Vect, Quaternion.identity);
                        Type += 3000;                                   // шахта
                        GameData.Stone -= 50;
                        GameData.Iron -= 20;
                    }else{
                            Debug.Log("Шахту можно построить только на месторождении");
                        }
                }else{
                    Debug.Log("Тут уже что-то построено");
                }break;

            case 4:
                if((Type / 1000) % 10 == 0){
                    if(Type % 10 == 4){
                        Obj.name = GameData.XY.ToString();              // называем объект его же координатой
                        Instantiate(Obj, Vect, Quaternion.identity);
                        Type += 4000;                                   // ферма
                        GameData.Stone -= 10;
                        }else{
                            Debug.Log("Ферму можно построить только на равнине");
                        }
                }else{
                    Debug.Log("Тут уже что-то построено");
                }break;
            
            case 5:
                if((Type / 1000) % 10 == 0){
                    Obj.name = GameData.XY.ToString();                  // называем объект его же координатой
                    Instantiate(Obj, Vect, Quaternion.identity);
                    Type += 5000;                                       // окоп
                    GameData.Stone -= 10;
                }else{
                    Debug.Log("Тут уже что-то построено");
                }break;

            case 6:
                if((Type / 1000) % 10 == 0){
                    Obj.name = GameData.XY.ToString();                  // называем объект его же координатой
                    Instantiate(Obj, Vect, Quaternion.identity);
                    Type += 6000;                                       // аэропорт
                    GameData.Stone -= 150;
                    GameData.Iron -= 50;
                }else{
                    Debug.Log("Тут уже что-то построено");
                }break;

            case 10:
                if(Type < 10000){                                   // если хотят построить дорогу и дороги нет
                    Obj.name = 'R' + GameData.XY.ToString();        // называем объект его же координатой (дорога)
                    Vect.z = - 1f;                                  // чтобы дорога была под зданием
                    Instantiate(Obj, Vect, Quaternion.identity);    // строительство дороги
                    Vect.z = - 1.5f;
                    Type += 10000;                                  // записываем дорогу в БД
                    GameData.Stone -= 5;
                }else{
                    Debug.Log("Дорога уже построена");              // если хотят построить дорогу и дорога есть
                }break;

            case 11:
                Destroy(GameObject.Find(GameData.XY.ToString() + "(Clone)"));
                Type = Type / 10000 + Type % 100;
                break;
            
            case 12:
                Destroy(GameObject.Find("R" + GameData.XY.ToString() + "(Clone)"));
                Type = Type % 1000;
                break;
        }
        
        SaveHexs(GameData.XY, Type);
        SaveToFile();
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

    public void SaveHexs(int xy, int newType){
        Hexse hex = data.hexses.FirstOrDefault(h => h.xy == xy);

        if (hex == null)
        {
            hex = new Hexse();
            hex.xy = xy;
            data.hexses.Add(hex);
        }

        hex.Type = newType;
    }

    public void SaveToFile(){
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(DataLocation, json);
    }
}
