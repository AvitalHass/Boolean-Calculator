using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public partial class BinTreeNode
    {
        private static bool hadChanged = false;//מאפיין השומר אם העץ השתנה לאחר מציאת האקסיומות       
        Dictionary<Axiom, AxiomTrees> dicOfAxiomAndTree = new Dictionary<Axiom, AxiomTrees>();//מקבלת את המילון של האקסיומות
        private static Dictionary<char, BinTreeNode> dicOfVars = new Dictionary<char, BinTreeNode>();

        public BinTreeNode mainSearch(List<string> lstReduceLevels)//פונקציה השולחת לחיפוש אקסיומות  
        {
            BinTreeNode tree = this.searchAxiom(lstReduceLevels);//שולחת פעם ראשונה
            while (hadChanged)//אם ישנו שינוי ונמצאה אקסיומה בפעם הקודמת אולי נוצרה אקסיומה שוב לצימצום ולכן מזמנים שוב את חיפוש האקסיומות 
            {
                hadChanged = false;
                tree = tree.searchAxiom(lstReduceLevels);

            }
            return tree;
        }
        public BinTreeNode searchAxiom(List<string> lstReduceLevels)//מחפשת אקסיומות
        {
            //הפונקציה עוברת על רשימת האקסיומות ושולחת כל אקסיומה לפונקציה שבודקת האם היא קיימת בעץ
            foreach (var item in AxiomList.getDicOfAxiomAndTrees())
            {              
                dicOfVars = new Dictionary<char, BinTreeNode>();//מילון שהמפתח שלו הוא הערך באקסיומה והערך שלו הוא הערך המוחלף
                if (this.compareTreeWithAxiomTree(item.Value.leftTree))//אם עץ הביטוי שווה לאקסיומה אם כך יוצאים כיוון שאין בתת העצים שלו אקסיומות 
                {
                    hadChanged = true;//מצאנו אקסיומה ולכן יש שינוי 
                    BinTreeNode newTree = new BinTreeNode();
                    this.duplicateTree(newTree);
                    newTree = buildRightTree(item.Value.rightTree, newTree);//בונים את העץ ע"פ פתרון האקסיומה
                    if (lstReduceLevels[lstReduceLevels.Count-1]!=(newTree.treeToExpression(new Expression()).expression))
                    {
                        lstReduceLevels.Add(newTree.treeToExpression(new Expression()).expression);
                    }
                    return newTree;
                }
                this.findAxiomTreeInTheTree(item.Value.leftTree, item.Value.rightTree, lstReduceLevels);
                //Task.Factory.StartNew(()=>);//אחרת שולחים לפונקציה הבודקת בתתי העצים השמאלי והימני
            }
            return this;
        }
        //פונקציה המחפשת אקסיומות בצורה רקורסיבית שמחפשת גם בתתי העצים השמאלי והימני
        public void findAxiomTreeInTheTree(AxiomBinTreeNode axiomLeftTree, AxiomBinTreeNode axiomRightTree, List<string> lstReduceLevels)
        {
            //הפונקציה הזאת סורקת את העץ ושולחת כל תת עץ לפונקציה שמשווה בין העץ לאקסיומה
            if (this.info != Const.NOT)// '^' אם ראש העץ הוא לא    
            {
                if (this.left != null && this.left.isLeaf() && this.right != null && this.right.isLeaf())//אם שני תתי העצים הם עלים אין בהם אקסיומות
                {
                    return;
                }
            }
            else if (this.left != null && this.left.isLeaf())//אם ראש העץ הוא '^'ותת העץ השמאלי הוא עלה אין בו אקסיומה
            {
                return;
            }
            dicOfVars = new Dictionary<char, BinTreeNode>();
            if (this.left != null && this.left.compareTreeWithAxiomTree(axiomLeftTree))//אם תת העץ השמאלי אינו ריק ושווה לאקסיומה
            {
                hadChanged = true;//מצאנו אקסיומה ולכן יש שינוי 
                this.left = buildRightTree(axiomRightTree, this.left);//בונים את העץ ע"פ פתרון האקסיומה
            }
            dicOfVars = new Dictionary<char, BinTreeNode>();
            if (this.right != null && this.right.compareTreeWithAxiomTree(axiomLeftTree))//אם תת העץ הימני אינו ריק ושווה לאקסיומה
            {
                hadChanged = true;//מצאנו אקסיומה ולכן יש שינוי 
                this.right = buildRightTree(axiomRightTree, this.right);//בונים את העץ ע"פ פתרון האקסיומה
            }
            if (this.left != null && !this.left.isLeaf())//אם תת העץ השמאלי לא ריק ולא עלה תחפש את האקסיומה בתת העץ השמאלי
            {
                this.left.findAxiomTreeInTheTree(axiomLeftTree, axiomRightTree,lstReduceLevels);
                //Task.Factory.StartNew(()=>
            }
            if (this.right != null && !this.right.isLeaf())//אם תת העץ הימני לא ריק ולא עלה תחפש את האקסיומה בתת העץ הימני
            {
               /* Task.Factory.StartNew (()=>*/ this.right.findAxiomTreeInTheTree(axiomLeftTree, axiomRightTree,lstReduceLevels);
            }
        }
        public bool compareTreeWithAxiomTree(AxiomBinTreeNode axiomTree)//משווה בין העץ לעץ האקסיומה
        {
            if (this == null && axiomTree == null)//true אם שניהם ריקים תחזיר
            {
                return true;
            }
            if ((this == null && axiomTree != null) || (this != null && axiomTree == null))//false אם עץ אחד ריק והשני מלא תחזיר 
                return false;
            if (axiomTree.absoluti)// אם המפתח הוא אבסולוטי מפתח העץ חייב להיות שווה לו 
            {
                if (this.info != axiomTree.info)//false אם הוא לא שווה תחזיר 
                    return false;
                else
                {
                    if (this.left != null && this.right != null)
                    //אם תתי העצים אינם ריקים תשווה את תת העץ השמאלי עם תת העץ השמאלי של עץ האקסיומה ואת תת העץ הימני עם תת העץ הימני של האקסימה
                    {
                        return (this.left.compareTreeWithAxiomTree(axiomTree.left as AxiomBinTreeNode)
                                         && this.right.compareTreeWithAxiomTree(axiomTree.right as AxiomBinTreeNode));
                    }
                    if (this.left != null)//  אם  תת העץ השמאלי אינו ריק תשווה אותו עם תת העץ השמאלי של האקסיומה
                        return this.left.compareTreeWithAxiomTree(axiomTree.left as AxiomBinTreeNode);
                    if (this.right != null)//  אם  תת העץ הימני אינו ריק תשווה אותו עם תת העץ הימני של האקסיומה
                        return this.right.compareTreeWithAxiomTree(axiomTree.right as AxiomBinTreeNode);
                }
            }
            if (!axiomTree.absoluti)//אם מפתח האקסיומה אינו אבסלוטי ניתן להחלפה
            {
                //if (this.info == Const.ZERO || this.info == Const.ONE || this.info == Const.MUL || this.info == Const.PLUS || this.info == Const.NOT)
                //    return false;
                if (!dicOfVars.ContainsKey(axiomTree.info))//אם המילון לא מכיל את המפתח של האקסיומה 
                {
                    if (dicOfVars.ContainsValue(this))//אם המילון מכיל ערך ששוה לעץ   
                    {
                        return false;
                    }
                    else
                    {
                        dicOfVars.Add(axiomTree.info, this);//נוסיף אותו למילון
                    }
                }
                else//אם הוא נמצא במילון 
                {
                    if (BinTreeNode.compareTo(dicOfVars[axiomTree.info], this))//תשווה את העץ עם הערך במילון במפתח השווה למפתח האקסיומה
                    {

                        //if (this.left != null && this.right != null)
                        //{
                        //    //אם תתי העצים אינם ריקים תשווה את תת העץ השמאלי עם תת העץ השמאלי של עץ האקסיומה ואת תת העץ הימני עם תת העץ הימני של האקסימה
                        //    return (this.left.compareTreeWithAxiomTree(axiomTree.left as AxiomBinTreeNode)
                        //                     && this.right.compareTreeWithAxiomTree(axiomTree.right as AxiomBinTreeNode));
                        //}
                        //if (this.left != null)//  אם  תת העץ השמאלי אינו ריק תשווה אותו עם תת העץ השמאלי של האקסיומה
                        //    return this.left.compareTreeWithAxiomTree(axiomTree.left as AxiomBinTreeNode);
                        //if (this.right != null)//  אם  תת העץ הימני אינו ריק תשווה אותו עם תת העץ הימני של האקסיומה
                        //    return this.right.compareTreeWithAxiomTree(axiomTree.right as AxiomBinTreeNode);
                        return true;
                    }
                    else
                        return false;
                }
            }
            return true;
        }

        public BinTreeNode buildRightTree(AxiomBinTreeNode axiomRightTree, BinTreeNode treeToBuild)// היכן שנמצאה אקסיומה מחליפים אותה בפתרון האקסיומה אך עם המשתנים שבמילון  
        {
            //הפונקציה הזאת בונה את העץ החדש לפי התבנית של פתרון האקסיומה עם המשתנים של הביטוי
            if (axiomRightTree == null)//אם עץ פתרון האקסיומה ריק סיימת 
                return treeToBuild;
            if (axiomRightTree.absoluti)//אם מפתח עץ הפתרון של האקסיומה הוא אבסולוטי 
            {
                treeToBuild.info = axiomRightTree.info;//נשים במפתח העץ את המפתח של עץ הפתרון של האקסיומה
                treeToBuild.left = null;
                treeToBuild.right = null;
            }
            else
            {
                treeToBuild = dicOfVars[axiomRightTree.info];//נשים במפתח העץ את הערך במילון הנמצא במפתח של עץ הפתרון של האקסיומה!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            if (axiomRightTree.left != null)//אם תת העץ השמאלי של עץ הפתרון של האקסיומה לא ריק 
            {
                treeToBuild.left = new BinTreeNode();
                treeToBuild.left = buildRightTree(axiomRightTree.left as AxiomBinTreeNode, treeToBuild.left);//תבנה את תת העץ השמאלי
            }

            if (axiomRightTree.right != null)//אם תת העץ הימני של עץ הפתרון של האקסיומה לא ריק
            {
                treeToBuild.right = new BinTreeNode();
                treeToBuild.right = buildRightTree(axiomRightTree.right as AxiomBinTreeNode, treeToBuild.right);//תבנה את תת העץ הימני
            }
            return treeToBuild;
        }

    }
}
