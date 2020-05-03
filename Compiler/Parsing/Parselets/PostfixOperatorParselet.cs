namespace Compiler.Parsing.Parselets
{
    public class PostfixOperatorParselet : IInfixParselet
    {
        private readonly int precedence;

        public PostfixOperatorParselet(int precedence)
        {
            this.precedence = precedence;
        }

        public IExpression Parse(Parser parser, IExpression left, Token token)
        {
            return new PostfixExpression(token.type, left);
        }

        public int GetPrecedence()
        {
            return precedence;
        }
    }
}