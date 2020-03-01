using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    partial class Expression
    {
        public class varAndValue
        {

            public char var { get; set; }
            public int value { get; set; }
            public varAndValue()
            { }
            public varAndValue(char var, int value)
            {
                this.var = var;
                this.value = value;
            }
        }
        private static int countOperns = 0;
        public List<varAndValue> numOfOpands()
        {//הפונרציה בודקת כמה משתנים מופיעים בביטוי
            List<varAndValue> lstoprn = new List<varAndValue>();
            for (int i = 0; i < this.expression.Length; i++)
            {
                if (this.expression[i] != Const.PLUS && this.expression[i] != Const.MUL && this.expression[i] != Const.NOT && this.expression[i] != Const.OPEN && this.expression[i] != Const.CLOSE && this.expression[i] != Const.ONE && this.expression[i] != Const.ZERO)
                {//אם התו הנוכחי הוא לא פעולה ולא 0 או 1 סימן שהוא משתנה
                    if (lstoprn.Exists(t => t.var == this.expression[i]) == false)//אם לא שמרנו כבר את המשתנה הזה
                    // if (varAndValue.ContainsKey(exp.expression[i]) == false)
                    {
                        // varAndValue.Add(exp.expression[i], 0);
                        varAndValue v = new varAndValue(this.expression[i], 0);
                        lstoprn.Add(v);
                        countOperns++;
                    }
                }
            }
            return lstoprn;
        }
        //  מחזיר מערך של מערכים עם כל האפשרויות של הצבת מספרים בשביל טבלת אמת
        public int[][] options(int count)
        {

            double numOfOption = Math.Pow(2, count);// מספר האפשרויות לשבץ 0 ו-1 בביטוי הוא 2 בחזקת מספר המשתנים
            int[][] matOptions = new int[Convert.ToInt32(numOfOption)][];//מטריצה לאחסון כל האפשרויות
            for (int i = 0; i < numOfOption; i++)//אתחול המטריצה
            {
                matOptions[i] = new int[count];
                for (int j = 0; j < count; j++)
                {
                    matOptions[i][j] = 0;
                }
            }
            for (int i = 0; i < numOfOption; i++)//מספר האפשרויות
            {
                int x = i;
                for (int j = 1; j <= count; j++)//מספר המשתנים 
                {
                    matOptions[i][j - 1] = (x & 1);// נותן אפשרות לכל משתנה ע"י שמביא את הביט האחרון של המספר 
                    x = x / 2;// כדי שיביא תוצאה שונה לכל משתנה צריך שיעבוא לביט הבא 
                }
            }
            return matOptions;
        }
        public void lstOprn(List<varAndValue> lstOprnsWithValues, List<char> lstOprn)
        {
            foreach (var item in lstOprnsWithValues)
            {
                lstOprn.Add(item.var);
            }
        }

        public string[] stringsOptions(List<Char> opandLst, List<List<int>> lstValuesOfOparands)
        {//שיבוץ המערכים של האפסים והאחדות בתוך התבנית של הביטוי 

            List<varAndValue> lstOprnsWithValues = this.numOfOpands();
            string newExp = "";
            int[][] options = this.options(countOperns);//קבלת המטריצה של אפשרויות השיבוץ
            double num = Math.Pow(2, countOperns);
            string[] arrOptions = new string[int.Parse(num.ToString())];//מערך של מחרוזות- כל מחרוזת תכיל ביטוי ששובצו בו מספרים שונים
            for (int i = 0; i < num; i++)
            {
                lstOprnsWithValues = setValue(options[i], lstOprnsWithValues);//הפונקציה מציבה לכל משתנה את הערך שלו בביטוי הנוכחי שמרכיבים
                for (int j = 0; j < this.expression.Length; j++)
                {
                    if (this.expression[j] == Const.PLUS || this.expression[j] == Const.MUL || this.expression[j] == Const.NOT || this.expression[j] == Const.OPEN || this.expression[j] == Const.CLOSE || this.expression[j] == Const.ZERO || this.expression[j] == Const.ONE)
                    {//אם התו הנוכחי הוא לא משתנה מעתיקים אותו לביטוי החדש
                        newExp += this.expression[j];
                    }
                    else
                    {//אם התו הנוכחי הוא כן משתנה מעתיקים לביטוי החדש את הערך שלו
                        newExp += (lstOprnsWithValues.Find(t => t.var == this.expression[j]).value);
                    }
                }
                arrOptions[i] = newExp;
                newExp = "";
            }
            lstOprn(lstOprnsWithValues, opandLst);
            valuesOfOparands(options, lstValuesOfOparands);
            return arrOptions;
        }
        public bool booleanIdentical(Expression ex2)
        {//שיבוץ המערכים של האפסים והאחדות בתוך התבנית של הביטוי 
            countOperns = 0;
            List<varAndValue> lstOprn1 = this.numOfOpands();
            int operand1 = countOperns;
            countOperns = 0;
            List<varAndValue> lstOprn2 = ex2.numOfOpands();
            int operand2 = countOperns;
            countOperns = Math.Max(operand1, operand2);
            List<varAndValue> lstOprn = operand1 > operand2 ? lstOprn1 : lstOprn2;
            string newExp = "";
            string newExp2 = "";
            int[][] options = this.options(countOperns);//קבלת המטריצה של אפשרויות השיבוץ
            double num = Math.Pow(2, countOperns);
            string[] arrOptions1 = new string[int.Parse(num.ToString())];//מערך של מחרוזות- כל מחרוזת תכיל ביטוי ששובצו בו מספרים שונים
            string[] arrOptions2 = new string[int.Parse(num.ToString())];
            for (int i = 0; i < num; i++)
            {
                lstOprn = setValue(options[i], lstOprn);//הפונקציה מציבה לכל משתנה את הערך שלו בביטוי הנוכחי שמרכיבים                
                for (int j = 0; j < this.expression.Length; j++)
                {
                    if (this.expression[j] == Const.PLUS || this.expression[j] == Const.MUL || this.expression[j] == Const.NOT || this.expression[j] == Const.OPEN || this.expression[j] == Const.CLOSE || this.expression[j] == Const.ZERO || this.expression[j] == Const.ONE)
                    {//אם התו הנוכחי הוא לא משתנה מעתיקים אותו לביטוי החדש
                        newExp += this.expression[j];
                    }

                    else
                    {//אם התו הנוכחי הוא כן משתנה מעתיקים לביטוי החדש את הערך שלו
                        newExp += (lstOprn.Find(t => t.var == this.expression[j]).value);
                        //  newExp += varAndValue[exp.expression[j]];
                    }
                }
                arrOptions1[i] = newExp;
                newExp = "";
            }
            for (int i = 0; i < num; i++)
            {
                lstOprn = setValue(options[i], lstOprn);//הפונקציה מציבה לכל משתנה את הערך שלו בביטוי הנוכחי שמרכיבים
                for (int j = 0; j < ex2.expression.Length; j++)
                {
                    if (ex2.expression[j] == Const.PLUS || ex2.expression[j] == Const.MUL || ex2.expression[j] == Const.NOT || ex2.expression[j] == Const.OPEN || ex2.expression[j] == Const.CLOSE || ex2.expression[j] == Const.ZERO || ex2.expression[j] == Const.ONE)
                    {//אם התו הנוכחי הוא לא משתנה מעתיקים אותו לביטוי החדש
                        newExp2 += ex2.expression[j];
                    }
                    else
                    {
                        newExp2 += (lstOprn.Find(t => t.var == ex2.expression[j]).value);
                    }
                }
                arrOptions2[i] = newExp2;
                newExp2 = "";
            }
            int[] arryResult1 = getResults(arrOptions1);//רשימת הפתרונות של הצבת 0,1 בביטוי א
            int[] arryResult2 = getResults(arrOptions2);//רשימת הפתרונות של הצבת 0,1 בביטוי ב
            if (arryResult1.Length != arryResult2.Length)//ז"א שמספר המשתנים בשני הביטויים שונה 
            {
                return false;
            }
            for (int i = 0; i < arryResult1.Length; i++)//עוברים על הרשימות ובודקים אם הם שוות
            {
                if (arryResult1[i] != arryResult2[i])
                {
                    return false;
                }
            }
            countOperns = 0;
            return true;
        }

        public List<varAndValue> setValue(int[] arryOpnds, List<varAndValue> lstOprn)// הפונקציה מציבה לכל משתנה את הערך שלו
        {
            for (int i = 0; i < countOperns; i++)
            {
                lstOprn[i].value = arryOpnds[i];
            }
            return lstOprn;
        }
        public static int[] getResults(string[] optionsArry)
        {//הפונקציה מקבלת מערך של כל הביטויים החדשים ושולחת כל ביטוי לפונקציית הפיתרון
            int[] arryResult = new int[optionsArry.Length];
            for (int i = 0; i < optionsArry.Length; i++)
            {
                arryResult[i] = new Expression(optionsArry[i]).solve();
            }
            return arryResult;//מערך של כל הפתרונות
        }
        public ObjectToTruthTable truthTable()
        {//הפונקציה הזאת בונה טבלת אמת ע"י הפונקציות הנ"ל
            countOperns = 0;
            ObjectToTruthTable objectToTruthTable = new ObjectToTruthTable();
            List<char> opandLst = new List<char>();
            List<List<int>> lstValuesOfOparands = new List<List<int>>();
            string[] arryOption = this.stringsOptions(opandLst, lstValuesOfOparands);
            int[] arryResult = getResults(arryOption);
            for (int i = 0; i < arryOption.Length; i++)//מוסיפים את הפתרון למחרוזות של הביטויים 
            {
                arryOption[i] += "=";
                arryOption[i] += arryResult[i];
            }
            //List<List<int>> lstValues = addExpToOptionArry(lstValuesOfOparands, arryOption);
            objectToTruthTable.arryOptions = arryOption;
            objectToTruthTable.lstOperands = opandLst;
            objectToTruthTable.valuesOfOperands = lstValuesOfOparands;
            countOperns = 0;
            return objectToTruthTable;
        }
        public List<List<int>> valuesOfOparands(int[][] options, List<List<int>> lstValuesOfOparands)
        {//הפונקציה הזאת בונה טבלת אמת ע"י הפונקציות הנ"ל            
            List<int> valuesOfOparand = new List<int>();
            for (int i = 0; i < options[i].Count(); i++)//מוסיפים את הפתרון למחרוזות של הביטויים 
            {
                for (int j = 0; j < options.ToArray().Length; j++)
                {
                    valuesOfOparand.Add(options[j][i]);
                }
                lstValuesOfOparands.Add(valuesOfOparand);
                valuesOfOparand = new List<int>();
            }
            return lstValuesOfOparands;
        }

        //public List<List<string>> addExpToOptionArry(List<List<string>> lstValues, string[] expArry)
        //{
        //    List<List<string>> lstValuesAndEx = new List<List<string>>();
        //    List<string> helpList = new List<string>();
        //    for (int i = 0; i < expArry.Length; i++)
        //    {
        //        for (int j = 0; j < lstValues.Count; j++)
        //        {
        //            helpList.Add(lstValues[j][i]);
        //        }
        //        helpList.Add(expArry[i]);
        //        lstValuesAndEx.Add(helpList);
        //        helpList = new List<string>();
        //    }
        //    return lstValuesAndEx;
        //}
    }
}
