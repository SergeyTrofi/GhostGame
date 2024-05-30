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
        public Transform transform;
        
        public float Gravity;
        float a;
        public float XMotion;
        public bool IsJumping = false;  

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            Gravity = 0;
            a = 0.4f;
            XMotion = 0;
        }

        public void ApplyPhysics()
        {
            if (transform.Position.Y > 176)
            {
                transform.Position.Y = 176;
                IsJumping = false;
            }
            CalculatePhysics();
            Collide();
        }

        private void CalculatePhysics()
        {
            if (XMotion != 0)
            {
                transform.Position = new PointF(transform.Position.X + XMotion, transform.Position.Y);
            }
            if (transform.Position.Y < 176)
            {
                transform.Position = new PointF(transform.Position.X, transform.Position.Y + Gravity);
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
                while (IsHorizontalCollision(platform.Transform))
                {
                    ResolveVerticalCollision(platform.Transform);
                }
            }
        }

        private bool CheckCollisionsWithPipes()
        {
            foreach (var pipe in PipeController.Pipes)
            {
                if (IsHorizontalCollision(pipe.Transform))
                {
                    // Проверяем вертикальное столкновение для корректировки позиции игрока по оси Y
                    if (IsVerticalCollision(pipe.Transform))
                    {
                        ResolveVerticalCollision(pipe.Transform);
                        return true; // Игрок на трубе
                    }
                    else
                    {
                        ResolveHorizontalCollision(pipe.Transform);
                    }
                }
                
            }
            return false; // Игрок не на трубе
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

                if (IsHorizontalCollision(rival.Transform))
                {
                    OnRivalCollision(); // Обработка столкновения с врагом сбоку
                }
            }
        }


        private bool CheckIfFellOn(Rival rival)
        {
            return (transform.Position.Y + transform.Size.Height >= rival.Transform.Position.Y - 5) &&
                   (transform.Position.X + transform.Size.Width > rival.Transform.Position.X) &&
                   (transform.Position.X < rival.Transform.Position.X + rival.Transform.Size.Width);
        }

        private void EnsureWithinGameBounds(float minX, float maxX)
        {
            if (transform.Position.X < minX)
            {
                transform.Position = new PointF(minX, transform.Position.Y);
                XMotion = 0;
            }
            else if (transform.Position.X + transform.Size.Width > maxX)
            {
                transform.Position = new PointF(maxX - transform.Size.Width, transform.Position.Y);
                XMotion = 0;
            }
        }
        private bool IsHorizontalCollision(Transform other)
        {
            return (transform.Position.X + transform.Size.Width > other.Position.X) &&
                   (transform.Position.X < other.Position.X + other.Size.Width) &&
                   (transform.Position.Y + transform.Size.Height > other.Position.Y) &&
                   (transform.Position.Y < other.Position.Y + other.Size.Height);
        }
        private bool IsVerticalCollision(Transform other)
        {
            return (transform.Position.Y + transform.Size.Height > other.Position.Y) &&
                   (transform.Position.Y < other.Position.Y + other.Size.Height) &&
                   (transform.Position.X + transform.Size.Width > other.Position.X) &&
                   (transform.Position.X < other.Position.X + other.Size.Width);
        }
        private void ResolveHorizontalCollision(Transform other)
        {
            if (XMotion > 0) // Игрок движется вправо
            {
                transform.Position = new PointF(other.Position.X - transform.Size.Width, transform.Position.Y);
                XMotion = 0; // Остановить движение
            }
            else if (XMotion < 0) // Игрок движется влево
            {
                transform.Position = new PointF(other.Position.X + other.Size.Width, transform.Position.Y);
                XMotion = 0; // Остановить движение
            }
        }
        private void ResolveVerticalCollision(Transform other)
        {
            if (Gravity > 0) 
            {
                transform.Position = new PointF(transform.Position.X, other.Position.Y - transform.Size.Height);
                IsJumping = false;
            }
            else if (Gravity < 0) 
            {
                transform.Position = new PointF(transform.Position.X, other.Position.Y + transform.Size.Height);
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
                if (transform.Position.Y <= 176)
                {
                    transform.Position = new PointF(transform.Position.X, transform.Position.Y + Gravity);
                    Gravity += a;
                }
                else
                {
                    IsJumping = false;
                }

                if (transform.Position.Y >= 176 && Gravity > 0)
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

