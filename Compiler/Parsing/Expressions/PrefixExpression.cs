namespace Compiler.Parsing
{
    public class PrefixExpression : IExpression
    {
        public readonly TokenType op;
        public readonly IExpression right;

        public PrefixExpression(TokenType op, IExpression right)
        {
            this.op = op;
            this.right = right;
        }
    }    
}