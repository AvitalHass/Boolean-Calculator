using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public partial class BinTreeNode
    {
        public char info { get; set; }//מפתח הצומת 
        public BinTreeNode left { get; set; }//מצביע לתת עץ שמאלי
        public BinTreeNode right { get; set; }//מצביע לתת עץ ימני
        public BinTreeNode()//פונקציה בונה ריקה
        {

        }
        public BinTreeNode(char info)//פונקציה בונה
        {
            this.info = info;
            this.left = null;
            this.right = null;
        }
        public BinTreeNode(BinTreeNode left, char info, BinTreeNode right)//פונקציה בונה
        {
            this.info = info;
            this.left = left;
            this.right = right;
        }
        public bool validExpression()
        {
            if ((this.info == Const.MUL || this.info == Const.PLUS) && (this.left == null || this.right == null))
            {
                return false;
            }
            else if (this.info != Const.MUL && this.info != Const.PLUS && this.info == Const.NOT && !this.isLeaf())
            {
                return false;
            }
            if (this.info == Const.NOT && this.right != null || this.info == Const.NOT && this.left == null)
            {
                return false;
            }
            return true;
        }
        public void treeToString(Expression stringTree)//פונקציה רקורסיבית שסורקת את העץ בסריקה תוכית ומחזירה ביטוי מהעץ שנסרק ללא סדר פעולות חשבון
        {
            if (this.left != null)// על תת העץ השמאליtreeToString אם תת העץ השמאלי אינו ריק תזמן את הפונקציה
            {
                this.left.treeToString(stringTree);
            }
            stringTree.expression += this.info;
            if (this.right != null) //על תת העץ הימניtreeToString אם תת העץ הימני אינו ריק תזמן את הפונקציה
            {
                this.right.treeToString(stringTree);
            }
            return;
        }
        public Expression treeToExpression(Expression myNewEx)//פונקציה ההופכת ביטוי המיוצג בעץ לביטוי המיוצג במחרוזת תוך כדי שמירת סדר פעולות חשבון
        {

            if (this == null)//אם העץ ריק סיימנו
            {
                return myNewEx;
            }
            else
            {
                if (this.info == Const.MUL || this.info == Const.NOT)//אם יש צומת שהמפתח שלו הוא כפל או^נוסיף סוגריים על תת העץ השמאלי ועל תת העץ הימני    
                {
                    if (this.left.isLeaf())//אם תת העץ השמאלי הוא עלה לא צריך להוסיף סוגריים
                    {
                        myNewEx.expression += this.left.info;
                    }
                    else if (this.left.info == Const.NOT /*&& this.left.left != null && this.left.left.isLeaf()*/)
                    {
                        this.left.treeToExpression(myNewEx);
                    }
                    else if (this.info == Const.NOT && this.left.info == Const.MUL)//TreeToExpression אם תת העץ השמאלי הוא כפל נשלח אותו שוב לפונקציה
                    {
                        myNewEx.expression += Const.OPEN;
                        this.left.treeToExpression(myNewEx);
                        myNewEx.expression += Const.CLOSE;
                    }
                    else if (this.left.info == Const.MUL)//TreeToExpression אם תת העץ השמאלי הוא כפל נשלח אותו שוב לפונקציה
                    {
                        this.left.treeToExpression(myNewEx);
                    }
                    else//אחרת תוסיף סוגריים לתת העץ השמאלי
                    {
                        myNewEx.expression += Const.OPEN;
                        this.left.treeToExpression(myNewEx); //על תת העץ השמאליtreeToString נזמן את הפונקציה
                        myNewEx.expression += Const.CLOSE;
                    }
                    myNewEx.expression += this.info;
                    if (this.right != null)//אם ראש העץ הוא ^אזי הבן הימני הוא ריק ולכן לא צריך לשים סוגריים רק אם הוא כפל נוסיף לתת העץ הימני סוגריים 
                    {
                        if (this.right.isLeaf())//אם תת העץ הימני הוא עלה לא צריך להוסיף סוגריים
                        {
                            myNewEx.expression += this.right.info;
                        }
                        else if (this.right.info == Const.NOT /*&& this.right.left!=null && this.right.left.isLeaf()*/)
                        {
                            this.right.treeToExpression(myNewEx);
                        }
                        else if (this.info == Const.NOT && this.right.info == Const.MUL)//TreeToExpression אם תת העץ השמאלי הוא כפל נשלח אותו שוב לפונקציה
                        {
                            myNewEx.expression += Const.OPEN;
                            this.right.treeToExpression(myNewEx);
                            myNewEx.expression += Const.CLOSE;
                        }
                        else if (this.right.info == Const.MUL)//TreeToExpression אם תת העץ הימני הוא כפל נשלח אותו שוב לפונקציה
                        {
                            this.right.treeToExpression(myNewEx);
                        }
                        else//אחרת תוסיף סוגריים לתת העץ הימני
                        {
                            myNewEx.expression += Const.OPEN;
                            this.right.treeToExpression(myNewEx); //על תת העץ הימניtreeToString נזמן את הפונקציה
                            myNewEx.expression += Const.CLOSE;
                        }
                    }
                }
                else//אם המפתח אינו *או^לא צריך להוסיף  סוגריים
                {
                    if (this.left != null)//על תת העץ השמאליtreeToExpression אם תת העץ השמאלי אינו ריק נזמן  את הפונקציה 
                    {
                        this.left.treeToExpression(myNewEx);
                    }
                    myNewEx.expression += this.info;
                    if (this.right != null)//על תת העץ הימניtreeToExpression אם תת העץ הימני אינו ריק נזמן  את הפונקציה 
                    {
                        this.right.treeToExpression(myNewEx);
                    }
                }
            }
            return myNewEx;
        }

        public bool isLeaf()//פונקציה המחזירה האם הצומת היא עלה 
        {
            if ((this.left == null) && (this.right == null))
                return true;
            else return false;
        }
        //פונקציה  רקורסיבית המשווה בין שני עצים
        public static bool compareTo(BinTreeNode treeToCompare1, BinTreeNode treeToCompare2)
        {
            //אם שני העצים ריקים תחזיר שהם שווים 
            if ((treeToCompare1 == null) && (treeToCompare2 == null))
                return true;
            //אם אחד העצים הוא ריק והשני לא תחזיר שאינם שווים
            if (((treeToCompare1 != null) && (treeToCompare2 == null)) || ((treeToCompare1 == null) && (treeToCompare2 != null)))
                return false;
            //אחרת תחזיר אם המפתחות שווים ותתי העצים השמאליים שווים ותתי העצים הימניים שויים 
            Task<bool> taskLeft = Task.Factory.StartNew<bool>(() => compareTo(treeToCompare1.left, treeToCompare2.left));
            Task<bool> taskRight = Task.Factory.StartNew<bool>(() => compareTo(treeToCompare1.right, treeToCompare2.right));
            return treeToCompare2.info.Equals(treeToCompare1.info)
                && taskLeft.Result && taskRight.Result;
        }

        public static bool isSubset(BinTreeNode subNode, BinTreeNode mainNode)//פונקציה רקורסיבית המחזירה אם עץ הוא תת עץ בעץ אחר 
        {
            if (subNode == null)//  אם העץ הוא ריק הוא תת עץ של העץ השני
                return true;

            if (mainNode == null)//אם העץ השני הוא ריק אזי העץ אינו תת עץ
                return false;
            return isSubset(subNode.left, mainNode.left)
                && isSubset(subNode.right, mainNode.right)
                && mainNode.info.Equals(subNode.info);
        }
        public static bool isContain(BinTreeNode subNode, BinTreeNode mainNode)//אם עץ מכיל עץ אחר
        {
            if (subNode == null)//  אם העץ הוא ריק הוא מוכל בעץ השני
                return true;

            if (mainNode == null)//  אם העץ השני הוא ריק העץ אינו מוכל בעץ השני
                return false;
            if (isSubset(subNode, mainNode))//אם העץ הוא תת עץ של העץ השני אז הוא מוכל בעץ השני
                return true;
            else
                return isContain(subNode, mainNode.right)//אם העץ הוא תת עץ של תת עץ ימני של העץ השני אז הוא מוכל בעץ השני
                || isContain(subNode, mainNode.left);//אם העץ הוא תת עץ של תת העץ השמאלי של העץ השני אז הוא מוכל בעץ השני
        }
        public List<BinTreeNode> divTreeBetweenPluses(List<BinTreeNode> lstPartsOfTree)
        {
            //this function is diving the tree to sub trees when the father of the sub tree is +
            // הפונקציה מפרידה את העץ לתתי עצים כאשר ראש העץ הוא פלוס ומחזירה רשימה של עצים 
            if (this.info == Const.PLUS)//אם המפתח הוא פלוס
            {
                if (this.left != null)//אם תת העץ השמאלי אינו ריק
                {
                    if (this.left.info != Const.PLUS)//אם המפתח של תת העץ השמאלי הוא אינו פלוס תוסיף אותו לרשימה
                    {
                        lstPartsOfTree.Add(this.left);
                    }
                    else//אם המפתח של תת העץ השמאלי הוא פלוס נשלח אותו לפונקציה שוב שתפריד ביניהם
                    {
                        this.left.divTreeBetweenPluses(lstPartsOfTree);
                    }
                }
                if (this.right != null)//אם תת העץ הימני אינו ריק
                {
                    if (this.right.info != Const.PLUS)//אם המפתח של תת העץ הימני הוא אינו פלוס תוסיף אותו לרשימה
                    {
                        lstPartsOfTree.Add(this.right);
                    }
                    else//אם המפתח של תת העץ הימני הוא פלוס נשלח אותו לפונקציה שוב שתפריד ביניהם
                    {
                        this.right.divTreeBetweenPluses(lstPartsOfTree);
                    }
                }
            }
            return lstPartsOfTree;
        }
        public List<BinTreeNode> divTreeBetweenMul(List<BinTreeNode> lstPartsOfTree)
        {
            //this function is diving the tree to sub trees when the father of the sub tree is *
            // פונקציה המפרידה את העץ לתתי עצים כאשר האבא של התת עץ הוא כפל 
            if (this.info == Const.MUL)//אם המפתח הוא כפל
            {
                if (this.left != null)//אם תת העץ הימני אינו ריק
                {
                    if (this.left.info != Const.MUL)//אם המפתח של תת העץ השמאלי הוא אינו כפל תוסיף אותו לרשימה
                    {
                        lstPartsOfTree.Add(this.left);
                    }
                    else//אם המפתח של תת העץ השמאלי הוא כפל נשלח אותו לפונקציה שוב שתפריד ביניהם
                    {
                        this.left.divTreeBetweenMul(lstPartsOfTree);
                    }
                }
                if (this.right != null)//אם תת העץ הימני אינו ריק
                {
                    if (this.right.info != Const.MUL)//אם המפתח של תת העץ הימני הוא אינו כפל תוסיף אותו לרשימה
                    {
                        lstPartsOfTree.Add(this.right);
                    }
                    else//אם המפתח של תת העץ הימני הוא כפל נשלח אותו לפונקציה שוב שתפריד ביניהם
                    {
                        this.right.divTreeBetweenMul(lstPartsOfTree);
                    }
                }
            }
            return lstPartsOfTree;
        }
        public BinTreeNode duplicateTree(BinTreeNode treeToDuplicate)//פונקציה רקורסיבית המשכפלת עץומחזירה עץ חדש
        {
            if (this != null)//אם ההעץ לא ריק תוסיף את המפתח לעץ החדש
            {
                treeToDuplicate.info = this.info;
            }
            if (this.isLeaf() == true)//אם העץ הוא עלה סיימנו 
            {
                return treeToDuplicate;
            }
            if (this.left != null)//על תת העץ השמאליduplicateTree אם תת העץ השמאלי אינו ריק תזמן את הפונקציה 
            {
                treeToDuplicate.left = new BinTreeNode();//תיצור צומת חדשה
                this.left.duplicateTree(treeToDuplicate.left);
            }
            if (this.right != null)//על תת העץ הימניduplicateTree אם תת העץ הימני אינו ריק תזמן את הפונקציה 
            {
                treeToDuplicate.right = new BinTreeNode();//תיצור צומת חדשה
                this.right.duplicateTree(treeToDuplicate.right);
            }
            return treeToDuplicate;

        }
        public Node treeToLogicGate()//פונקציה החליפה עץ בינארי המיצג ביטוי לעץ המיצג שער לוגי
        {
            if (!this.isLeaf())//אם העץ אינו עלה
            {
                if (this.info.Equals(Const.NOT))// ותוסיף אותו לעץ השערUnaryGate אם המפתח שלו הוא'^' תיצור אוביקט מסוג 
                {
                    UnaryGate unGate = new UnaryGate();
                    unGate.Oprt = this.info;//מעתיקה את המפתח
                    unGate.son1 = this.left.treeToLogicGate();//על תת העץ השמאלי treeToLogicGateמזמנת את הפונקציה
                    return unGate;
                }
                else// ותוסיף אותו לעץ השער BInaryGate אם המפתח שלו הוא'*'או'+'תיצור אוביקט מסוג 
                {
                    BinaryGate binGate = new BinaryGate();
                    binGate.Oprt = this.info;//מעתיקה את המפתח
                    binGate.son1 = this.left.treeToLogicGate();//על תת העץ השמאלי treeToLogicGateמזמנת את הפונקציה
                    binGate.son2 = this.right.treeToLogicGate();//על תת העץ הימני treeToLogicGateמזמנת את הפונקציה
                    return binGate;
                }
            }
            else//אם המפתח הוא אופרנד
            {
                Operand opnd = new Operand(this.info);//יוצרת צומת חדשה ומעתיקה את המפתח
                return opnd;
            }
        }


        //פונקציה המבצעת את כלל דמורגן כאשר יש רצף של משתנים ועל כל אחד יש'^'הפונקציה מוציאה את ה'^'מכל הרצף 
        // '+'שמה '^'על  כלם ביחד. אם בין הרצף האופרטור הוא'+'היא תהפוך את כלם ל'*' וכן להפך אם האופרטור המפריד הוא'*'אז היא תהפוך את כלם ל
        public void demorganClose()
        {
            if (this != null && this.info == Const.MUL && this.right.info == Const.NOT && this.left.info == Const.NOT)//'+' אם שני הבנים הם '^'והמפתח הוא '*'נחליף אותו ב
            {
                this.info = Const.NOT;//שמה '^'מעל כלם
                this.left.info = Const.PLUS;//'+'מחליפה את ה'*'ב
                //מסירה את ה'^'מכל אחד מהרצף 
                BinTreeNode rightTree = this.right.left;
                this.left.right = rightTree;
                this.right = null;
            }
            if (this.info == Const.PLUS && this.right.info == Const.NOT && this.left.info == Const.NOT)//'*' אם שני הבנים הם '^'והמפתח הוא '+'נחליף אותו ב
            {
                this.info = Const.NOT;//שמה '^'מעל כלם
                this.left.info = Const.MUL;//'*'מחליפה את ה'+'ב
                //מסירה את ה'^'מכל אחד מהרצף
                BinTreeNode rightTree = this.right.left;
                this.left.right = rightTree;
                this.right = null;
            }
            // מזמנת את הפונקציה על תתי העצים של העץ אם הם לא ריקים
            if (this.left != null)
            {
                this.left.demorganClose();
            }
            if (this.right != null)
            {
                this.right.demorganClose();
            }
        }
        //פונקציה המבצעת את כלל דמורגן כאשר יש רצף של משתנים ועל כלם יש'^'לא בנפרד הפונקציה שמה את ה'^'לכל הרצף 
        // '+'ומוציאה את ה- '^'מעל  כלם ביחד. אם בין הרצף האופרטור הוא'+'היא תהפוך את כלם ל'*' וכן להפך אם האופרטור המפריד הוא'*'אז היא תהפוך את כלם ל
        public void demorganOpen()
        {
            if (this != null && this.info == Const.NOT && this.left.info == Const.PLUS)//'+'אם התו המפריד הוא 
            {
                this.info = Const.MUL;//'*'מחליפה את ה'^'ב
                this.left.info = Const.NOT;//'^' שמה על כל אחד    
                this.right = new BinTreeNode(Const.NOT);
                this.right.left = this.left.right;
                this.left.right = null;
            }
            if (this.info == Const.NOT && this.left.info == Const.MUL)//'*'אם התו המפריד הוא
            {
                this.info = Const.PLUS;//'+'מחליפה את ה'^'ב
                this.left.info = Const.NOT;//'^' שמה על כל אחד 
                this.right = new BinTreeNode(Const.NOT);
                this.right.left = this.left.right;
                this.left.right = null;
            }
            // מזמנת את הפונקציה על תתי העצים של העץ אם הם לא ריקים
            if (this.left != null)
            {
                this.left.demorganOpen();
            }
            if (this.right != null)
            {
                this.right.demorganOpen();
            }

        }
    }
}
