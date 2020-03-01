using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public abstract class LogicGate : Node
    {
        public char Oprt { get; set; }
        public Point point { get; set; }
        public double height { get; set; }
        public double width { get; set; }

        public LogicGate()
        {
            this.point = new Point();
        }
        public LogicGate(char oprt,Point point)
        {
            this.Oprt = oprt;
            this.point = point;
        }
        public abstract BinTreeNode getBinTreeNode();

        public abstract string svgString();
        public abstract Point setPoints();
    }
}
