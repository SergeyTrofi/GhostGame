using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Pipe  : GeneralObjects
    {
        public Pipe(PointF pos) : base(pos)
        {
            Sprite = Properties.Resources.pipe;
            SizeX = 40;
            SizeY = 40;
            Transform = new Transform(pos, new Size(SizeX, SizeY));
        }
    }
}
