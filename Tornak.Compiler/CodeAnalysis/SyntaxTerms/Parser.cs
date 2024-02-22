namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Parser(ILexer lexer)
        {
            _tokens = lexer.ParseSyntaxTokens().ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public SyntaxTree Parse()
        {
            var root = ParseExpression();
            var endOfFile = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(root, endOfFile, _diagnostics);
        }

        private SyntaxNode ParseExpression(int previosWeight = 0)
        {
            SyntaxNode left;
            var unaryWeight = Current.SyntaxKind.GetUnaryOperatorWeight();
            if (unaryWeight == 0 || unaryWeight < previosWeight)
            {
                left = ParseLiteral();
            }
            else
            {
                var @operator = Next();
                var operand = ParseExpression(unaryWeight);
                left = new UnaryExpressionSyntax(@operator, operand);
            }
            while (true)
            {
                var weight = Current.SyntaxKind.GetBinaryOperatorWeight();
                if (weight == 0 || previosWeight >= weight)
                    break;
                var @operator = Next();
                var right = ParseExpression(weight);
                left = new BinaryExpressionSyntax(left, @operator, right);
            }
            return left;
        }

        private SyntaxNode ParseLiteral()
        {
            if (Current.SyntaxKind == SyntaxKind.OpenParenthisToken)
            {
                var openParenthis = Next();
                var expression = ParseExpression();
                var closeParenthis = Match(SyntaxKind.CloseParenthesisToken);
                return new ParenthisedExpression(openParenthis, expression, closeParenthis);
            }
            else if (Current is { SyntaxKind: 
                SyntaxKind.TrueKeyword or 
                SyntaxKind.FalseKeyword or 
                SyntaxKind.IdentifierToken })
            {
                var currentToken = Next();
                return new LiteralExpressionSyntax(currentToken);
            }
            var literalToken = Match(SyntaxKind.LiteralToken);
            return new LiteralExpressionSyntax(literalToken);
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken Peek(int offset)
        {
            var position = _position + offset;
            if (position >= _tokens.Length)
                return _tokens[_tokens.Length - 1];
            return _tokens[position];
        }

        private SyntaxToken Next()
        {
            var token = Current;
            _position++;
            return token;
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            var current = Next();
            if (current.SyntaxKind == kind)
                return current;
            _diagnostics.Add($"Unexpected token <{Current.SyntaxKind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }
    }
}
