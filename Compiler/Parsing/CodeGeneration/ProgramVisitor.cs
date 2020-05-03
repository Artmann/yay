using System;
using System.Linq;

namespace Compiler.Parsing.CodeGeneration
{
    public class ProgramVisitor : IVisitor
    {
        public Type GetExpressionType()
        {
            return typeof(ProgramExpression);
        }

        public string Enter(ICodeGenerator codeGenerator, IExpression expression)
        {
            var parts = ((ProgramExpression)expression).body.Select(codeGenerator.Visit);

            return string.Join(Environment.NewLine, parts.ToArray());
        }

        public string Exit(ICodeGenerator codeGenerator, IExpression expression)
        {
            return "";
        }
    }
}