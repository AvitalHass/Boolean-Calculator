using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace calculator
{
    public partial class BinTreeNode
    {

        public void mainLawSplit()
        {//פונקציה רקורסיבית שעוברת על העץ ובודקת אם יש תבנית שמתאימה לפעולת פתיחת סוגריים
            if (this.isLeaf() == true)
                return;
            if ((this.left != null) && (this.left.info == Const.MUL))
                this.left.lawSplit();
            if ((this.right != null) && (this.right.info == Const.MUL))
                this.right.lawSplit();
            if (this.left != null)
                this.left.mainLawSplit();
            if (this.right != null)
                this.right.mainLawSplit();
            if (this.info == Const.MUL)
                this.lawSplit();

        }
        public void lawSplit()
        {//הפונקציה מבצעת את פעולת פתיחת סוגריים
            BinTreeNode temp = this;
            BinTreeNode tempTree = new BinTreeNode(); ;
            if ((this.left != null && this.right != null) && (this.left.isLeaf() == false || this.right.isLeaf() == false))//צריך שיהיו שני בנים ושלפחות אחד מהם לא יהיה עלה
            {
                this.demorganOpen();//קריאה לכלל דה- מורגן כדי לבטל ^ שמפריע לפעולת פתיחת סוגריים
                if (!this.left.isLeaf() && (this.left.info == Const.PLUS))
                {    //אם הבן השמאלי הוא לא עלה ויש בו את הפעולה + צריך לפצל אותו לשני עצים וכל חלק להכפיל/ לחבר לתת העץ הימני
                    BinTreeNode newTree = new BinTreeNode(this.info);
                    tempTree = this.right.duplicateTree(tempTree);//משכפלים את תת העץ הימני
                    newTree.right = tempTree;//יוצרים עץ חדש שבראשו הפעולה שהיתה בראש העץ המקורי והבן הימני הוא העץ ששוכפל
                    newTree.left = this.left.right;//הבן השמאלי של העץ החדש הוא החלק שפוצל מתת העץ השמאלי של העץ המקורי
                    this.left = this.left.left;//מוחקים מהעץ המקורי את החלק שהוסיפו לעץ החדש
                    temp = new BinTreeNode(Const.PLUS);//צומת בעל מפתח + שאליו יחוברו שני העצים שנוצרו
                    BinTreeNode dupThis = new BinTreeNode();
                    this.duplicateTree(dupThis);
                    temp.left = dupThis;
                    temp.right = newTree;
                }
                else if (this.right.info == Const.PLUS)
                {//אותה דרך פעולה - על תת העץ הימני ולא על השמאלי
                    BinTreeNode newTree = new BinTreeNode(this.info);
                    tempTree = this.left.duplicateTree(tempTree);
                    newTree.left = tempTree;
                    temp = new BinTreeNode(Const.PLUS);
                    newTree.right = this.right.left;
                    this.right = this.right.right;
                    BinTreeNode dupThis = new BinTreeNode();
                    this.duplicateTree(dupThis);
                    temp.right = dupThis;
                    temp.left = newTree;
                }
            }
            this.info = temp.info;
            this.left = temp.left;
            this.right = temp.right;
        }
    }
}
