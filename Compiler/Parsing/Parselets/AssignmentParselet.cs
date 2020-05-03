using System;

namespace Compiler.Parsing.Parselets
{
    public class AssignmentParselet : IInfixParselet
    {
        public IExpression Parse(Parser parser, IExpression left, Token token)
        {
            if (!(left is NameExpression))
            {
                throw new Exception($"The left hand side of an assignment must be an identifier.");
            }
            
            var right = parser.ParseExpression(Precedence.Assignment - 1);
            var name = ((NameExpression) left).name;
            
            return new AssignmentExpression(name, right);
        }

        public int GetPrecedence()
        {
            return Precedence.Assignment;
        }
    }
}