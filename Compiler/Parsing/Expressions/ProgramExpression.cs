using System.Collections.Generic;

namespace Compiler.Parsing
{
    public class ProgramExpression : IExpression
    {
        public List<IExpression> body;

        public ProgramExpression()
        {
            body = new List<IExpression>();
        }
    }
}