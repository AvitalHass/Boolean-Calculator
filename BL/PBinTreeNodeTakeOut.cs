using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public partial class BinTreeNode
    {
        public bool findTemplateOfParentPlusAndTwoMulSons()
        //פונקציה המחפשת תבנית בעץ של אבא עם מפתח פלוס ושני בנים כפל או בן כפל ובן שני עלה או מפתח  פלוס 
        {
            if (this == null)
                return false;
            if (this.info == Const.PLUS && this.left.info == Const.MUL && this.right.isLeaf())
            //אם הבן הימני הוא עלה נשלח אותו לפונקציה שתבנה ממנו עץ 
            {
                this.right = this.right.changeLeafToTree();
            }
            if (this.info == Const.PLUS && this.left.info == Const.MUL && this.right.info == Const.NOT && this.right.left.isLeaf())
            //הבן הימני הוא'^'והבן שלו עלה נשלח את העלה לפונקציה שתבנה ממנו עץ
            {
                this.right = this.right.left.changeLeafToNotTree();
            }
            if (this.info == Const.PLUS && this.right.info == Const.MUL && this.left.isLeaf())
            //אם הבן השמאלי הוא עלה נשלח אותו לפונקציה שתבנה ממנו עץ
            {
                this.left = this.left.changeLeafToTree();
            }
            if (this.info == Const.PLUS && this.right.info == Const.MUL && this.left.info == Const.NOT && this.left.left.isLeaf())
            //הבן השמלי הוא'^'והבן שלו עלה נשלח את העלה לפונקציה שתבנה ממנו עץ
            {
                this.left = this.left.left.changeLeafToNotTree();
            }
            return ((this.info == Const.PLUS && this.left.info == Const.MUL && this.right.info == Const.MUL) ||
                (this.info == Const.PLUS && this.left.info == Const.MUL && this.right.info == Const.NOT && this.right.left.info == Const.MUL)
                || (this.info == Const.PLUS && this.right.info == Const.MUL && this.left.info == Const.NOT && this.left.left.info == Const.MUL));
        }

        public bool findTemlateOfParentPlusWithSonPlus()
        {//פונקציה שבודקת אם האבא הוא פלוס ואם כן היא בודקת את הבנים- אם שניהם כפל חוזר 
         //true ואם הבן הימני הוא פלוס ישלחו אותו לרקורסיה 
            if (this != null && this.info == Const.PLUS)
            {
                if (this.left.isLeaf())//הופכים את העלים לעצים
                    this.left = this.left.changeLeafToTree();
                if (this.right.isLeaf())
                    this.right = this.right.changeLeafToTree();
                if (this.right.info == Const.NOT && this.right.left.isLeaf())
                {
                    this.right = this.right.left.changeLeafToNotTree();
                }
                if (this.left.info == Const.NOT && this.left.left.isLeaf())
                {
                    this.left = this.left.left.changeLeafToNotTree();
                }
                if (this.left != null && this.left.info == Const.MUL && this.right != null && this.right.info == Const.MUL)
                {//אם התבנית מתאימה מחזירים true
                    return true;
                }
                else
                {
                    if (this.right != null && this.right.info == Const.PLUS && this.left != null && this.left.info == Const.PLUS)
                    {//אם שני הבנים הם + שולחים את שניהם לבדיקה
                        return this.right.findTemlateOfParentPlusWithSonPlus() && this.left.findTemlateOfParentPlusWithSonPlus();
                    }
                    if (this.right != null && this.right.info == Const.NOT && this.right.left.info == Const.PLUS && this.left != null && this.left.info == Const.NOT && this.left.left.info == Const.PLUS)
                    {//אם שני הבנים הם ^+ שולחים אותם לבדיקה
                        return this.right.left.findTemlateOfParentPlusWithSonPlus() && this.left.left.findTemlateOfParentPlusWithSonPlus();
                    }
                    else
                    {
                        if (this.right != null && this.right.info == Const.PLUS)
                        {//אם אחד הבנים הוא*  והשני הוא + שולחים רק את השני לבדיקה                        
                            return this.left != null && this.left.info == Const.MUL && this.right.findTemlateOfParentPlusWithSonPlus();
                        }
                        else if (this.right != null && this.right.info == Const.NOT && this.right.left.info == Const.PLUS)
                        {//אם אחד הבנים הוא* והשני הוא ^+שולחים רק את השני לבדיקה 
                            return this.left != null && this.left.info == Const.MUL && this.right.left.findTemlateOfParentPlusWithSonPlus();
                        }
                        else
                        {
                            if (this.left != null && this.left.info == Const.PLUS)
                            {//+ אם אחד הבנים הוא *והשני הוא  
                                return this.right != null && this.right.info == Const.MUL && this.left.findTemlateOfParentPlusWithSonPlus();
                            }
                            else if (this.left != null && this.left.info == Const.NOT && this.left.left.info == Const.PLUS)
                            {//אם אחד הבנים הוא * והשני הוא  ^+ שולחים רק את השני לבדיקה
                                return this.right != null && this.right.info == Const.MUL && this.left.findTemlateOfParentPlusWithSonPlus();
                            }
                        }
                    }
                }
            }
            return false;
        }

        public BinTreeNode changeLeafToNotTree()
        // "1" פונקציה שהופכת עלה עם אבא ^ לעץ שהאבא הוא כפל ושני בנים האחד העלה עם האבא ^ שקבלנו והשני צומת עם מפתח  
        {
            BinTreeNode tree = new BinTreeNode(Const.MUL);
            tree.left = new BinTreeNode(Const.NOT);
            tree.left.left = this;
            tree.right = new BinTreeNode(Const.ONE);
            return tree;
        }
        public BinTreeNode changeLeafToTree()
        // "1" פונקציה שהופכת עלה לעץ שהאבא הוא כפל ושני בנים האחד העלה שקבלנו והשני צומת עם מפתח  
        {
            BinTreeNode tree = new BinTreeNode(Const.MUL);
            tree.left = this;
            tree.right = new BinTreeNode(Const.ONE);
            return tree;
        }
        public static void createList(BinTreeNode treeToList, List<string> lstOfSubTree)
        {//פונקציה רקורסיבית שמקבלת עץ ויוצרת רשימה של כל האיברים שלו שיכולים להצטמצם
            Expression ex = new Expression();
            if (treeToList.info == Const.PLUS)//אם ראש העץ הוא + שולחים לרקורסיה את שני בניו
            {
                createList(treeToList.left, lstOfSubTree);
                createList(treeToList.right, lstOfSubTree);

            }
            if (treeToList.info == Const.NOT)//אם ראש העץ הוא ^ הופכים את העץ למחרוזת ומוסיפים אותה לרשימה של חלקי העץ
            {
                treeToList.treeToString(ex);
                if (!lstOfSubTree.Contains(ex.expression))
                    lstOfSubTree.Add(ex.expression);
            }
            if (treeToList.info == Const.MUL)//אם ראש העץ הוא כפל
            {
                if (treeToList.left.isLeaf() && treeToList.left.info != Const.ONE)//אם הבן השמאלי הוא עלה והמפתח שלו הוא לא אחד
                {
                    if (!lstOfSubTree.Contains(treeToList.left.info.ToString()))
                        lstOfSubTree.Add(treeToList.left.info.ToString());//מוסיפים את העלה לרשימה
                }
                else
                {
                    if (treeToList.left.info != Const.PLUS)
                    {
                        createList(treeToList.left, lstOfSubTree);//שולחים את תת העץ לרקורסיה
                    }

                }
                if (treeToList.right.isLeaf() && treeToList.right.info != Const.ONE)//אם הבן השמאלי הוא עלה והמפתח שלו הוא לא אחד
                {
                    if (!lstOfSubTree.Contains(treeToList.right.info.ToString()))
                        lstOfSubTree.Add(treeToList.right.info.ToString());//מוסיפים את העלה לרשימה
                }
                else
                {
                    if (treeToList.right.info != Const.PLUS)
                        createList(treeToList.right, lstOfSubTree);//שולחים את תת העץ לרקורסיה
                }
            }
        }
        public static string compareBetweenTwoLists(List<string> lstLeft, List<string> lstRight)
        {//פונקציה שמשווה בין שתי רשימות ובודקת האם אחד האיברים ברשימה הראשונה מופיע גם בשניה
            foreach (string item in lstLeft)
            {
                foreach (string item2 in lstRight)
                {
                    if (item == item2)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public BinTreeNode findItemAndReplace(string commonItem, BinTreeNode treeToReplace)
        {//פונקציה רקורסיבית שמוצאת את האיבר המשותף בתוך העץ ומוציאה אותו
            if (treeToReplace.left != null && treeToReplace.left.isLeaf() && treeToReplace.left.info.ToString() == commonItem)
            {//אם הבן השמאלי הוא האיבר המשותף אפשר למחוק אותו
                treeToReplace = treeToReplace.right;
                return treeToReplace;
            }
            if (treeToReplace.right != null && treeToReplace.right.isLeaf() && treeToReplace.right.info.ToString() == commonItem)
            {//אם הבן הימני הוא האיבר המשותף אפשר למחוק אותו
                treeToReplace = treeToReplace.left;
                return treeToReplace;
            }
            if (treeToReplace.left != null && treeToReplace.left.info == Const.PLUS)
            {//אם הבן השמאלי הוא תת עץ שהמפתח של הראש שלו הוא +,
                Expression ex = new Expression();
                treeToReplace.left.treeToString(ex);//הופכים את תת העץ לביטוי 
                if (ex.expression == commonItem)//משווים את הביטוי עם האיבר המשותף
                {
                    //מבטלים את תת העץ אם הוא שווה לאיבר המשותף
                    treeToReplace = treeToReplace.right;
                    return treeToReplace;
                }
            }
            if (treeToReplace.right != null && treeToReplace.right.info == Const.PLUS)
            {//אם הבן הימני הוא תת עץ שהמפתח של הראש שלו הוא +,
                Expression ex = new Expression();
                treeToReplace.right.treeToString(ex);//הופכים את תת העץ לביטוי 
                if (ex.expression == commonItem)//משווים את הביטוי עם האיבר המשותף
                {
                    //מבטלים את תת העץ אם הוא שווה לאיבר המשותף
                    treeToReplace = treeToReplace.left;
                    return treeToReplace;
                }
            }
            if (treeToReplace.left != null && treeToReplace.left.info == Const.NOT)
            {//אם הבן השמאלי הוא תת עץ שמורכב מאבא ^ ובן עלה 
                Expression ex = new Expression();
                treeToReplace.left.treeToString(ex);//הופכים את תת העץ לביטוי 
                if (ex.expression == commonItem)//משווים את הביטוי עם האיבר המשותף
                    //מבטלים את תת העץ אם הוא שווה לאיבר המשותף
                    treeToReplace = treeToReplace.right;
            }
            if (treeToReplace.right != null && treeToReplace.right.info == Const.NOT)
            {//אם הבן הימני הוא תת עץ שמורכב מאבא ^ ובן עלה 
                Expression ex = new Expression();
                treeToReplace.right.treeToString(ex);//הופכים את תת העץ לביטוי 
                if (ex.expression == commonItem)//משווים את הביטוי עם האיבר המשותף
                    treeToReplace = treeToReplace.left;//מבטלים את תת העץ אם הוא שווה לאיבר המשותף
            }
            if (treeToReplace.left != null && treeToReplace.left.info == Const.MUL)
            {//אם הבן השמאלי הוא * שולחים אותו לרקורסיה
                treeToReplace.left = findItemAndReplace(commonItem, treeToReplace.left);
            }
            if (treeToReplace.right != null && treeToReplace.right.info == Const.MUL)
            {//אם הבן הימני הוא * שולחים אותו לרקורסיה
                treeToReplace.right = findItemAndReplace(commonItem, treeToReplace.right);
            }
            return treeToReplace;
        }
        public void addParentToTree(string commonItem)
        {
            //  פונקציה הבונה אבא כפל שהבן השמאלי שלו יהיה האיבר המשותף והבן הימני יהיה שאר העץ 
            BinTreeNode dupThis = new BinTreeNode();
            this.duplicateTree(dupThis);
            this.info = Const.MUL;
            Expression ex = new Expression(commonItem);
            this.left = ex.expressionToTree();//בונים עץ מהאיבר המשותף
            this.right = dupThis;
        }

        public void mainTakeOut()
        {
            if (this.left != null && !this.left.isLeaf())
            {//mainTakeOut אם הבן השמאלי ללא עלה תזמן עליו את הפונקציה
                this.left.mainTakeOut();
            }
            if (this.right != null && !this.right.isLeaf())
            {//mainTakeOut אם הבן הימני ללא עלה תזמן עליו את הפונקציה
                this.right.mainTakeOut();
            }
            if (this.findTemplateOfParentPlusAndTwoMulSons()
                || this.findTemlateOfParentPlusWithSonPlus())
            //אם נמצאת תבנית כזאת יש אפשרות להוציא איבר מכל האיברים
            //ולהכפיל אותם באיבר הזה בצורה כללית
            {
                string commonItem;
                List<string> lstLeft = new List<string>();
                List<string> lstRight = new List<string>();
                List<string> lstCommonItem = new List<string>();
                createList(this.left, lstLeft);//יוצרת רשימה לעץ השמאלי
                createList(this.right, lstRight);//יוצרת רשימה לעץ הימני
                //שולחת לפונקציה שמשווה בין הרשימות ומחזירה את האיבר המשותף 
                commonItem = compareBetweenTwoLists(lstLeft, lstRight);
                while (commonItem != null)//אם יש איבר משותף
                {
                    BinTreeNode temp = new BinTreeNode(Const.PLUS);
                    temp = this.duplicateTree(temp);
                    BinTreeNode sortByCommonItemTree = this.sortByCommonItem(commonItem);
                    this.info = sortByCommonItemTree.info;
                    this.left = sortByCommonItemTree.left;
                    this.right = sortByCommonItemTree.right;
                    if (BinTreeNode.compareTo(temp, this))//אם העץ לא השתנה לאחר המיון
                    {
                        //מוסיפה לרשימת האיברים המשותפים את האיבר המשותף
                        lstCommonItem.Add(commonItem);
                        //מסירה מהרשימה את האיבר המשותף
                        lstLeft.Remove(commonItem);
                        //מסירה מהרשימה את האיבר המשותף
                        lstRight.Remove(commonItem);
                        if (this.left.isLeaf())
                            this.left = this.left.changeLeafToTree();
                        if (this.right.isLeaf())
                            this.right = this.right.changeLeafToTree();
                        //מסירה מתת העץ השמאלי את האיבר המשותף
                        this.left = findItemAndReplace(commonItem, this.left);
                        //מסירה מתת העץ הימני את האיבר המשותף   
                        this.right = findItemAndReplace(commonItem, this.right);
                        //מחפשת איבר משותף לשתי הרשימות                                                                                                                
                        commonItem = compareBetweenTwoLists(lstLeft, lstRight);
                    }
                    else
                    {
                        commonItem = null;
                        this.mainTakeOut();
                    }
                }
                for (int i = lstCommonItem.Count - 1; i >= 0; i--)
                {// '*' מוסיפה את כל האיברים המשותפים לעץ ע"י
                    this.addParentToTree(lstCommonItem[i]);
                }
            }
        }
        //}
        public BinTreeNode sortByCommonItem(string commonItem)
        {//הפונקציה הזאת ממינת את איברי העץ לפי האיבר המשותף- כל תתי העצים שאין בהם את האיבר המשותף יכנסו ראשונים לרשימה 
         //וכל תתי העצים שיש בהם את האיבר המשותף יכנסו לסוף הרשימה, וכך העץ שיבנה מהרשימה יהיה ממוין
            List<BinTreeNode> lstPartsOfPlus = new List<BinTreeNode>();
            lstPartsOfPlus = this.divTreeBetweenPluses(lstPartsOfPlus);
            //מחלקים את העץ לחלקים שהפעולה שמחברת ביניהם היא +, כך שבמהלך המיון לא תיפגע משמעות הביטוי
            //BinTreeNode treeCommonItem = new Expression(commonItem).expressionToTree();//הופכים את האיבר המשותף לעץ
            for (int i = 0; i < lstPartsOfPlus.Count; i++)//עוברים על כל חלקי העץ
            {
                //if (!BinTreeNode.isContain(treeCommonItem, lstPartsOfPlus[i]))
                {//אם החלק הנוכחי לא מכיל את האיבר המשותף מעבירים אותו לתחילת הרשימה
                    List<string> lstItems = new List<string>();
                    createList(lstPartsOfPlus[i], lstItems);
                    if (!lstItems.Contains(commonItem))
                    {
                        BinTreeNode temp = lstPartsOfPlus[i];
                        lstPartsOfPlus[i] = lstPartsOfPlus[0];
                        lstPartsOfPlus[0] = temp;
                    }

                }
            }
            // יוצרים עץ המורכב מרשימת העצים ע"י שמוסיפים צמתים שהמפתח שלהם הוא + והצמתים הללו הם האבות של חלקי העץ שברשימה
            BinTreeNode headTree = new BinTreeNode(Const.PLUS);
            headTree.right = lstPartsOfPlus[lstPartsOfPlus.Count - 1];//סורקים את הרשימה מהסוף ומוסיפים את תת העץ שברשימה כבן ימני של ראש העץ החדש
            for (int i = lstPartsOfPlus.Count - 2; i >= 0; i--)
            {
                headTree.left = lstPartsOfPlus[i];//מוסיפים לראש העץ בן שמאלי מהרשימה
                BinTreeNode tempHead = new BinTreeNode(Const.PLUS);//יוצרים צומת חדש עם מפתח +
                if (i > 0)//אם לא הגענו לתחילת הרשימה
                {
                    tempHead.right = headTree;//העץ הקודם הוא תת העץ הימני של הצומת החדש
                    headTree = tempHead;//משנים את הצבעת ראש העץ לצומת החדש
                }
            }
            return headTree;
        }


    }
}
