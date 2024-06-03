using System.Collections.Generic;
using System.Drawing;

namespace GAME.Classes
{
    public class GameController<T>
    {
        public static List<T> Objects;
        public static int StartPosX;
        public static int StartPosY;

        public static void AddObject(PointF position, List<T> objects)
        {

        }

        public static void GenerateStartSequence(List<T> objects)
        {
            
        }

        public static void Init(List<T> objects)
        {
            Objects = objects;
        }
    }
}
