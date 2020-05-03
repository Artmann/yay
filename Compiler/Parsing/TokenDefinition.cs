namespace Compiler.Parsing
{
    public class TokenDefinition
    {
        public readonly TokenType type;
        public readonly string test;

        public TokenDefinition(TokenType type, string test)
        {
            this.type = type;
            this.test = test;
        }
    }
}