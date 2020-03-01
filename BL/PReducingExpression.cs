using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public partial class Expression
    {
        //בשם ה' נעשה ונצליח\

        BinTreeNode treeToReduce = new BinTreeNode();
        public List<string> mainReduce()
        {
            //הפונקציה הזאת מזמנת את כל הפונקציות של צמצום ביטוי עד שהביטוי לא יכול להצטמצם יותר
            BinTreeNode tempTree = new BinTreeNode();
            List<string> lstReduceLevels = new List<string>();
            lstReduceLevels.Add(this.expression);
            if (this.expression == "" || this.expression == null)
            {
                return lstReduceLevels;
            }
            treeToReduce = this.expressionToTree();
            Stopwatch sw = Stopwatch.StartNew();
            while (!BinTreeNode.compareTo(treeToReduce, tempTree))//בדיקה האם העץ השתנה באיטרציה הקודמת ואם כן צריך לשלוח אותו שוב לצמצום
            {
                tempTree = new BinTreeNode();
                tempTree = treeToReduce.duplicateTree(tempTree);
                //   this.sort();//מיון הביטוי
                
                treeToReduce = treeToReduce.mainSearch(lstReduceLevels);//חיפוש אקסיומות
                this.sort(lstReduceLevels);
                treeToReduce.mainLawSplit();//פתיחת סוגריים
                this.sort(lstReduceLevels);
                treeToReduce = treeToReduce.mainSearch(lstReduceLevels);
                treeToReduce.mainTakeOut();//הוצאת איבר               
                this.sort(lstReduceLevels);
                treeToReduce = treeToReduce.mainSearch(lstReduceLevels);
                treeToReduce.demorganOpen();//כלל דה- מורגן
                this.sort(lstReduceLevels);
                treeToReduce = treeToReduce.mainSearch(lstReduceLevels);
                treeToReduce.demorganClose();//כלל דה- מורגן
                this.sort(lstReduceLevels);
            }
            long time = sw.ElapsedMilliseconds;
            sw.Stop();
            this.expression = "";
            // lstReduceLevels.Add(treeToReduce.treeToExpression(this).expression);
            return lstReduceLevels;
        }
        public List<string> sort(List<string> lstReduce)
        {//הפונקציה הזאת הופכת את העץ לביטוי ושולחת אותו למיון
            this.expression = "";
            this.expression = treeToReduce.treeToExpression(this).expression;
            this.mainSort();
            treeToReduce = this.expressionToTree();
            if (lstReduce[lstReduce.Count - 1] != (this.expression))
            {
                lstReduce.Add(this.expression);
            }
            return lstReduce;
        }
    }
}
