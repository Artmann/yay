namespace Compiler.Parsing.CodeGeneration
{
    public interface ICodeGenerator
    {
        string GenerateCode(ProgramExpression program);
        string Visit(IExpression expression);
    }
}