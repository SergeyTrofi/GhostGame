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
        public static List<Platform> Platforms;
        public static int StartPlatformPosX = 390;

        public static void AddPlatform(PointF position)
        {
            Platform platform = new Platform(position);
            Platforms.Add(platform);
        }

        public static void GenerateStartSequence()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = rnd.Next(300, 2750);
                int y = rnd.Next(80, 150);
                StartPlatformPosX += x;
                PointF position = new PointF(StartPlatformPosX, y);
                Platform platform = new Platform(position);
                Platforms.Add(platform);
            }
        }
        
        public static void Init()
        {
            Platforms = new List<Platform>();
            AddPlatform(new PointF(350, 90)); // Изменяем координаты платформы
            StartPlatformPosX = 400;
            GenerateStartSequence();
        }
    }

}
