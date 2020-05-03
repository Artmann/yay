namespace Compiler.Parsing
{
    public class NameExpression : IExpression
    {
        public readonly string name;

        public NameExpression(string name)
        {
            this.name = name;
        }
    }
}