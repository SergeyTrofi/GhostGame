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
            sprite = Properties.Resources.pipe;
            sizeX = 40;
            sizeY = 40;
            transform = new Transform(pos, new Size(sizeX, sizeY));
        }
    }
}
