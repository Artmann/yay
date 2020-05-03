using System;
using System.IO;
using Compiler.Parsing;
using Compiler.Parsing.CodeGeneration;

namespace Compiler
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const string input = @"
                a = 1
                b = 3
                c = 7

                println(a + b)
                println(a + b - c)
                println(a, b, c)
            ";

            var tokens = new Lexer().Tokenize(input);
            var program = new Parser().Parse(tokens);
            var output = new CCodeGenerator().GenerateCode(program);

            Console.WriteLine(output.Replace("&quot;", "\""));
            
            /*
            var path = $"{Directory.GetCurrentDirectory()}\\program.c";
            
            System.IO.File.WriteAllText(path, output);
            
            Console.WriteLine($"Wrote program to ${path}.");
            */
        }
    }
}