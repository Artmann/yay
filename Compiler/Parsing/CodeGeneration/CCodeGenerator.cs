using System;
using System.Collections.Generic;
using System.Linq;
using HandlebarsDotNet;

namespace Compiler.Parsing.CodeGeneration
{
    public class CCodeGenerator : ICodeGenerator
    {
        private readonly List<VariableDeclaration> variableDeclarations = new List<VariableDeclaration>();
        private readonly HashSet<string> imports = new HashSet<string>();

        public string GenerateCode(ProgramExpression program)
        {
            Traverse(program);

            var body = Visit(program);

            const string source = @"
{{#each imports}}
#include <{{.}}>
{{/each}}

int main() {
{{#each variableDeclarations}}
    {{type}} {{name}};
{{/#each}}

    {{body}}
    
    return 0;
}
";
            var template = Handlebars.Compile(source);

            var data = new
            {
                body,
                imports = imports.ToArray(),
                variableDeclarations = variableDeclarations.ToArray()
            };

            return template(data);
        }

        public string Visit(IExpression expression)
        {
            var visitors = new List<IVisitor>()
            {
                new AssignmentVisitor(),
                new CallVisitor(),
                new NameVisitor(),
                new NumberVisitor(),
                new OperatorVisitor(),
                new ProgramVisitor()
            };

            var visitor = visitors.FirstOrDefault(v => v.GetExpressionType() == expression.GetType());

            if (visitor == null)
            {
                return "";
            }
            
            return visitor.Enter(this, expression) + visitor.Exit(this, expression);
        }

        private void Traverse(IExpression expression)
        {
            if (expression is AssignmentExpression assignment)
            {
                var isPresent = variableDeclarations.Exists(d => d.name.Equals(assignment.name));

                if (isPresent)
                {
                    return;
                }

                var value = ((NumberExpression) assignment.right).value;
                var type = value.Contains(".") ? "double" : "int";

                variableDeclarations.Add(new VariableDeclaration(assignment.name, type, value));
            }

            if (expression is CallExpression callExpression)
            {
                if (callExpression.function.name.Equals("println"))
                {
                    imports.Add("stdio.h");
                }

                callExpression.arguments.ForEach(Traverse);
            }

            if (expression is ProgramExpression program)
            {
                program.body.ForEach(Traverse);
            }
        }
    }
}