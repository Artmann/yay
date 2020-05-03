namespace Compiler.Parsing.Parselets
{
    public class GroupParselet : IPrefixParselet
    {
        public IExpression Parse(Parser parser, Token token)
        {
            var expression = parser.ParseExpression();

            parser.Consume(TokenType.RightParenthesis);

            return expression;
        }
    }
}