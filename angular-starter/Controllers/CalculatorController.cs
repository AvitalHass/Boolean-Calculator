using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using calculator;

namespace angular_starter.Controllers
{
    public class CalculatorController : ApiController
    {
        [HttpPost]
        [Route("api/reduce/")]
        public List<string> sendExpression(Expression expression)
        {
            return expression.mainReduce();
        }
        [HttpPost]
        [Route("api/truthTable/")]
        public ObjectToTruthTable expressionToTruthTable(Expression exToTruthTable)
        {
            return exToTruthTable.truthTable();
        }
        [HttpPost]
        [Route("api/logicGate/")]
        public string expressionToLogicGate(Expression exToLogicGate)
        {
            List<string> reduceLst=exToLogicGate.mainReduce();
            string l = reduceLst[reduceLst.Count-1];
            l=new Expression(l).exToLogicGate();
            return l;
        }

        [HttpPost]
        [Route("api/booleanIdentity/")]
         public bool booleanIdentity(TwoEx ex)
        {
            return ex.ex1.booleanIdentical(ex.ex2);
        }
        [HttpPost]
        [Route("api/validation/")]
        public bool expressionToCheckValid(Expression exToCheckValid)
        {
            var b = exToCheckValid.validExpression() && exToCheckValid.validBrakets()&& exToCheckValid.IsNumber();
            return b;
        }
        [HttpPost]
        [Route("api/axiomList/")]
        public List<Axiom> axiomList()
        {
            return AxiomList.getLstAx;
        }
    }
}
