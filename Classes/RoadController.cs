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
        public static List<Road> roads;
        public static int startRoadPosX = 0;
        public static int startRoadPosY = 208;
        public static void AddRoad(PointF position)
        {
            Road road = new Road(position);
            roads.Add(road);
        }

        public static void GenerateStartSequence()
        {
            for (int i = 0; i < 16; i++)
            {
                startRoadPosX += 224;
                PointF position = new PointF(startRoadPosX, startRoadPosY);
                Road road = new Road(position);
                roads.Add(road);
            }
        }
    }
}
