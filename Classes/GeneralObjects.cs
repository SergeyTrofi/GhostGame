using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class GeneralObjects
    {
        public Image sprite;
        public Transform transform;
        public int sizeX;
        public int sizeY;
        public GeneralObjects(PointF pos)
        {
            
        }
        public void DrawSprite(Graphics g)
        {
            g.DrawImage(sprite, transform.position.X, transform.position.Y, transform.size.Width, transform.size.Height);
        }
    }
}
