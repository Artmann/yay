using System;
using Compiler.Parsing;

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

                println(a + b)
            ";

            var tokens = new Lexer().Tokenize(input);
            var program = new Parser().Parse(tokens);
            
            Console.WriteLine(program);
        }
    }
}