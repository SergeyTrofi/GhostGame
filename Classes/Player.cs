using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Player
    {
        public Physics Physics;
        public Image Sprite;
        public Player()
        {
            Sprite = Properties.Resources.manR;
            Physics = new Physics(new PointF(300, 176), new Size(35,28));
        }
        public void DrawSprite(Graphics g)
        {
            g.DrawImage(Sprite, Physics.Transform.Position.X, Physics.Transform.Position.Y, Physics.Transform.Size.Width, Physics.Transform.Size.Height);
        }
    }
}
