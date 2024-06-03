using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class RoadController
    {
        public static List<Road> Roads;
        public static int StartRoadPosX = 0;
        public static int StartRoadPosY = 208;
        public static void AddRoad(PointF position)
        {
            Road road = new Road(position);
            Roads.Add(road);
        }

        public static void GenerateStartSequenceRoads()
        {
            for (int i = 0; i < 16; i++)
            {
                StartRoadPosX += 224;
                PointF position = new PointF(StartRoadPosX, StartRoadPosY);
                Road road = new Road(position);
                Roads.Add(road);
            }
        }
        public static void Init()
        {
            Roads = new List<Road>();
            AddRoad(new PointF(0, 208)); 
            StartRoadPosX = 0;
            GenerateStartSequenceRoads();
        }
        
    }
}
