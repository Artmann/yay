using System;

namespace Compiler.Parsing.CodeGeneration
{
    public class AssignmentVisitor : IVisitor
    {
        public Type GetExpressionType()
        {
            return typeof(AssignmentExpression);
        }

        public string Enter(ICodeGenerator codeGenerator, IExpression expression)
        {
            var assignment = expression as AssignmentExpression;

            if (assignment?.name == null)
            {
                return "";
            }
            
            return $"{assignment.name} = " + codeGenerator.Visit(assignment.right);
        }

        public string Exit(ICodeGenerator codeGenerator, IExpression expression)
        {
            return ";";
        }
    }
}