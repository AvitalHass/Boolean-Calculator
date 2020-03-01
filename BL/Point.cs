using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point()
        {
        }
        public Point(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
