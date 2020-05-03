using System.Collections.Generic;

namespace Compiler.Parsing
{
    public class CallExpression : IExpression
    {
        public readonly NameExpression function;
        public readonly List<IExpression> arguments;

        public CallExpression(IExpression function, List<IExpression> arguments)
        {
            this.function = (NameExpression) function;
            this.arguments = arguments;
        }
    }
}