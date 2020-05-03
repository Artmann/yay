using System;

namespace Compiler.Parsing.CodeGeneration
{
    public interface IVisitor
    {
        Type GetExpressionType();
        string Enter(ICodeGenerator codeGenerator, IExpression expression);
        string Exit(ICodeGenerator codeGenerator, IExpression expression);
    }
}