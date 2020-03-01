using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace calculator
{

    public partial class Expression
    {

        public string expression { get; set; }


        public Expression(string express)
        {
            this.expression = express;
        }

        public Expression()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public string exToLogicGate()
        {//פונקציה שמזמנת פונקציות שהופכות ביטוי לשער לוגי
            Operand.init();
            BinTreeNode treeToLogicGate = expressionToTree();
            Node logicGate = treeToLogicGate.treeToLogicGate();
            logicGate.setPoints();
            return logicGate.svgString();
        }




        #region expression to tree
        //הפונקציות הקטע הזה הופכות את הביטוי לעץ
        public char[] expressionToPostfix()//הפונקציה הזאת הופכת את הביטוי לביטוי בכתיב סופי (כדי לשמור על סדר פעולות חשבון) ע"י מחסנית
        {
            int cb = this.countBrakets();
            char[] NewExpress = new char[(expression.Length) - cb];
            Stack<char> s = new Stack<char>();
            int i = 0, j = 0;
            while (i < expression.Length)
            {
                if (expression[i] == Const.OPEN)
                {
                    s.Push(expression[i]);//אם נפתחו סוגריים דוחפים אותם למחסנית
                }
                else if (expression[i] == Const.CLOSE)
                {
                    while (s.Peek() != Const.OPEN)
                    {
                        NewExpress[j++] = s.Pop();// אם נסגרו סוגריים נוסיף לביטוי החדש את כל האופרטורים שהיו בתוך הסוגריים ונשמרו במחסנית
                    }
                    s.Pop();//שליפת הסוגר הפותח מהמחסנית
                }
                else if (expression[i] != Const.PLUS && expression[i] != Const.MUL)//אם האיבר הוא אופרנד
                {
                    NewExpress[j++] = expression[i];//מוסיפים אותו לביטוי החדש
                }
                else
                {
                    if ((s.Count != 0))//כל עוד המחסנית לא ריקה
                    {
                        if ((expression[i] == s.Peek()) || (expression[i] == Const.PLUS && (s.Peek() == Const.MUL || s.Peek() == Const.NOT)))//בודקים אם האיבר הוא אופרטור (כדי לשמור על סדר פעולות חשבון)
                        {
                            NewExpress[j++] = s.Pop();//מוסיפים את הפעולה הקודמת שנשמרה במחסנית לביטוי
                        }
                    }
                    s.Push(expression[i]);//דוחפים את הפעולה הנוכחית למחסנית
                }
                i++;
            }
            while (s.Count != 0)//כל עוד המחסנית לא ריקה
            {
                NewExpress[j++] = s.Pop();//מוסיפים לביטוי את כל הפעולות שנשארו במחסנית
            }
            return NewExpress;

        }
        public int countBrakets()
        {//הפונקציה בודקת כמה סוגריים יש בביטוי
            int count = 0, i = 0;
            while (i < expression.Length)
            {
                if (expression[i] == Const.OPEN)
                    count++;
                i++;
            }
            return count * 2;
        }
        public BinTreeNode postfixToTree(char[] exChar)
        {//הפונקציה הזאת בונה מהביטוי בכתיב סופי עץ
            int i = 0;
            Stack<BinTreeNode> s = new Stack<BinTreeNode>();
            while (i < exChar.Length)
            {
                if (exChar[i] == Const.NOT)//אם הפעולה בביטוי היא ^ 
                {
                    BinTreeNode bin = new BinTreeNode(exChar[i]);//בונים צומת עם מפתח ^
                    bin.left = (s.Pop());//מוסיפים לצומת בן שמאלי שהוא העץ שנשמר במחסנית שמבטא את הביטוי שעליו יש את הפעולה ^
                    s.Push(bin);// דוחפים את העץ החדש למחסנית 
                }
                else if (exChar[i] == Const.PLUS || exChar[i] == Const.MUL)//אם הפעולה היא * או + 
                {
                    BinTreeNode oprtr = new BinTreeNode(exChar[i]);//בונים צומת עם המפתח המתאים
                    oprtr.right = (s.Pop());//מוסיפים לצומת בן שמאלי וימני - העצים שנשמרו במחסנית שמבטאים את שני חלקי הביטוי שעליהם יש את הפעולה
                    oprtr.left = (s.Pop());
                    s.Push(oprtr);//דוחפים את העץ החדש למחסנית
                }
                else
                {
                    BinTreeNode oprnd = new BinTreeNode(exChar[i]);//אם האיבר הוא אופרנד
                    s.Push(oprnd);//דוחפים אותו למחסנית
                }
                i++;
            }
            return s.Pop();//מחזירים את ראש העץ שנמצא בראש המחסנית
        }//הביטוי הופך מכתיב סופי לעץ ג"כ ע"י מחסנית

        public AxiomBinTreeNode postfixToAxiomTree(char[] exChar)
        {//AxiomBinTreeNode הפונקציה הזאת הופכת את הביטוי לעץ מסוג 
            //התהליך זהה לפונקציה הקודמת
            int i = 0;
            Stack<AxiomBinTreeNode> s = new Stack<AxiomBinTreeNode>();
            while (i < exChar.Length)
            {
                if (exChar[i] == Const.NOT)
                {
                    AxiomBinTreeNode axiomBin = new AxiomBinTreeNode(exChar[i]);
                    axiomBin.left = (s.Pop());
                    s.Push(axiomBin);
                }
                else if (exChar[i] == Const.PLUS || exChar[i] == Const.MUL)
                {
                    AxiomBinTreeNode oprtr = new AxiomBinTreeNode(exChar[i]);
                    oprtr.right = (s.Pop());
                    oprtr.left = (s.Pop());
                    s.Push(oprtr);
                }
                else
                {
                    AxiomBinTreeNode oprnd = new AxiomBinTreeNode(exChar[i]);
                    s.Push(oprnd);
                }
                i++;
            }
            return s.Pop();
        }
        public BinTreeNode expressionToTree()
        {//פונקציה שקוראת לשתי הפונקציות של הפיכה מביטוי לכתיב סופי ומכתיב סופי לעץ
            char[] arr = this.expressionToPostfix();
            return this.postfixToTree(arr);
        }

        public AxiomBinTreeNode axiomToAxiomTree()
        {//AxiomBinTreeNode פונקציה שקוראת לשתי הפונקציות של הפיכה מביטוי לכתיב סופי ומכתיב סופי לעץ
            char[] arr = this.expressionToPostfix();
            return this.postfixToAxiomTree(arr);
        }
        #endregion
        #region check valid     
        public bool validBrakets()
        {
            //הפעולה בודקת האם הסוגריים בביטוי תקינות
            Stack<char> stackBrakets = new Stack<char>();
            for (int i = 0; i < this.expression.Length; i++)
            {
                if (expression[i] == Const.OPEN)
                    stackBrakets.Push(expression[i]);//סוגריים פותחות נדחפות למחסנית
                if (expression[i] == Const.CLOSE)
                    if (stackBrakets.Count > 0)// סוגריים סוגרות נשלפות מהמחסנית
                        stackBrakets.Pop();
                    else
                        return false;//אם המחסנית ריקה ויש סוגריים סוגרות סימן שהן מיותרות והביטוי לא תקין
            }
            if (stackBrakets.Count > 0)//אם בסיום הסריקה של הביטוי נשארו במחסנית סוגריים פותחות סימן שהן מיותרות והביטוי לא תקין
                return false;
            return true;
        }

        public bool validExpression()
        {
            if (this.expression[0] == Const.MUL || this.expression[0] == Const.PLUS || this.expression[0] == Const.NOT || this.expression[0] == Const.CLOSE)
            {
                return false;
            }
            if (this.expression[this.expression.Length - 1] == Const.MUL || this.expression[this.expression.Length - 1] == Const.PLUS || this.expression[this.expression.Length - 1] == Const.OPEN)
            {
                return false;
            }
            for (int i = 1; i < this.expression.Length; i++)
            {
                if ((this.expression[i] == Const.MUL || this.expression[i] == Const.PLUS) && (this.expression[i - 1] == Const.OPEN || this.expression[i - 1] == Const.PLUS || this.expression[i - 1] == Const.MUL))
                {
                    return false;
                }
                if (this.expression[i] == Const.OPEN && ((this.expression[i - 1] != Const.MUL && this.expression[i - 1] != Const.PLUS)))
                {
                    return false;
                }
                if ((this.expression[i] == Const.CLOSE || this.expression[i] == Const.NOT) && (this.expression[i - 1] == Const.MUL || this.expression[i - 1] == Const.PLUS || this.expression[i - 1] == Const.OPEN))
                {
                    return false;
                }
                if ((this.expression[i] != Const.MUL && this.expression[i] != Const.PLUS && this.expression[i] != Const.NOT
                    && this.expression[i] != Const.OPEN && this.expression[i] != Const.CLOSE) &&
                   ((this.expression[i - 1] != Const.MUL && this.expression[i - 1] != Const.PLUS && this.expression[i - 1] != Const.NOT
                    && this.expression[i - 1] != Const.OPEN && this.expression[i - 1] != Const.CLOSE)||this.expression[i-1]==Const.NOT))
                {
                    return false;
                }
            }
            return true;
        }
        //   מספרים בלבד
        public bool IsNumber()
        {
            if (this.expression == "")
                return true;
            string pattern = @"\b[2-9-\s]";
            Regex reg = new Regex(pattern);
            bool c=!reg.IsMatch(this.expression);
            return c;
        }
        //public Expression deleteSpace()
        //{
        //    Expression newExp = new Expression();
        //    newExp.expression = "";
        //    for (int i = 0; i < this.expression.Length; i++)
        //    {
        //        if (this.expression[i]==' ')
        //        {
        //            for (int j = 0; j < i; j++)
        //            {
        //                newExp.expression += this.expression[j];
        //            }
        //            for (int j = i+1; j < this.expression.Length-1; j++)
        //            {
        //                newExp.expression += this.expression[j];
        //            }
        //        }
        //    }
        //    return newExp;
        //}
        #endregion
        #region sort
        public void mainSort()
        {
            bool Not = false;
            bool needBrakets = false;
            List<string> mySortList = new List<string>();
            if (OnePairOfBraket(this.expression))//בודקים אם אפשר להוריד את הסוגריים בתחילת ובסוף הביטוי והביטוי ישאר תקין
            {
                if (this.needNot())//בודקים אם נצטרך להוסיף ^ בסוף הביטוי אחרי שנוריד את הסוגריים
                {
                    Not = true;
                }
                else
                    Not = false;
                this.expression = this.removeBrakets();//מסירים את הסוגריים
                needBrakets = true;//מדליקים דגל שאומר שהורידו את הסוגריים ויצטרכו אח"כ להוסיף אותם
            }
            if (this.justMul())//אם יש בביטוי רק את הפעולה *
            {
                mySortList = this.divToMul();// מחלקים את הביטוי למחרוזות שבינהן יש את הפעולה * בביטוי וממינים את המחרוזות
                if (needBrakets)
                {
                    this.expression = listToStringMul(mySortList, true);//מרכיבים את המחרוזות לביטוי חדש ומוסיפים לו סוגריים
                }
                else
                {
                    this.expression = listToStringMul(mySortList, false);//מרכיבים את המחרוזות לביטוי חדש ולא מוסיפים לו סוגריים
                }
            }
            else
            {
                mySortList = this.divToPlus();// מחלקים את הביטוי למחרוזות שבינהן יש את הפעולה + בביטוי וממינים את המחרוזות
                if (needBrakets)
                {
                    this.expression = listToStringPlus(mySortList, true);//מרכיבים את המחרוזות לביטוי חדש ומוסיפים לו סוגריים
                }
                else
                {
                    this.expression = listToStringPlus(mySortList, false);//מרכיבים את המחרוזות לביטוי חדש ולא מוסיפים לו סוגריים
                }
            }
            if (Not)//אם צריך להוסיף ^ לביטוי
            {
                this.expression += Const.NOT;//מוסיפים ^ בסוף הביטוי 
            }
        }
        public static bool hasOprt(string checkHasOprt)
        {//הפונקציה בודקת אם יש את הפעולה * או + במחרוזת שהתקבלה
            for (int i = 0; i < checkHasOprt.Length; i++)
            {
                if (checkHasOprt[i] == Const.MUL || checkHasOprt[i] == Const.PLUS)
                {
                    return true;
                }
            }
            return false;
        }
        public static string listToStringPlus(List<string> lststring, bool needBraket)
        {//הפונקציה מקבלת רשימה של מחרוזות ויוצרת מהן ביטוי. בין מחרוזת למחרוזת תשובץ הפעולה +
            string newString = "";
            if (needBraket == true)//משתנה שאומר האם הביטוי צריך להיכנס לתוך סוגריים
            {
                newString += Const.OPEN;//שמים סוגריים פותחות בראש הביטוי
            }
            for (int i = 0; i < lststring.Count; i++)//עברים על הרשימה ובונים את הביטוי
            {
                newString += lststring[i];
                if (i != lststring.Count - 1)
                    newString += Const.PLUS;
            }
            if (needBraket == true)//אם הביטוי צריך סוגריים
                newString += Const.CLOSE;//שמים סוגריים סוגרות בסוף הביטוי
            return newString;
        }
        public static string listToStringMul(List<string> lststring, bool needBraket)
        {//הפונקציה מקבלת רשימה של מחרוזות ויוצרת מהן ביטוי. בין מחרוזת למחרוזת תשובץ הפעולה *
            int count = 0;
            string newString = "";
            if (needBraket == true)//משתנה שאומר האם הביטוי צריך להיכנס לתוך סוגריים
            {
                newString += Const.OPEN;//שמים סוגריים פותחות בראש הביטוי
            }
            foreach (string item in lststring)//עברים על הרשימה ובונים את הביטוי
            {
                count++;
                newString += item;
                if (count < lststring.Count())
                    newString += Const.MUL;
            }
            if (needBraket == true)//אם הביטוי צריך סוגריים
                newString += Const.CLOSE;//שמים סוגריים סוגרות בסוף הביטוי
            return newString;
        }

        public List<string> divToPlus()
        {//הפונקציה מחלקת את הביטוי למחרוזות כאשר בין מחרוזת למחרוזת מופיעה הפעולה + בביטוי
            List<string> arrPartsOfPlus = new List<string>();
            string partString = "";
            int itemp = 0, monePluses = 1, MoneBrakets = 0;
            for (int i = 0; i < this.expression.Length; i++)
            {
                if (this.expression[i] == Const.OPEN)
                {
                    MoneBrakets++;// סופרים את כמות הסוגריים שנפתחו בביטוי
                }
                if (this.expression[i] == Const.CLOSE && i != this.expression.Length - 1)
                {
                    MoneBrakets--;// אם יש סוגריים סוגרות ועוד לא הגענו לסוף הביטוי מורידים אחד מהמונה 
                }
                if (MoneBrakets == 0 && this.expression[i] == Const.PLUS)//עם המונה של הסוגריים לא מאופס צריך להמשיך בסריקה כי לא מפרידים את הביטוי באמצע סוגריים
                {//אם אנחנו לא באמצע סוגריים והפעולה היא + מפרידים בין שני חלקי הביטוי - את החלק הראשון מוסיפים לרשימה וממשיכים בסריקה של החלק השני
                    monePluses++;
                    partString = "";
                    for (int j = itemp; j < i; j++)
                    {
                        partString += this.expression[j];
                    }
                    arrPartsOfPlus.Add(partString);
                    itemp = i + 1;
                }
            }
            partString = "";
            for (int j = itemp; j < this.expression.Length; j++)//מעתיקים למחרוזת חדשה את החלק של הביטוי מהמקום האחרון שבו נמצאה הפעולה + ועד סוף הביטוי
            {
                partString += this.expression[j];
            }
            arrPartsOfPlus.Add(partString);//מוסיפים את המחרוזת האחרונה לרשימה
            arrPartsOfPlus.Sort();//ממיינים את הרשימה
            for (int i = 0; i < arrPartsOfPlus.Count; i++)
            {
                if (arrPartsOfPlus[i][0] == Const.OPEN || hasOprt(arrPartsOfPlus[i]))//שולחים למיון את כל המחרוזות שהן בתוך סוגריים כי לא מיינו בתוך הסוגריים
                {
                    Expression exp = new Expression(arrPartsOfPlus[i]);
                    exp.mainSort();
                    arrPartsOfPlus[i] = exp.expression;
                }
            }
            return arrPartsOfPlus;
        }
        public List<string> divToMul()
        {//הפונקציה מחלקת את הביטוי למחרוזות כאשר בין מחרוזת למחרוזת מופיעה הפעולה * בביטוי
            //התהליך בפונקציה זאת זהה לפונקציה הקודמת
            List<string> arrPartsOfMul = new List<string>();
            string partString = "";
            int itemp = 0, moneMuls = 1, MoneBrakets = 0;
            for (int i = 0; i < this.expression.Length; i++)
            {
                if (this.expression[i] == Const.OPEN)
                {
                    MoneBrakets++;
                }
                if (this.expression[i] == Const.CLOSE && i != this.expression.Length - 1)
                {
                    MoneBrakets--;
                }
                if (MoneBrakets == 0 && this.expression[i] == Const.MUL)
                {
                    moneMuls++;
                    partString = "";
                    for (int j = itemp; j < i; j++)
                    {
                        partString += this.expression[j];
                    }
                    arrPartsOfMul.Add(partString);
                    itemp = i + 1;
                }
            }
            partString = "";
            for (int j = itemp; j < this.expression.Length; j++)
            {
                partString += this.expression[j];
            }
            arrPartsOfMul.Add(partString);
            arrPartsOfMul.Sort();
            for (int i = 0; i < arrPartsOfMul.Count; i++)
            {
                if (arrPartsOfMul[i][0] == Const.OPEN || hasOprt(arrPartsOfMul[i]))
                {
                    Expression exp = new Expression(arrPartsOfMul[i]);
                    exp.mainSort();
                    arrPartsOfMul[i] = exp.expression;
                }
            }
            return arrPartsOfMul;
        }
        public bool justMul()
        {//הפונקציה בודקת אם יש בביטוי רק את הפעולה *, לא בודקים בתוך סוגריים
            int moneBrakets = 0;
            for (int i = 0; i < this.expression.Length; i++)
            {
                if (this.expression[i] == Const.OPEN)
                {
                    moneBrakets++;
                }
                if (this.expression[i] == Const.CLOSE)
                {
                    moneBrakets--;
                }
                if (this.expression[i] == Const.PLUS && moneBrakets == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool OnePairOfBraket(string exToSort)
        {//הפונקציה בודקת האם אפשר להוריד את הסוגריים שבתחילת ובסוף הביטוי והביטוי ישאר תקין 
            int index = exToSort.Length - 1;
            while (exToSort[index] == Const.NOT)
            {
                index--;
            }
            if (exToSort[0] == Const.OPEN && exToSort[index] == Const.CLOSE)
            {
                Expression helper = new Expression(exToSort);//משכפלים את הביטוי
                helper.expression = helper.removeBrakets();//שולחים את הביטוי לפונקציה שתוריד את הסוגריים
                if (new Expression(helper.expression).validBrakets())//אם הביטוי החדש תקין מחזירים true
                    return true;
            }
            return false;
        }
        public bool needNot()
        {//הפונקציה בודקת את מספר הפעמים שמופיעה הפעולה ^, אם היא מופיעה מספר זוגי של פעמים אפשר לבטל את כל ה-^ ואם
         // היא מופיעה מספר אי זוגי הביטוי צריך את הפעולה ^ פעם אחת
            bool needNot = false;
            int index = this.expression.Length - 1;
            if (this.expression[index] == Const.NOT)//כל מופע של הפעולה ^
            {
                index--;
                needNot = !needNot;//אם היה מספר זוגי של מופעים ועכשיו יש מופע נוסף המשתנה יהפוך מ false ל- true 
            }
            return needNot;
        }
        public string removeBrakets()
        {
            //הפונקציה מורידה סוגריים מיותרים שמופיעות בתחילת ובסוף ביטוי
            string exRemoveBrakets = "";
            int index = this.expression.Length - 1;
            while (this.expression[index] == Const.NOT)//אם מופיעה הפעולה ^ בסוף הביטוי צריך לדלג עליה עד שמגיעים לסוגר
            {
                index--;
            }
            for (int i = 1; i < index; i++)
            {
                exRemoveBrakets += this.expression[i];//מעתיקים את הביטוי בלי הסוגריים
            }
            return exRemoveBrakets;
        }
        #endregion
    }
}
