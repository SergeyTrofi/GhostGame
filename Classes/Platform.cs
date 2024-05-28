using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Platform : GeneralObjects
    {
        public Platform(PointF pos)  :  base(pos)
        {
            sprite = Properties.Resources.platformTwo;
            sizeX = 32;
            sizeY = 16;
            transform = new Transform(pos, new Size(sizeX, sizeY));
        }
    }
}
