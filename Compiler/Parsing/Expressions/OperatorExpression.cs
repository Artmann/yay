namespace Compiler.Parsing
{
    public class OperatorExpression : IExpression
    {
        public readonly TokenType op;
        public readonly IExpression left;
        public readonly IExpression right;

        public OperatorExpression(TokenType op, IExpression left, IExpression right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }
    }
}