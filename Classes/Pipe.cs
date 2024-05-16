using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Pipe
    {
        Image sprite;
        public Transform transform;

        public int sizeX;
        public int sizeY;

        public Pipe(PointF pos)
        {
            sprite = Properties.Resources.pipe;
            sizeX = 40;
            sizeY = 40;
            transform = new Transform(pos, new Size(sizeX, sizeY));
        }
        public void DrawSprite(Graphics g)
        {
            g.DrawImage(sprite, transform.position.X, transform.position.Y, transform.size.Width, transform.size.Height);
        }
    }
}
