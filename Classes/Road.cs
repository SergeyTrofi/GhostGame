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
            Sprite = Properties.Resources.road;
            SizeX = 224;
            SizeY = 32;
            Transform = new Transform(pos, new Size(SizeX, SizeY));
        }
    }
}
