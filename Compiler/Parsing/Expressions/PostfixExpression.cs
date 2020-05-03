namespace Compiler.Parsing
{
    public class PostfixExpression : IExpression
    {
        public readonly IExpression left;
        public readonly TokenType op;

        public PostfixExpression(TokenType op, IExpression left)
        {
            this.op = op;
            this.left = left;
        }
    }
}