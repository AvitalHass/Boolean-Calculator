using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class Operand : Node
    {
        static int operandY = 40;
        static List<Operand> lstOperand = new List<Operand>();
        public char Opnd { get; set; }
        public Point point { get; set; }
        public double height { get; set; }
        public double width { get; set; }

        public Operand()
        {
            this.point = new Point();
            this.height = 25;
            this.width = 20;
        }
        public Operand(char opnd)
        {
            this.Opnd = opnd;
            this.point = new Point();
            this.height = 25;
            this.width = 20;
        }

        public Operand(char opnd, Point point)
        {
            this.Opnd = opnd;
            this.point = point;
            this.height = 30;
        }
        public static void init()
        {
            operandY = 40;
            lstOperand = new List<Operand>();
        }

        public BinTreeNode getBinTreeNode()
        {
            return new BinTreeNode(Opnd);
        }

        public string svgString()
        {
            string svg = "";
            double px = this.point.x;
            double py = this.point.y;
            svg += "<text x = '" + px + "' y = '" + py + "' font-size='40'>" + this.Opnd + "</text>";
            //svg += "<polygon points= '" + (px + 5) + " " + (py) + " " + (15 + px) + " " + (py) + " " + (px) + " " + (12 + py) + " " + (px + 30) + " " + (py) + "' stroke='black' fill='transparent'></polygon>";
            return svg;
        }

        public Point setPoints()
        {
            if (lstOperand.Find(a => a.Opnd == this.Opnd) != null)
            {
                this.point.x = lstOperand.Find(a => a.Opnd == this.Opnd).point.x;
                this.point.y = lstOperand.Find(a => a.Opnd == this.Opnd).point.y;
            }
            else
            {
                lstOperand.Add(this);
                const int operandX = 10;
                this.point.x = operandX;
                this.point.y = operandY;
                operandY += Const.LENGTH;
            }
            return this.point;
        }
    }
}
