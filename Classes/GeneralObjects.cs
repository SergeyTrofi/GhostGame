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
        public Image Sprite;
        public Transform Transform;
        public int SizeX;
        public int SizeY;
        public GeneralObjects(PointF pos)
        {
            
        }
        public void DrawSprite(Graphics g)
        {
            g.DrawImage(Sprite, Transform.Position.X, Transform.Position.Y, Transform.Size.Width, Transform.Size.Height);
        }
    }
}
