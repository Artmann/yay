using System;

namespace Compiler.Parsing.CodeGeneration
{
    public class OperatorVisitor : IVisitor
    {
        public Type GetExpressionType()
        {
            return typeof(OperatorExpression);
        }

        public string Enter(ICodeGenerator codeGenerator, IExpression expression)
        {
            var op = (OperatorExpression) expression;

            var a = codeGenerator.Visit(op.left);
            var b = codeGenerator.Visit(op.right);

            switch(op.op)
            {
                case TokenType.Add:
                    return $"{a} + {b}";
                case TokenType.Divide:
                    return $"{a} / {b}";
                case TokenType.Minus:
                    return $"{a} - {b}";
                case TokenType.Multiply:
                    return $"{a} * {b}";
            }

            return "";
        }

        public string Exit(ICodeGenerator codeGenerator, IExpression expression)
        {
            return "";
        }
    }
}