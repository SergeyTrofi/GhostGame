using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class PlatformController // если придет жопа, добавить статик
    {
        public static List<Platform> platforms;
        public static int startPlatformPosX = 390;

        public static void AddPlatform(PointF position)
        {
            Platform platform = new Platform(position);
            platforms.Add(platform);
        }

        public static void GenerateStartSequence()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = rnd.Next(300, 2750);
                int y = rnd.Next(80, 120);
                startPlatformPosX += x;
                PointF position = new PointF(startPlatformPosX, y);
                Platform platform = new Platform(position);
                platforms.Add(platform);
            }
        }

    }

}
