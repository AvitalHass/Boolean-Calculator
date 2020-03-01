using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class BinaryGate : LogicGate
    {
        public Node son1 { get; set; }
        public Node son2 { get; set; }
        public BinaryGate() : base()
        {
            this.width = 40;
            this.height = 40;
        }
        public BinaryGate(Node son1, Node son2, char oprt, Point point) : base(oprt, point)
        {
            this.son1 = son1;
            this.son2 = son2;
            this.width = 40;
            this.height = 40;
        }

        public override BinTreeNode getBinTreeNode()
        {
            BinTreeNode newNode = new BinTreeNode();
            newNode.info = this.Oprt;
            newNode.left = son1.getBinTreeNode();
            newNode.right = son2.getBinTreeNode();
            return newNode;
        }

        public override string svgString()
        {
            string svg = "";
            double px = this.point.x;
            double py = this.point.y;
            Point p1 = this.son1 is LogicGate ? (this.son1 as LogicGate).point : (this.son1 as Operand).point;
            Point p2 = this.son2 is LogicGate ? (this.son2 as LogicGate).point : (this.son2 as Operand).point;
            double h1 = this.son1 is LogicGate ? (this.son1 as LogicGate).height : (this.son1 as Operand).height;
            double h2 = this.son2 is LogicGate ? (this.son2 as LogicGate).height : (this.son2 as Operand).height;
            double w1 = this.son1 is LogicGate ? (this.son1 as LogicGate).width : (this.son1 as Operand).width;
            double w2 = this.son2 is LogicGate ? (this.son2 as LogicGate).width : (this.son2 as Operand).width;
            double mulOrPlus = this.Oprt==Const.MUL?0:9;
            double avg = Math.Abs((p1.y - p2.y) / 2);
            if (this.Oprt == Const.MUL)
            {
                svg += "<path d='M " + px + " " + (py - height / 2) + " h 10 C" + " " + (px + width) + " " + (py - height / 2) + " " + (px + 40) + " " + (py + width - height / 2) + " " + (px + 10) + " " + (py + width - height / 2) + " " + "h-10 z' stroke='black' fill='transparent'/>";
            }
            if (this.Oprt == Const.PLUS)
            {
                svg += "<path d='M " + px + " " + (py - height / 2) + " h 30 C " + " " + (px + width) + " " + (py + 10 - height / 2) + " ," + (px + width) + " " + (py + 30 - height / 2) + " ," + (px + 30) + " " + (py + 40 - height / 2) + " " + " h-30 ' stroke='black' fill='transparent'/>" +
                      "<path d = 'M " + px + " " + (py + 40 - height / 2) + " C " + (px + 10) + " " + (py + 30 - height / 2) + " ," + (px + 10) + " " + (py + 10 - height / 2) + " ," + px + " " + (py - height / 2) + "' stroke = 'black' fill = 'transparent'/> ";
            }
            if (this.son1 is BinaryGate)
            {
                svg += "<polyline points = '" + (p1.x + w1-8) + " " + (p1.y) + " " + ((px - p1.x) / 2 + w1-8 + p1.x) + " " + (p1.y) + " " + ((px - p1.x) / 2 + w1-8 + p1.x) + " " + (py) + " " + (px+ mulOrPlus ) + " " + (py) + "' stroke = 'black' fill = 'transparent'></polyline>";
            }
            if (this.son2 is BinaryGate)
            {
                svg += "<polyline points = '" + (p2.x + w2-8) + " " + (p2.y) + " " + ((px - p2.x) / 2 + w2-8 + p2.x) + " " + (p2.y ) + " " + ((px - p2.x) / 2 + w2-8 + p2.x) + " " + (py) + " " + (px + mulOrPlus) + " " + (py) + "' stroke = 'black' fill = 'transparent'></polyline>";
            }
            if (this.son1 is UnaryGate)
            {
                svg += "<polyline points = '" + (p1.x + w1) + " " + (p1.y) + " " + ((px - p1.x + w1) / 2  + p1.x) + " " + (p1.y) + " " + ((px - p1.x + w1) / 2   + p1.x) + " " + (py) + " " + (px + mulOrPlus) + " " + (py) + "' stroke = 'black' fill = 'transparent'></polyline>";
            }
            if (this.son2 is UnaryGate)
            {
                svg += "<polyline points = '" + (p2.x + w2) + " " + (p2.y/*-h2/2*/) + " " + ((px - p2.x + w2) / 2   + p2.x) + " " + (p2.y/*- h2/ 2*/) + " " + ((px - p2.x + w2) / 2  + p2.x) + " " + (py) + " " + (px + mulOrPlus) + " " + (py) + "' stroke = 'black' fill = 'transparent'></polyline>";
            }
            if (this.son1 is Operand)
            {
                svg += "<polyline points = '" + (p1.x + w1) + " " + (p1.y-(h1/2)) + " " + ((px - p1.x) / 2 + w1 + p1.x) + " " + (p1.y - (h1 / 2)) + " " + ((px - p1.x) / 2 + w1 + p1.x) + " " + (py) + " " + (px + mulOrPlus) + " " + (py) + "' stroke = 'black' fill = 'transparent'></polyline>";
            }
            if (this.son2 is Operand)
            {
                svg += "<polyline points = '" + (p2.x + w2) + " " + (p2.y - (h2 / 2)) + " " + ((px - p2.x) / 2 + w2 + p2.x) + " " + (p2.y - (h2 / 2)) + " " + ((px - p2.x) / 2 + w2 + p2.x) + " " + (py) + " " + (px + mulOrPlus) + " " + (py) + "' stroke = 'black' fill = 'transparent'></polyline>";
            }
            svg += this.son1.svgString();
            svg += this.son2.svgString();
            return svg;
        }


        public override Point setPoints()
        {
            Point p1 = this.son1.setPoints();
            Point p2 = this.son2.setPoints();
            double h2 = this.son2 is LogicGate ? (this.son2 as LogicGate).height : (this.son2 as Operand).height;
            this.point.x = Math.Max(p1.x, p2.x) + Const.LENGTH;
            this.point.y = ((p1.y + p2.y- h2) / 2 );
            return this.point;
        }


    }
}
