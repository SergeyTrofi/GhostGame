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
            CalculatePhysics();
        }

        public void CalculatePhysics()
        {
            if (dx != 0)
            {
                transform.position.X += dx;
            }
            Collide();
        }

        public void Collide()
        {
            // Проверка выхода за границы игровой области на основе дорог
            if (RoadController.roads.Count > 0)
            {
                var firstRoad = RoadController.roads.First().transform;
                var lastRoad = RoadController.roads.Last().transform;

                float minX = firstRoad.position.X + 50;
                float maxX = lastRoad.position.X - lastRoad.size.Width;

                // Обработка коллизий с платформами
                for (int i = 0; i < PlatformController.platforms.Count; i++)
                {
                    var platform = PlatformController.platforms[i];

                    if (IsCollidingWith(platform.transform))
                    {
                        ResolveVerticalCollision(platform.transform);
                    }
                }

                // Обработка коллизий с трубами
                for (int i = 0; i < PipeController.pipes.Count; i++)
                {
                    var pipe = PipeController.pipes[i];

                    if (IsCollidingWith(pipe.transform))
                    {
                        ResolveVerticalCollision(pipe.transform);
                    }

                    if (IsHorizontalCollision(pipe.transform))
                    {
                        ResolveHorizontalCollision(pipe.transform);
                    }
                }

                // Проверка выхода за границы игровой области
                if (transform.position.X < minX)
                {
                    transform.position.X = minX;
                    dx = 0;
                }
                else if (transform.position.X + transform.size.Width > maxX)
                {
                    transform.position.X = maxX - transform.size.Width;
                    dx = 0;
                }
            }
        }

        private bool IsCollidingWith(Transform other)
        {
            return transform.position.X + transform.size.Width > other.position.X &&
                   transform.position.X < other.position.X + other.size.Width &&
                   transform.position.Y + transform.size.Height > other.position.Y &&
                   transform.position.Y < other.position.Y + other.size.Height;
        }

        private bool IsHorizontalCollision(Transform other)
        {
            return transform.position.X + transform.size.Width > other.position.X &&
                   transform.position.X < other.position.X + other.size.Width;
        }

        private void ResolveVerticalCollision(Transform other)
        {
            if (gravity > 0) // Если персонаж падает
            {
                transform.position.Y = other.position.Y - transform.size.Height; // Остановка на верхней части платформы/трубы
                isJumping = false;
            }
            else if (gravity < 0) // Если персонаж прыгает вверх и ударяется о низ платформы
            {

                transform.position.Y = other.position.Y + other.size.Height; // Остановка при ударе о низ платформы
            }
        }

        private void ResolveHorizontalCollision(Transform other)
        {
            if (dx > 0 && transform.position.Y >= 168) // Если движется вправо
            {
                transform.position.X = other.position.X - transform.size.Width; // Остановка перед левой стороной трубы
            }
            else if (dx < 0 && transform.position.Y >= 168) // Если движется влево
            {
                transform.position.X = other.position.X + other.size.Width; // Остановка перед правой стороной трубы
            }
        }
        public void ApplyJump()
        {
            if (!isJumping) // Начинаем прыжок только если его еще нет
            {
                gravity = -10;
                isJumping = true;
            }
        }

        public void CalculateJump()
        {
            if (isJumping)
            {
                // Проверяем, не достигли ли мы максимальной высоты прыжка
                if (transform.position.Y <= 176)
                {
                    transform.position.Y += gravity;
                    gravity += a; // Ускорение свободного падения
                }
                else
                {
                    isJumping = false; // Заканчиваем прыжок
                }

                // Проверка на коллизию с землей
                if (transform.position.Y >= 176 && gravity > 0)
                {
                    AddForce();
                }
            }
        }

        public void AddForce()
        {
            gravity = -10; // Применяем силу прыжка
        }
    }
}

