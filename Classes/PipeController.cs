using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class PipeController
    {
        public static List<Pipe> pipes;
        public static int startRoadPosX = 400;
        public static int startRoadPosY = 168;
        public static void AddPipe(PointF position)
        {
            Pipe pipe = new Pipe(position);
            pipes.Add(pipe);
        }

        public static void GenerateStartSequencePipe()
        {
            for (int i = 0; i < 2; i++)
            {
                startRoadPosX += 800;
                PointF position = new PointF(startRoadPosX, startRoadPosY);
                Pipe pipe = new Pipe(position);
                pipes.Add(pipe);
            }
        }
    }
}
