using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Road : GeneralObjects
    {
        public Road(PointF pos) : base(pos)
        {
            sprite = Properties.Resources.road;
            sizeX = 224;
            sizeY = 32;
            transform = new Transform(pos, new Size(sizeX, sizeY));
        }
    }
}
