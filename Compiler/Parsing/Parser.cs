using System;
using System.Collections.Generic;
using Compiler.Parsing.Parselets;

namespace Compiler.Parsing
{
    public class Parser
    {
        private readonly Dictionary<TokenType, IInfixParselet> infixParselets;
        private readonly Dictionary<TokenType, IPrefixParselet> prefixParselets;

        private List<Token> read;
        private Token[] tokens;

        private int tokenIndex = 0;

        public Parser()
        {
            infixParselets = new Dictionary<TokenType, IInfixParselet>();
            prefixParselets = new Dictionary<TokenType, IPrefixParselet>();
            read = new List<Token>();

            Register(TokenType.Identifier, new NameParselet());
            Register(TokenType.Number, new NumberParselet());
            Register(TokenType.Equal, new AssignmentParselet());
            Register(TokenType.LeftParenthesis, new GroupParselet());
            Register(TokenType.LeftParenthesis, new CallParselet());

            RegisterPrefix(TokenType.Add, Precedence.Prefix);
            RegisterPrefix(TokenType.Minus, Precedence.Prefix);

            RegisterInfixLeft(TokenType.Add, Precedence.Sum);
            RegisterInfixLeft(TokenType.Minus, Precedence.Sum);
            RegisterInfixLeft(TokenType.Multiply, Precedence.Product);
            RegisterInfixLeft(TokenType.Divide, Precedence.Product);
        }

        public Token Consume(TokenType type)
        {
            var token = LookAhead(0);

            if (token == null)
            {
                return null;
            }

            if (token.type != type)
            {
                throw new Exception($"Expect token {type} but got {token.type}");
            }

            return Consume();
        }

        public bool Match(TokenType type)
        {
            var token = LookAhead(0);

            if (token == null)
            {
                return false;
            }

            if (token.type != type)
            {
                return false;
            }

            Consume();

            return true;
        }

        public ProgramExpression Parse(Token[] tokens)
        {
            this.tokens = tokens;
            
            read = new List<Token>();
            tokenIndex = 0;

            var program = new ProgramExpression();

            IExpression expression;
            while ((expression = ParseExpression()) != null)
            {
                program.body.Add(expression);
            }

            return program;
        }

        public IExpression ParseExpression(int precedence = 0)
        {
            var token = Consume();

            if (token == null)
            {
                return null;
            }

            if (!prefixParselets.ContainsKey(token.type))
            {
                throw new Exception($"Could not parse {token.value}.");
            }

            var prefix = prefixParselets[token.type];

            var left = prefix.Parse(this, token);

            while (precedence < GetPrecedence())
            {
                token = Consume();

                var infix = infixParselets[token.type];

                left = infix.Parse(this, left, token);
            }

            return left;
        }

        private Token Consume()
        {
            if (tokenIndex >= tokens.Length)
            {
                return null;
            }

            LookAhead(0);

            if (read.Count == 0)
            {
                return null;
            }

            var token = read[0];

            read.RemoveAt(0);

            return token;
        }

        private int GetPrecedence()
        {
            var token = LookAhead(0);

            if (token == null)
            {
                return 0;
            }

            if (!infixParselets.ContainsKey(token.type))
            {
                return 0;
            }

            var parser = infixParselets[token.type];

            return parser.GetPrecedence();
        }

        private Token LookAhead(int distance)
        {
            if (tokenIndex >= tokens.Length)
            {
                return null;
            }

            while (distance >= read.Count)
            {
                read.Add(tokens[tokenIndex++]);
            }

            return read[distance];
        }

        private void Register(TokenType token, IPrefixParselet parselet)
        {
            prefixParselets.Add(token, parselet);
        }

        private void Register(TokenType token, IInfixParselet parselet)
        {
            infixParselets.Add(token, parselet);
        }

        private void RegisterInfixLeft(TokenType type, int precedence)
        {
            Register(type, new BinaryOperatorParselet(precedence, false));
        }

        private void RegisterInfixRight(TokenType type, int precedence)
        {
            Register(type, new BinaryOperatorParselet(precedence, true));
        }

        private void RegisterPrefix(TokenType tokenType, int precedence)
        {
            Register(tokenType, new PrefixOperatorParselet(precedence));
        }

        private void RegisterPostfix(TokenType tokenType, int precedence)
        {
            Register(tokenType, new PostfixOperatorParselet(precedence));
        }
    }
}