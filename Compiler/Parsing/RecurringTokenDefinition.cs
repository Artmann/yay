using System.Text.RegularExpressions;

namespace Compiler.Parsing
{
    public class RecurringTokenDefinition
    {
        public readonly TokenType type;
        public readonly Regex start;
        public readonly Regex next;

        public RecurringTokenDefinition(TokenType type, Regex start, Regex next)
        {
            this.type = type;
            this.start = start;
            this.next = next;
        }
        
        public RecurringTokenDefinition(TokenType type, Regex start)
        {
            this.type = type;
            this.start = start;
            this.next = start;
        }
    }
}