namespace Compiler.Parsing.Parselets
{
    public class BinaryOperatorParselet : IInfixParselet
    {
        private readonly int precedence;
        private readonly bool isRight;

        public BinaryOperatorParselet(int precedence, bool isRight)
        {
            this.precedence = precedence;
            this.isRight = isRight;
        }

        public IExpression Parse(Parser parser, IExpression left, Token token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            
            var right = parser.ParseExpression(precedence - (isRight ? 1 : 0));
            
            return new OperatorExpression(token.type, left, right);
        }

        public int GetPrecedence()
        {
            return precedence;
        }
    }
}