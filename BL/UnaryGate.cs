using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class UnaryGate : LogicGate
    {
        public Node son1 { get; set; }
        public UnaryGate() : base()
        {
            this.width = 46;
            this.height = 30;
        }
        public UnaryGate(Node son1, char oprt, Point point) : base(oprt, point)
        {
            this.son1 = son1;
            this.width = 46;
            this.height = 30;
        }
        public override BinTreeNode getBinTreeNode()
        {
            BinTreeNode newNode = new BinTreeNode();
            newNode.info = this.Oprt;
            newNode.left = son1.getBinTreeNode();
            return newNode;
        }

        public override string svgString()
        {//לבדוק!!!
            string svg = "";
            Point p1 = this.son1 is LogicGate ? (this.son1 as LogicGate).point : (this.son1 as Operand).point;
            double h1 = this.son1 is LogicGate ? (this.son1 as LogicGate).height : (this.son1 as Operand).height;
            double w1 = this.son1 is LogicGate ? (this.son1 as LogicGate).width : (this.son1 as Operand).width;
            double px = this.point.x;
            double py = this.point.y;
            if (this.son1 is Operand)
            {
                svg += "<polygon points= '" + (px + 30) + " " + (py - (h1 / 5)) + " " + (px) + " " + (-20 + py - (h1 / 5)) + " " + (px) + " " + (20 + py - (h1 / 5)) + " " + (px + 30) + " " + (py - (h1 /5)) + "' stroke='black' fill='transparent'></polygon>" +
                   "<circle cx = '" + (38 + px) + "' cy = '" + (py - (h1 / 5)) + "' r = '8' stroke = 'black' fill = 'transparent' />";
                svg += "<polyline points=' " + (p1.x + w1) + " " + (py - (h1 / 5)) + " " + (px) + " " + (py - (h1 / 5)) + "' stroke='black' fill='transparent'></polyline> ";
            }
            else
            {
                svg += "<polygon points= '" + (px + 30) + " " + (py) + " " + (px) + " " + (-20 + py) + " " + (px) + " " + (20 + py) + " " + (px + 30) + " " + (py) + "' stroke='black' fill='transparent'></polygon>" +
                    "<circle cx = '" + (38 + px) + "' cy = '" + (py) + "' r = '8' stroke = 'black' fill = 'transparent' />";
                svg += "<polyline points=' " + (p1.x + w1-8) + " " + (py) + " " + (px) + " " + (py) + "' stroke='black' fill='transparent'></polyline> ";
            }
            svg += this.son1.svgString();
            return svg;
        }

        public override Point setPoints()
        {
            Point p1 = this.son1.setPoints();
            this.point.x = p1.x + Const.LENGTH;
            this.point.y = p1.y;
            return this.point;
        }

        //"<polygon points= '" + (p1.x+ 5) + " " + (py) + " " + (15 + px) + " " + (py) + " " + (px) + " " + (12 + py) + " " + (px + 30) + " " + (py) + "' stroke='black' fill='transparent'></polygon>"+

    }
}
