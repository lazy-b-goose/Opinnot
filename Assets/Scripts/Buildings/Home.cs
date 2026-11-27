using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Home : MonoBehaviour
{
    [System.Serializable]
    public class HexseHome
    {
        public int xy;
        public int Type;
        public string ppl;
    }

    [System.Serializable]
    public class DataHexsesHome
    {
        public List<HexseHome> hexses;
    }

    private DataHexsesHome data;

    

    void Start()
    {
        Vector2 a = gameObject.transform.position;

        int HomeXY = 1000000 + System.Math.Abs((int)a.x) * 100 + System.Math.Abs((int)a.y);
        string HomeXYst = HomeXY.ToString();

        JsonDownload();
        SaveHexs(HomeXY, "50");
        
        bool T;
        if ((a.x / 1) == 0)     // true если х не дробный
        {
            T = true;
        }
        else
        {
            T = false;
        }

        Sphere0(HomeXY, T, HomeXYst);

        SaveToFile();
    }

    



    //                                   [
    public void Sphere0(int HomeXY, bool T, string HomeXYst)
    {
        int n = FoundPpl(HomeXY).Length;                                        // n - длинна ppl дома 
        int pplHome = int.Parse(FoundPpl(HomeXY).Substring(n - 2, 2));          // количество безработных

        if(pplHome != 0){                                                       // если есть безработные
            SearchWork(HomeXY, T, pplHome, HomeXYst);                           // поиск работы
        }
    }



    void SearchWork(int HomeXY, bool T, int pplHome, string HomeXYst){
        SphereWork(HomeXY, T, pplHome, HomeXYst);                               // поиск работы вокруг дома
    }



    void SphereWork(int HomeXY, bool T, int pplHome, string HomeXYst){// есть ли вокруг рабочие здания
        if(pplHome != 0){
            pplHome = WorkHexs(HomeXY + 1, pplHome, HomeXYst, HomeXY);
            pplHome = WorkHexs(HomeXY - 1, pplHome, HomeXYst, HomeXY);
            pplHome = WorkHexs(HomeXY + 100, pplHome, HomeXYst, HomeXY);
            pplHome = WorkHexs(HomeXY - 100, pplHome, HomeXYst, HomeXY);
        }
        if(T && pplHome != 0){                                                  // х целый
            pplHome = WorkHexs(HomeXY + 99, pplHome, HomeXYst, HomeXY);
            pplHome = WorkHexs(HomeXY - 101, pplHome, HomeXYst, HomeXY);
        }
        if(!T && pplHome != 0){                                                 // x дробный
            pplHome = WorkHexs(HomeXY + 101, pplHome, HomeXYst, HomeXY);
            pplHome = WorkHexs(HomeXY - 99, pplHome, HomeXYst, HomeXY);
        }
    }



    int WorkHexs(int WorkXY, int pplHome, string HomeXYst, int HomeXY){// есть ли в гексе рабочие здания
        int a = Found(WorkXY) / 1000 % 10;                                      // тип здания
        if (a == 2 || a == 3 || a == 4 || a == 6)                               // ищем здания работы
        {
            int n = FoundPpl(WorkXY).Length;                                    // длина ppl работы
            string FpplWork = FoundPpl(WorkXY);                                 // ppl работы
            int pplWork = int.Parse(FpplWork.Substring(n - 2, 2));              // кол-во вакансий

            int pplHomeAft;
            int pplWorkAft;
            int pplHomeWort;
            if (pplHome >= pplWork)                                             // если безработных больше чем вакансий
            {
                pplHomeAft = pplHome - pplWork;                                             // кол-во безработных уменьшается на кол-во вакансий
                pplWorkAft = 0;
                pplHomeWort = pplWork;
            }
            else
            {
                pplWorkAft = pplWork - pplHome;                                             // кол-во вакансий уменьшается на кол-во безработных
                pplHomeAft = 0;
                pplHomeWort = pplHome;
            }

            bool N = true;
            int I = 0;
            for(int i = 0; i < n / 9; i++){                                     // проверяем БД работы на наличие нашего дома
                if(FpplWork.Substring(i * 9, 9) == HomeXYst){                   // при нахождении запоминаем
                    N = false;
                    I = i;
                }
            }

            if(N){                                                              // добавляем в БД если не нашли
                FpplWork = HomeXYst + pplHomeWort.ToString() + FpplWork.Substring(0, n - 2) + pplWorkAft.ToString();    
            }else{                                                              // добавляем в БД сколько рабочих из этого дома
                FpplWork = FpplWork.Substring(0, I * 9 + 7)+ pplHomeWort.ToString() + FpplWork.Substring(I * 9 + 9, n - (I * 9 + 7)) + pplWorkAft.ToString();
            }
            if(pplWorkAft == 0){FpplWork += '0';}                                   // чтобы было 00 а не 0
            
            SaveHexs(WorkXY, FpplWork);



            string FpplHome = FoundPpl(HomeXY);

            n = FoundPpl(HomeXY).Length;  
            string WorkXYst = WorkXY.ToString();
            for(int i = 0; i < n / 9; i++){                                     // проверяем БД работы на наличие нашего дома
                if(FpplHome.Substring(i * 9, 9) == WorkXYst){                   // при нахождении запоминаем
                    N = false;
                    I = i;
                }
            }

            if(N){                                                              // добавляем в БД если не нашли
                FpplHome = WorkXYst + pplHomeWort.ToString() + FpplHome.Substring(0, n - 2) + pplHomeAft.ToString(); 
            }else{                                                              // добавляем в БД сколько рабочих на этом предприятии
                FpplHome = FpplHome.Substring(0, I * 9 + 7)+ pplHomeWort.ToString() + FpplHome.Substring(I * 9 + 9, n - (I * 9 + 7)) + pplHomeAft.ToString();
            }
            if(pplHomeAft == 0){FpplHome += '0';}                                   // чтобы было 00 а не 0

            SaveHexs(HomeXY, FpplHome);

        }
        return pplHome;
    }
    


    /*void SphereRoad(int HomeXY, bool T, int pplHome){// есть ли вокруг дороги
        if(pplHome != 0){
            RoadHexs(HomeXY + 1);
            RoadHexs(HomeXY - 1);
            RoadHexs(HomeXY + 100);
            RoadHexs(HomeXY - 100);

            if(T && pplHome != 0){
                RoadHexs(HomeXY + 99);
                RoadHexs(HomeXY - 101);
            }
            if(!T && pplHome != 0){
                RoadHexs(HomeXY + 101);
                RoadHexs(HomeXY - 99);
            }
        }
    }



    //       координата гекса
    void RoadHexs(int WorkXY){// есть ли в гексе дорога
        int a = (Found(WorkXY) / 10000) % 10;                                   // тип здания
        if (a == 1)                                                             // ищем здания работы
        {
            int n = FoundPpl(WorkXY).Length;
            string FpplWork = FoundPpl(WorkXY);
        }
    } */





    void JsonDownload() // загрузка json
    {
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");
        string json = File.ReadAllText(DataLocation);
        data = JsonConvert.DeserializeObject<DataHexsesHome>(json);
    }
    int Found(int xy) // загрузить Type
    {
        HexseHome hex = data.hexses.FirstOrDefault(h => h.xy == xy);
        return hex.Type;
    }

    
    string FoundPpl(int xy) // загрузить ppl 
    {
        HexseHome hex = data.hexses.FirstOrDefault(h => h.xy == xy);
        return hex.ppl;
    }


    public void SaveHexs(int xy, string newType) // сохранение ppl переменной
    {
        HexseHome hex = data.hexses.FirstOrDefault(h => h.xy == xy);

        if (hex == null)
        {
            hex = new HexseHome();
            hex.xy = xy;
            data.hexses.Add(hex);
        }

        hex.ppl = newType;
    }

    public void SaveToFile() // сохранение в json
    {
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(DataLocation, json);
    }
}
