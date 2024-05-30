using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Classes
{
    public class Transform
    {
        public PointF Position;
        public Size Size;

        public Transform(PointF positoin, Size size)
        {
            this.Position = positoin;
            this.Size = size;
        }
    }
}
