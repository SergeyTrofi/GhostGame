using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAME.Classes;
namespace GAME.Classes
{
    public class Physics
    {
        public Transform transform;
        
        public float gravity;
        float a;
        public float dx;
        public static bool isJumping = false;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            a = 0.4f;
            dx = 0;
        }

        public void ApplyPhysics()
        {
            if (transform.position.Y > 176)
            {
                transform.position.Y = 176;
                isJumping = false;
            }
            CalculatePhysics();
            Collide();
        }

        private void CalculatePhysics()
        {
            if (dx != 0)
            {
                transform.position = new PointF(transform.position.X + dx, transform.position.Y);
            }
        }

        private void Collide()
        {
            
            if (RoadController.roads.Count > 0)
            {
                var firstRoad = RoadController.roads.First().transform;
                var lastRoad = RoadController.roads.Last().transform;

                float minX = firstRoad.position.X + 50;
                float maxX = lastRoad.position.X - lastRoad.size.Width;
                
                CheckCollisionsWithPlatforms();
                CheckCollisionsWithPipes();
                CheckCollisionsWithRivals();
                EnsureWithinGameBounds(minX, maxX);
                
            }
        }

        private void CheckCollisionsWithPlatforms()
        {
            foreach (var platform in PlatformController.platforms)
            {
                if (IsCollidingWith(platform.transform))
                {
                    ResolveVerticalCollision(platform.transform);
                    
                }
            }
        }

        private void CheckCollisionsWithPipes()
        {
            foreach (var pipe in PipeController.pipes)
            {
                if (IsCollidingWith(pipe.transform))
                {   
                    ResolveVerticalCollision(pipe.transform);
                    
                }
                
                if (IsHorizontalCollision(pipe.transform))
                {
                    ResolveHorizontalCollision(pipe.transform);
                    
                }
            }
        }

        private void CheckCollisionsWithRivals()
        {
            for (int i = RivalController.rivals.Count - 1; i >= 0; i--)
            {
                var rival = RivalController.rivals[i];
                rival.Move(); // Двигаем врагов

                /*if (IsCollidingWith(rival.transform))
                {
                    ResolveVerticalCollision(rival.transform);
                }*/

                if (IsHorizontalCollision(rival.transform))
                {
                    //ResolveHorizontalCollision(rival.transform);
                    OnRivalCollision(); // Обработка столкновения с врагом сбоку
                }

                if (CheckIfFellOn(rival))
                {
                    RivalController.rivals.RemoveAt(i); // Удаляем врага, если персонаж упал на него
                }
            }
        }

        private bool CheckIfFellOn(Rival rival)
        {
            return (transform.position.Y + transform.size.Height <= rival.transform.position.Y) &&
                   (transform.position.X + transform.size.Width > rival.transform.position.X) &&
                   (transform.position.X < rival.transform.position.X + rival.transform.size.Width);
        }

        private void EnsureWithinGameBounds(float minX, float maxX)
        {
            if (transform.position.X < minX)
            {
                transform.position = new PointF(minX, transform.position.Y);
                dx = 0;
            }
            else if (transform.position.X + transform.size.Width > maxX)
            {
                transform.position = new PointF(maxX - transform.size.Width, transform.position.Y);
                dx = 0;
            }
        }

        private bool IsCollidingWith(Transform other)
        {
            return (transform.position.X + transform.size.Width > other.position.X) &&
                   (transform.position.X < other.position.X + other.size.Width) &&
                   (transform.position.Y + transform.size.Height > other.position.Y) &&
                   (transform.position.Y < other.position.Y + other.size.Height);
        }

        private bool IsHorizontalCollision(Transform other)
        {
            return (transform.position.X + transform.size.Width > other.position.X) &&
                   (transform.position.X < other.position.X + other.size.Width) &&
                   (transform.position.Y + transform.size.Height > other.position.Y) &&
                   (transform.position.Y < other.position.Y + other.size.Height);
        }

        private void ResolveVerticalCollision(Transform other)
        {
            if (gravity > 0) 
            {
                transform.position = new PointF(transform.position.X, other.position.Y - transform.size.Height);
                isJumping = false;
            }
            else if (gravity < 0) 
            {
                transform.position = new PointF(transform.position.X, other.position.Y + other.size.Height);
            }
        }

        private void ResolveHorizontalCollision(Transform other)
        {
            if (dx > 0) 
            {
                transform.position = new PointF(other.position.X - transform.size.Width, transform.position.Y);
            }
            else if (dx < 0) 
            {
                transform.position = new PointF(other.position.X + other.size.Width, transform.position.Y);
            }
            dx = 0;
        }

        public void ApplyJump()
        {
            if (!isJumping)
            {
                AddForce();
                isJumping = true;
            }
        }

        public void CalculateJump()
        {
            if (isJumping)
            {
                if (transform.position.Y <= 176)
                {
                    transform.position = new PointF(transform.position.X, transform.position.Y + gravity);
                    gravity += a;
                }
                else
                {
                    isJumping = false;
                }

                if (transform.position.Y >= 176 && gravity > 0)
                {
                    AddForce();
                }
            }
        }

        private void AddForce()
        {
            gravity = -10;
        }

        private void OnRivalCollision()
        {
            Form1.RestartGame();
        }
    }
}

