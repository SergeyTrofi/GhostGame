using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Rival : GeneralObjects
    {
        public float Speed;
        private float leftBoundary;
        private float rightBoundary;

        public Rival(PointF pos) : base(pos)
        {
            Sprite = Properties.Resources.rival;
            SizeX = 37;
            SizeY = 37;
            Transform = new Transform(pos, new Size(SizeX, SizeY));
            Speed = 2; // Задаем начальную скорость
            leftBoundary = 0;
            rightBoundary = float.MaxValue;
        }

        public void SetBoundaries(float left, float right)
        {
            leftBoundary = left;
            rightBoundary = right;
        }

        public void Move()
        {
            Transform.Position = new PointF(Transform.Position.X + Speed, Transform.Position.Y);

            // Поменять направление движения при достижении границы
            if (Transform.Position.X <= leftBoundary || Transform.Position.X + Transform.Size.Width >= rightBoundary)
            {
                Speed = -Speed;
            }
        }
    }
}
