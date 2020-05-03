namespace Compiler.Parsing
{
    public class NumberExpression : IExpression
    {
        public readonly string value;

        public NumberExpression(string value)
        {
            this.value = value;
        }
    }
}