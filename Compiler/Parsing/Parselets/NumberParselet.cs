namespace Compiler.Parsing.Parselets
{
    public class NumberParselet : IPrefixParselet
    {
        public IExpression Parse(Parser parser, Token token)
        {
            return new NumberExpression(token.value);
        }
    }
}