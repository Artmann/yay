namespace Compiler.Parsing.Parselets
{
    public class NameParselet : IPrefixParselet
    {
        public IExpression Parse(Parser parser, Token token)
        {
            return new NameExpression(token.value);
        }
    }
}