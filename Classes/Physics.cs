using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAME.Classes;

namespace GAME.Classes
{
    public class Physics
    {
        public Transform Transform;

        public float Gravity { get; set; }
        private float a;
        public float XMotion { get; set; }
        public bool IsJumping = false;

        public Physics(PointF position, Size size)
        {
            Transform = new Transform(position, size);
            Gravity = 0;
            a = 0.4f;
            XMotion = 0;
            IsJumping = false; // Initialize IsJumping
        }

        public void ApplyPhysics()
        {
            if (Transform.Position.Y > 176)
            {
                Transform.Position.Y = 176;
                IsJumping = false;
            }
            CalculatePhysics();
            Collide();
        }

        private void CalculatePhysics()
        {
            if (XMotion != 0)
            {
                Transform.Position = new PointF(Transform.Position.X + XMotion, Transform.Position.Y);
            }
            if (Transform.Position.Y < 176)
            {
                Transform.Position = new PointF(Transform.Position.X, Transform.Position.Y + Gravity);
                Gravity += a; // Ускорение под действием гравитации
            }

        }

        private void Collide()
        {

            if (RoadController.Roads.Count > 0)
            {
                var firstRoad = RoadController.Roads.First().Transform;
                var lastRoad = RoadController.Roads.Last().Transform;

                float minX = firstRoad.Position.X + 50;
                float maxX = lastRoad.Position.X - lastRoad.Size.Width;

                CheckCollisionsWithPlatforms();
                CheckCollisionsWithPipes();
                CheckCollisionsWithRivals();
                EnsureWithinGameBounds(minX, maxX);

            }
        }

        private void CheckCollisionsWithPlatforms()
        {
            foreach (var platform in PlatformController.Platforms)
            {
                if (IsVerticalCollision(platform.Transform))
                {
                    ResolveVerticalCollision(platform.Transform);
                }
            }
        }

        private bool CheckCollisionsWithPipes()
        {
            bool isOnPipe = false; // Флаг, чтобы отслеживать, находится ли игрок на трубе

            foreach (var pipe in PipeController.Pipes)
            {
                if (IsVerticalCollision(pipe.Transform))
                {
                    ResolveVerticalCollision(pipe.Transform);
                    isOnPipe = true; // Игрок на трубе
                }
                if (IsHorizontalCollision(pipe.Transform))
                {
                    ResolveHorizontalCollision(pipe.Transform);
                }
            }

            return isOnPipe; // Возвращаем true, если игрок на трубе, иначе false
        }


        private void CheckCollisionsWithRivals()
        {
            var rivals = new List<Rival>(RivalController.Rivals); // Создаем копию списка

            foreach (var rival in rivals)
            {
                // Установим границы движения для врага
                float leftBoundary = float.MinValue;
                float rightBoundary = float.MaxValue;

                foreach (var pipe in PipeController.Pipes)
                {
                    if (pipe.Transform.Position.X < rival.Transform.Position.X && pipe.Transform.Position.X + pipe.Transform.Size.Width > leftBoundary)
                    {
                        leftBoundary = pipe.Transform.Position.X + pipe.Transform.Size.Width;
                    }

                    if (pipe.Transform.Position.X > rival.Transform.Position.X && pipe.Transform.Position.X < rightBoundary)
                    {
                        rightBoundary = pipe.Transform.Position.X;
                    }
                }

                rival.SetBoundaries(leftBoundary, rightBoundary);

                // Двигаем врагов
                rival.Move();

                if (CheckIfFellOn(rival))
                {
                    RivalController.Rivals.Remove(rival); // Удаляем врага, если персонаж упал на него
                }

                if (IsHorizontalCollision(rival.Transform) || RivalController.Rivals.Count <= 0)
                {
                    OnRivalCollision(); 
                }
            }
        }


        private bool CheckIfFellOn(Rival rival)
        {
            return (Transform.Position.Y + Transform.Size.Height >= rival.Transform.Position.Y - 5) &&
                   (Transform.Position.X + Transform.Size.Width > rival.Transform.Position.X) &&
                   (Transform.Position.X < rival.Transform.Position.X + rival.Transform.Size.Width);
        }

        private void EnsureWithinGameBounds(float minX, float maxX)
        {
            if (Transform.Position.X < minX)
            {
                Transform.Position = new PointF(minX, Transform.Position.Y);
                XMotion = 0;
            }
            else if (Transform.Position.X + Transform.Size.Width > maxX)
            {
                Transform.Position = new PointF(maxX - Transform.Size.Width, Transform.Position.Y);
                XMotion = 0;
            }
        }
        private bool IsHorizontalCollision(Transform other)
        {
            return (Transform.Position.X + Transform.Size.Width > other.Position.X) &&
                   (Transform.Position.X < other.Position.X + other.Size.Width) &&
                   (Transform.Position.Y + Transform.Size.Height > other.Position.Y) &&
                   (Transform.Position.Y < other.Position.Y + other.Size.Height);
        }
        private bool IsVerticalCollision(Transform other)
        {
            return (Transform.Position.Y + Transform.Size.Height > other.Position.Y) &&
                   (Transform.Position.Y < other.Position.Y + other.Size.Height) &&
                   (Transform.Position.X + Transform.Size.Width > other.Position.X) &&
                   (Transform.Position.X < other.Position.X + other.Size.Width);
        }
        private void ResolveHorizontalCollision(Transform other)
        {
            if (XMotion > 0) // Игрок движется вправо
            {
                Transform.Position = new PointF(other.Position.X - Transform.Size.Width, Transform.Position.Y);
                XMotion = 0; // Остановить движение
            }
            else if (XMotion < 0) // Игрок движется влево
            {
                Transform.Position = new PointF(other.Position.X + other.Size.Width, Transform.Position.Y);
                XMotion = 0; // Остановить движение
            }
        }
        private void ResolveVerticalCollision(Transform other)
        {
            if (Gravity > 0) // Игрок падает
            {
                Transform.Position = new PointF(Transform.Position.X, other.Position.Y - Transform.Size.Height);
                IsJumping = false;
            }
            else if (Gravity < 0 && IsJumping) // Игрок прыгает, и находится в воздухе
            {
                Transform.Position = new PointF(Transform.Position.X, other.Position.Y + Transform.Size.Height);
            }
        }

        public void ApplyJump()
        {
            if (!IsJumping)
            {
                AddForce();
                IsJumping = true;
            }
        }

        public void CalculateJump()
        {
            if (IsJumping)
            {
                if (Transform.Position.Y <= 176)
                {
                    Transform.Position = new PointF(Transform.Position.X, Transform.Position.Y + Gravity);
                    Gravity += a;
                }
                else
                {
                    IsJumping = false;
                }

                if (Transform.Position.Y >= 176 && Gravity > 0)
                {
                    AddForce();
                }
            }
        }

        private void AddForce()
        {
            Gravity = -10;
        }

        private void OnRivalCollision()
        {
            Form1.RestartGame();
        }
    }
}