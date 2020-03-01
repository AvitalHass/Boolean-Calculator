using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class TwoEx
    {
        public Expression ex1 { get; set; }
        public Expression ex2 { get; set; }

        public TwoEx()
        {

        }
        public TwoEx(Expression ex1,Expression ex2)
        {
            this.ex1 = ex1;
            this.ex2 = ex2;
        }
    }
}
