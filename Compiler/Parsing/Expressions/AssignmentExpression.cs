namespace Compiler.Parsing
{
    public class AssignmentExpression : IExpression
    {
        public readonly string name;
        public readonly IExpression right;

        public AssignmentExpression(string name, IExpression right)
        {
            this.name = name;
            this.right = right;
        }
    }
}