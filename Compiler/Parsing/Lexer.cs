using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compiler.Parsing
{
    public class Lexer
    {
        private readonly List<TokenDefinition> tokenDefinitions;
        private readonly List<RecurringTokenDefinition> recurringTokenDefinitions;
        
        public Lexer()
        {
            tokenDefinitions = new List<TokenDefinition>()
            {
                new TokenDefinition(TokenType.Add, "+"),
                new TokenDefinition(TokenType.Comma, ","),
                new TokenDefinition(TokenType.Divide, "/"),
                new TokenDefinition(TokenType.Equal, "="),
                new TokenDefinition(TokenType.Minus, "-"),
                new TokenDefinition(TokenType.Multiply, "*"),
                new TokenDefinition(TokenType.LeftParenthesis, "("),
                new TokenDefinition(TokenType.RightParenthesis, ")"),
                new TokenDefinition(TokenType.Semicolon, ";")
            };
            
            recurringTokenDefinitions = new List<RecurringTokenDefinition>()
            {
                new RecurringTokenDefinition(TokenType.Number, new Regex("[0-9]", RegexOptions.Compiled)),
                new RecurringTokenDefinition(
                    TokenType.Identifier,
                    new Regex("[a-zA-Z]", RegexOptions.Compiled),
                    new Regex("[a-zA-Z0-9]", RegexOptions.Compiled)
                )
            };
        }

        public Token[] Tokenize(string source)
        {
            var tokens = new List<Token>();

            Token currentToken = null;
            var cursor = 0;
            var line = 0;

            while (cursor < source.Length)
            {
                var ch = source[cursor].ToString();

                if (currentToken == null)
                {
                    var nextToken = GetNextToken(ch);

                    if (nextToken != null)
                    {
                        currentToken = nextToken;

                        cursor++;
                        continue;
                    }
                }

                if (currentToken != null)
                {
                    if (IsContinuationOfToken(currentToken, ch))
                    {
                        currentToken = new Token(currentToken.type, currentToken.value + ch);

                        cursor++;
                        continue;
                    }

                    tokens.Add(currentToken);

                    currentToken = null;
                }

                var definition = tokenDefinitions.FirstOrDefault(d => d.test.Equals(ch));

                if (definition != null)
                {
                    tokens.Add(new Token(definition.type, ch));

                    cursor++;
                    continue;
                }

                if (new Regex(@"\s", RegexOptions.Compiled).IsMatch(ch))
                {
                    cursor++;
                    continue;
                }
                
                throw new Exception($"Could not parse '{ch}' at line { line }.");
            }

            return tokens.ToArray();
        }

        private Token GetNextToken(string ch)
        {
            var definition = recurringTokenDefinitions.FirstOrDefault(d => d.start.IsMatch(ch));

            return definition != null ? new Token(definition.type, ch) : null;
        }

        private bool IsContinuationOfToken(Token token, string ch)
        {
            var definition = recurringTokenDefinitions.FirstOrDefault(d => d.type == token.type);

            return definition != null && definition.next.IsMatch(ch);
        }
    }
}