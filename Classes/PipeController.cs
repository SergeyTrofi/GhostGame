﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class PipeController
    {
        public static List<Pipe> Pipes;
        public static int StartRoadPosX = 800;
        public static int StartRoadPosY = 168;
        public static void AddPipe(PointF position)
        {
            Pipe pipe = new Pipe(position);
            Pipes.Add(pipe);
        }

        public static void GenerateStartSequencePipe()
        {
            for (int i = 0; i < 2; i++)
            {
                StartRoadPosX += 800;
                PointF position = new PointF(StartRoadPosX, StartRoadPosY);
                Pipe pipe = new Pipe(position);
                Pipes.Add(pipe);
            }
        }
        public static void Init()
        {
            Pipes = new List<Pipe>();
            AddPipe(new PointF(700, 168));
            StartRoadPosX = 400;
            GenerateStartSequencePipe();
        }
    }
}
