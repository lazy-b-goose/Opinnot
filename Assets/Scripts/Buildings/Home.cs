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

    int XY;
    string XYst;
    bool T;
    string ppl;

    Vector2 a;

    void Start()
    {
        a = gameObject.transform.position;
        XY = 1000000 + System.Math.Abs((int)a.x) * 100 + System.Math.Abs((int)a.y);
        XYst = XY.ToString();

        JsonDownload();

        
        if ((a.x / 1) == 0)     
        {
            T = true;
        }
        else
        {
            T = false;
        }

        Sphere0(XY, T, XYst);
    }

    



    //                                   [
    public void Sphere0(int HomeXY, bool T, string XY)
    {
        int n = FoundPpl(HomeXY).Length;
        int pplHome = int.Parse(FoundPpl(HomeXY).Substring(n - 2, 2));           // количество безработных

        if(pplHome == 0){
            SearchWork(HomeXY, T, pplHome, XY);
        }
    }



    void SearchWork(int HomeXY, bool T, int pplHome, string XY){
        SphereWork(HomeXY, T, pplHome, XY);
    }



    void SphereWork(int HomeXY, bool T, int pplHome, string XY){// есть ли вокруг рабочие здания
        if(pplHome != 0){
            WorkHexs(HomeXY + 1, pplHome, XY);
            WorkHexs(HomeXY - 1, pplHome, XY);
            WorkHexs(HomeXY + 100, pplHome, XY);
            WorkHexs(HomeXY - 100, pplHome, XY);
        }
        if(T && pplHome != 0){
            WorkHexs(HomeXY + 99, pplHome, XY);
            WorkHexs(HomeXY - 101, pplHome, XY);
        }
        if(!T && pplHome != 0){
            WorkHexs(HomeXY + 101, pplHome, XY);
            WorkHexs(HomeXY - 99, pplHome, XY);
        }
    }



    //       координата гекса|кол-во безработных
    void WorkHexs(int WorkXY, int pplHome, string XY){// есть ли в гексе рабочие здания
        int a = (Found(WorkXY) / 1000) % 10;                                 // тип здания
        if (a == 2 || a == 3 || a == 4 || a == 6)                       // ищем здания работы
        {
            int n = FoundPpl(WorkXY).Length;
            string FpplWork = FoundPpl(WorkXY);
            int pplWork = int.Parse(FpplWork.Substring(n - 2, 2));         // кол-во вакансий

            if (pplHome >= pplWork)                                     // если безработных больше чем вакансий
            {
                pplHome -= pplWork;                                     // кол-во безработных уменьшается на кол-во вакансий

                bool N = true;
                for(int i = 0; i < n / 7; i++){                         // проверяем БД работы на наличие нашего дома
                    if(FpplWork.Substring(i * 7, 7) == XY){     // при нахождении запоминаем
                        N = false;
                    }
                }
                if(N){
                    FpplWork = XY + FpplWork.Substring(0, n - 2) + "00";    // добавляем в БД если не нашли
                }else{
                    FpplWork = FpplWork.Substring(0, n - 2) + "00";
                }

                SaveHexs(WorkXY, FpplWork);                                         // вакансий становится ноль
            }
            else
            {
                pplWork -= pplHome;                                     // кол-во вакансий уменьшается на кол-во безработных
                pplHome = 0;

                FpplWork = FpplWork.Substring(0, n - 2) + pplWork.ToString();
                SaveHexs(WorkXY, FpplWork);                             // сохранение кол-во вокансий
            }
        }
    }
    


    void SphereRoad(int HomeXY, bool T, int pplHome){// есть ли вокруг дороги
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
        int a = (Found(WorkXY) / 10000) % 10;                                 // тип здания
        if (a == 1)                       // ищем здания работы
        {
            int n = FoundPpl(WorkXY).Length;
            string FpplWork = FoundPpl(WorkXY);
        }
    }





    void JsonDownload()
    {
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");
        string json = File.ReadAllText(DataLocation);
        data = JsonConvert.DeserializeObject<DataHexsesHome>(json);
    }
    int Found(int xy)
    {
        HexseHome hex = data.hexses.FirstOrDefault(h => h.xy == xy);
        return hex.Type;
    }

    
    string FoundPpl(int xy)
    {
        HexseHome hex = data.hexses.FirstOrDefault(h => h.xy == xy);
        return hex.ppl;
    }


    public void SaveHexs(int xy, string newType)
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

    public void SaveToFile()
    {
        string DataLocation = Path.Combine(Application.dataPath, "DataHexses.json");

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(DataLocation, json);
    }
}
