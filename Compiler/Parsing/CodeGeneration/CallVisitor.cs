using System;
using System.Linq;

namespace Compiler.Parsing.CodeGeneration
{
    public class CallVisitor : IVisitor
    {
        public Type GetExpressionType()
        {
            return typeof(CallExpression);
        }

        public string Enter(ICodeGenerator codeGenerator, IExpression expression)
        {
            var call = (CallExpression) expression;

            var args = call.arguments.Select(codeGenerator.Visit);

            var functionName = call.function.name;

            if (functionName.Equals("println"))
            {
                var placeholders = string.Join(" ", args.Select(a => "%d"));
                
                return $"printf(\"{placeholders} \\n\", " + string.Join(", ", args);
            }
            
            return $"{functionName}(" + string.Join(", ", args);
        }

        public string Exit(ICodeGenerator codeGenerator, IExpression expression)
        {
            return ");";
        }
    }
}