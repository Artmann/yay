namespace Compiler.Parsing.Parselets
{
    public class PrefixOperatorParselet : IPrefixParselet
    {
        public readonly int precedence;

        public PrefixOperatorParselet(int precedence)
        {
            this.precedence = precedence;
        }

        public IExpression Parse(Parser parser, Token token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.

            var right = parser.ParseExpression(precedence);

            return new PrefixExpression(token.type, right);
        }
    }
}