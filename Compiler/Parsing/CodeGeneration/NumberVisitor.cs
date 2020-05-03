using System;

namespace Compiler.Parsing.CodeGeneration
{
    public class NumberVisitor : IVisitor
    {
        public Type GetExpressionType()
        {
            return typeof(NumberExpression);
        }

        public string Enter(ICodeGenerator codeGenerator, IExpression expression)
        {
            return ((NumberExpression)expression).value;
        }

        public string Exit(ICodeGenerator codeGenerator, IExpression expression)
        {
            return "";
        }
    }
}