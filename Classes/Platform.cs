using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Platform
    {
        Image sprite;
        public Transform transform;

        public int sizeX;
        public int sizeY;

        public bool isTouchByPlayer;
        
        public Platform(PointF pos)
        {
            Random rnd = new Random();

            sprite = Properties.Resources.platformTwo;
            sizeX = 32;
            sizeY = 16;
            transform = new Transform(pos, new Size(sizeX, sizeY));
            isTouchByPlayer = false;
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(sprite, transform.position.X, transform.position.Y, transform.size.Width, transform.size.Height);
        }
    }
}
