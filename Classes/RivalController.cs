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
        public static List<Rival> Rivals;
        public static int StartPosX = 600;
        public static int StartPosY = 176;
        public static void AddRival(PointF position)
        {
            Rival rival = new Rival(position);
            Rivals.Add(rival);
        }

        public static void GenerateStartSequencePipe()
        {
            for (int i = 0; i < 2; i++)
            {
                StartPosX += 500;
                PointF position = new PointF(StartPosX, StartPosY);
                Rival rival = new Rival(position);
                Rivals.Add(rival);
            }
        }
        public static void Init()
        {
            Rivals = new List<Rival>();
            AddRival(new PointF(800, 176));
            StartPosX = 800;
            GenerateStartSequencePipe();
        }
    }
}
