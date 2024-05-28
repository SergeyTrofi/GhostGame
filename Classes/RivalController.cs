using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class RivalController
    {
        public static List<Rival> rivals;
        public static int startPosX = 600;
        public static int startPosY = 168;
        public static void AddRival(PointF position)
        {
            Rival rival = new Rival(position);
            rivals.Add(rival);
        }

        public static void GenerateStartSequencePipe()
        {
            for (int i = 0; i < 2; i++)
            {
                startPosX += 500;
                PointF position = new PointF(startPosX, startPosY);
                Rival rival = new Rival(position);
                rivals.Add(rival);
            }
        }
        public static void Init()
        {
            rivals = new List<Rival>();
            AddRival(new PointF(800, 168));
            startPosX = 800;
            GenerateStartSequencePipe();
        }
    }
}
