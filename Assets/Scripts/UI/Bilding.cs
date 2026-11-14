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
        x = GameData.XY / 100;          // конвертируем XY в Vector2
        y = GameData.XY % 100;
        Vect.x = x;
        Vect.y = -y;
        if(y % 2 == 1){                 // смещение на пол клетки
            Vect.x += 0.5f;
        }

        if(a == 10){            
        }else{
            Obj.name = GameData.XY.ToString();         // называем объект его же координатой
        }
        
        Type = Found(GameData.XY);
        if(a == 10 && Type < 10000){                        // если хотят построить дорогу и дороги нет
            Obj.name = 'R' + GameData.XY.ToString();        // называем объект его же координатой (дорога)
            Vect.z = - 1f;                                  // чтобы дорога была под зданием
            Instantiate(Obj, Vect, Quaternion.identity);                                                        // строительство дороги
            Vect.z = - 1.5f;
            Type += 10000;                                  // записываем дорогу в клетку
        }else if(a == 10 && Type > 10000){                  // если хотят построить дорогу и дорога есть
            Debug.Log("Дорога уже построена");
        }else if(a < 10 && (Type < 1000 || (Type > 10000 && Type < 11000))){   // если хотят построить не дорогу и здания на клетке нет
            if (a == 3){
                if(Type % 100 > 9){
                    Obj.name = GameData.XY.ToString(); 
                    Instantiate(Obj, Vect, Quaternion.identity);
                    Type += a * 1000; 
                }else{Debug.Log("Шахту можно построить только на месторождении");}
            }else if(a == 4){
                if(Type % 10 == 4){
                    Obj.name = GameData.XY.ToString(); 
                    Instantiate(Obj, Vect, Quaternion.identity);
                    Type += a * 1000; 
                }else{Debug.Log("Ферму можно построить только на равнине");}
            }else{
                Obj.name = GameData.XY.ToString(); 
                Instantiate(Obj, Vect, Quaternion.identity);
                Type += a * 1000; 
            }
        }else if(a == 11){
                Destroy(GameObject.Find(GameData.XY.ToString() + "(Clone)"));
                Type = Type / 10000 + Type % 100;
        }else if(a == 12){
                Destroy(GameObject.Find("R" + GameData.XY.ToString() + "(Clone)"));
                Type = Type % 1000;
        }else{
            Debug.Log("Тут уже что-то построено");
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
