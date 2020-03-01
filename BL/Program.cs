using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.IO;
namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression ex = new Expression("(a*b)^*a");//   
            //ex.mainSort();        
            BinTreeNode bin = ex.expressionToTree();
           /// bin.printTree();
            bin.mainLawSplit();
            //Expression exx = new Expression();
            //bin.TreeToExpression(exx);
            //Console.WriteLine();
            BinTreeNode b = new BinTreeNode();
            b = bin.duplicateTree(b);
            bin.demorganOpen();
            bin.demorganClose();
            //Sort.mainSort(ex);
            //Console.WriteLine(ex.expression);

            bool bb = bin.findTemlateOfParentPlusWithSonPlus();
            //  bin.mainTakeOut();
            //bin.printTree();
            //findAxiomInTree.mainSearch(bin);
            //bin = BinTreeNode.removeUnnecessaryOne(bin);
            //bin.printTree();
            //Expression exx = new Expression();
            //ex.expression = bin.TreeToExpression(exx);
            //Sort.mainSort(ex);
            //findAxiomInTree.mainSearch(ex.expressionToTree());
            //Console.WriteLine(ex.expression);
            //bin.printTree();
            //Node n = bin.treeToLogicGate();
            //bin = n.getBinTreeNode();
        }
    }
}
