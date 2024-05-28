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
        public Rival(PointF pos) : base(pos)
        {
            sprite = Properties.Resources.rival;
            sizeX = 35;
            sizeY = 35;
            transform = new Transform(pos, new Size(sizeX, sizeY));
        }
        public void Move()
        {
            if (transform.position.X <= 800 + transform.size.Width)
            {
                transform.position = new PointF(transform.position.X - 2, transform.position.Y);
            }
            if (transform.position.X >= 1600)
            {
                transform.position = new PointF(transform.position.X + 2, transform.position.Y);
            }
            
        }
    }
}
