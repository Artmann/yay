using System;

namespace Compiler.Parsing.CodeGeneration
{
    public class NameVisitor : IVisitor
    {
        public Type GetExpressionType()
        {
            return typeof(NameExpression);
        }

        public string Enter(ICodeGenerator codeGenerator, IExpression expression)
        {
            var name = (NameExpression) expression;

            return name.name;
        }

        public string Exit(ICodeGenerator codeGenerator, IExpression expression)
        {
            return "";
        }
    }
}