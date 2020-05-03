using System.Collections.Generic;

namespace Compiler.Parsing.Parselets
{
    public class CallParselet : IInfixParselet
    {
        public IExpression Parse(Parser parser, IExpression left, Token token)
        {
            var args = new List<IExpression>();

            if (parser.Match(TokenType.RightParenthesis))
            {
                return new CallExpression(left, args);
            }

            do
            {
                args.Add(parser.ParseExpression());
            } while (parser.Match(TokenType.Comma));

            parser.Consume(TokenType.RightParenthesis);

            return new CallExpression(left, args);
        }

        public int GetPrecedence()
        {
            return Precedence.Call;
        }
    }
}