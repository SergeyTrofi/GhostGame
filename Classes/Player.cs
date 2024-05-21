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
        public Physics physics;
        public Image sprite;
        public Player()
        {
            sprite = Properties.Resources.man2;
            physics = new Physics(new PointF(300, 176), new Size(35,28));
        }
        public void DrawSprite(Graphics g)
        {
            g.DrawImage(sprite, physics.transform.position.X, physics.transform.position.Y, physics.transform.size.Width, physics.transform.size.Height);
        }
    }
}
