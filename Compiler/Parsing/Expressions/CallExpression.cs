using System.Collections.Generic;

namespace Compiler.Parsing
{
    public class CallExpression : IExpression
    {
        public readonly IExpression function;
        public readonly List<IExpression> arguments;

        public CallExpression(IExpression function, List<IExpression> arguments)
        {
            this.function = function;
            this.arguments = arguments;
        }
    }
}