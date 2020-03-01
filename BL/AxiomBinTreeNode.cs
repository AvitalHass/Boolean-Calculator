using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class AxiomBinTreeNode : BinTreeNode
    {
        public bool absoluti { get; set; }//מאפיין המיצג האם המפתח רשאי להתחלף בערך אחר או לא
      
        public AxiomBinTreeNode()//פעולה בונה
        {
            this.absoluti = false;
        }
        public AxiomBinTreeNode(bool absoluti, BinTreeNode left, char info, BinTreeNode right) : base(left, info, right)//פעולה בונה
        {
            this.absoluti = absoluti;
        }
        
        public AxiomBinTreeNode(char info):base(info)//פעולה בונה
        {
            this.absoluti = false;
        }

    }
}
