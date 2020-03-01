using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.IO;




namespace calculator
{
    public class AxiomTrees
    {
        public AxiomBinTreeNode leftTree { get; set; }//עץ המיצג את האקסיומה 
        public AxiomBinTreeNode rightTree { get; set; }//עץ המיצג את פתרון האקסיומה
        public AxiomTrees() { }//פעולה בונה
        public AxiomTrees(AxiomBinTreeNode leftTree, AxiomBinTreeNode rightTree)//פעולה בונה
        {
            this.leftTree = leftTree;
            this.rightTree = rightTree;
        }
    }
    public static class AxiomList
    {
        private static List<Axiom> lstAx;//רשימה של אקסיומות     
        public static List<Axiom> getLstAx//פונקציה המחזירה את רשימת האקסיומות
        {
            get
            {
                StreamReader reader = new StreamReader("C:\\json1.json");
                string json = reader.ReadToEnd();
                lstAx = Newtonsoft.Json.JsonConvert.DeserializeObject<Axiom[]>(json).ToList<Axiom>();
                lstAx.RemoveAt(10);
                return lstAx;
            }
        }      
    

        public static Dictionary<Axiom, AxiomTrees> getDicOfAxiomAndTrees()//פונקציה המחזירה מילון של אקסיומה והעצים השייכים לה עץ האקסיומה ועץ הפתרון
        {
            //מאתחלת את הרשימה
            StreamReader reader =  new StreamReader("C://json1.json");
            string json = reader.ReadToEnd();
            lstAx = Newtonsoft.Json.JsonConvert.DeserializeObject<Axiom[]>(json).ToList<Axiom>();            
            Dictionary<Axiom, AxiomTrees> dicAxiomAndTrees = new Dictionary<Axiom, AxiomTrees>();
            foreach (Axiom item in lstAx)//עוברת על הרשימה ויוצרת עצים ומכניסה למילון את האקסיומה והעצים שלה
            {
                AxiomTrees axiomTrees = new AxiomTrees();
                Expression helper = new Expression(item.exLeft);
                axiomTrees.leftTree = helper.axiomToAxiomTree();
                helper.expression = item.exRight;
                axiomTrees.rightTree = helper.axiomToAxiomTree();
                changeToAbsolut(axiomTrees.leftTree);//לעץ השמאליchangeToAbsolut מזמנת פונקציה    
                changeToAbsolut(axiomTrees.rightTree);//לעץ הימניchangeToAbsolut מזמנת פונקציה    
                dicAxiomAndTrees.Add(item, axiomTrees);//מוסיפה למילון
            }
            return dicAxiomAndTrees;
        }       
        public static void changeToAbsolut(AxiomBinTreeNode treeToChangeToAbsolut)//absolute פונקציה רקורסיבית הקובעת לכל צומת את המאפיין  
        {
            if (treeToChangeToAbsolut == null)//אם העץ ריק סיימת
                return;
           //  אם הערך הוא 1,*,+,^,0 trueמשתנה ל absolute המאפיין
            if (treeToChangeToAbsolut.info == Const.ZERO || treeToChangeToAbsolut.info == Const.ONE || treeToChangeToAbsolut.info == Const.MUL || treeToChangeToAbsolut.info == Const.PLUS || treeToChangeToAbsolut.info == Const.NOT)
            {
                treeToChangeToAbsolut.absoluti = true;
            }
            if (treeToChangeToAbsolut.left != null)//אם תת העץ השמאלי לא ריק תזמן את הפונקציה שוב על תת העץ השמאלי
            {
                changeToAbsolut((treeToChangeToAbsolut.left as AxiomBinTreeNode));
            }
            if (treeToChangeToAbsolut.right != null)//אם תת העץ הימני לא ריק תזמן את הפונקציה שוב על תת העץ הימני
            {
                changeToAbsolut((treeToChangeToAbsolut.right as AxiomBinTreeNode));
            }
        }
    }


}


