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
            Sprite = Properties.Resources.platformTwo;
            SizeX = 32;
            SizeY = 16;
            Transform = new Transform(pos, new Size(SizeX, SizeY));
        }
    }
}
