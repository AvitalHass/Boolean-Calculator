using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    partial class Expression
    {
        public int solve()
        {//הפונקציה הזאת פותרת את הביטוי
            int i = 0;
            Expression exp = new Expression(this.expression);
            char[] arry = exp.expressionToPostfix();
            Stack<int> stackSolve = new Stack<int>();
            while (i < arry.Length)
            {
                if (arry[i] == Const.NOT)
                {
                    stackSolve.Push(1 - stackSolve.Pop());//אם פעולה היא ^ צריך להפוך את התוצאה מ-0 ל-1 ולהפך
                }
                else if (arry[i] == Const.PLUS)
                {
                    stackSolve.Push(stackSolve.Pop() | stackSolve.Pop());//אם הפעולה היא + צריך לעשות את הפעולה | בין שתי התוצאות הקודמות  
                }
                else if (arry[i] == Const.MUL)
                {
                    stackSolve.Push(stackSolve.Pop() & stackSolve.Pop());//אם הפעולה היא * צריך לעשות את הפעולה & בין שתי התוצאות הקודמות
                }
                else
                {
                    stackSolve.Push(arry[i] - 48);
                }
                i++;
            }
            return stackSolve.Pop();
        }
    }
}