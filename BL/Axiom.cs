using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
  public  class Axiom
    {
        public string exLeft { get; set; }//האקסיומה
        public string exRight { get; set; }//פתרון האקסיומה
        
        public Axiom()//פעולה בונה ריקה
        {

        }

        public Axiom(string exLeft, string exRight)//פעולה בונה
        {
            this.exLeft = exLeft;
            this.exRight = exRight;
        }
    }
}
