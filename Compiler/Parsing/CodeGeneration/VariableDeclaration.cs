namespace Compiler.Parsing.CodeGeneration
{
    public class VariableDeclaration
    {
        public readonly string name;
        public readonly string type;
        public readonly string value;

        public VariableDeclaration(string name, string type, string value)
        {
            this.name = name;
            this.type = type;
            this.value = value;
        }
    }
}