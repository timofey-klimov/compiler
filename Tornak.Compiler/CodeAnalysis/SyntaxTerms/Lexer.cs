namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public interface ILexer
    {
        public IEnumerable<SyntaxToken> ParseSyntaxTokens();
        IEnumerable<string> Diagnostics { get; }
    }

    public class Lexer : ILexer
    {
        private readonly string _text;
        private int _position;
        private const char EndOfFile = '\0';
        private List<string> _diagnostics = new List<string>();
        private char Current => Peek(0);
        private char LookAhead => Peek(1);

        public Lexer(string text) => _text = text;

        public IEnumerable<string> Diagnostics => _diagnostics;

        public IEnumerable<SyntaxToken> ParseSyntaxTokens()
        {
            SyntaxToken token;
            do
            {
                token = NextToken();
                if (token.SyntaxKind == SyntaxKind.BadToken || token.SyntaxKind == SyntaxKind.WhiteSpaceToken)
                    continue;
                yield return token;
            }
            while (token.SyntaxKind != SyntaxKind.EndOfFileToken);
        }
        private char Peek(int offset)
        {
            var position = _position + offset;
            if (position >= _text.Length)
                return EndOfFile;
            return _text[position];
        }

        private int NextPosition()
        {
            var currentPosition = _position;
            _position++;
            return currentPosition;
        }

        private SyntaxToken NextToken()
        {
            if (char.IsDigit(Current))
            {
                var startPosition = NextPosition();
                while (char.IsDigit(Current))
                    NextPosition();

                var text = _text.Substring(startPosition, _position - startPosition);

                if (!int.TryParse(text, out var number))
                {
                    _diagnostics.Add($"Invalid number {text}");
                }
                return new SyntaxToken(SyntaxKind.LiteralToken, startPosition, text, number);
            }

            if (char.IsWhiteSpace(Current))
            {
                var startPosition = NextPosition();
                while (char.IsWhiteSpace(Current))
                    NextPosition();
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, startPosition, null, null);
            }

            if (char.IsLetter(Current))
            {
                var startPosition = NextPosition();
                while (char.IsLetter(Current))
                    NextPosition();

                var text = _text.Substring(startPosition, _position - startPosition);

                var kind = text.GetKeyWordKind();

                object value = kind == SyntaxKind.IdentifierToken
                    ? text
                    : kind == SyntaxKind.TrueKeyword
                        ? true
                        : false;

                return new SyntaxToken(kind, startPosition, text, value);
            }

            switch (Current)
            {
                case ('\0'):
                    return new SyntaxToken(SyntaxKind.EndOfFileToken, _position - 1, null, null);
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, NextPosition(), "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, NextPosition(), "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, NextPosition(), "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, NextPosition(), "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthisToken, NextPosition(), "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, NextPosition(), ")", null);
                case '^':
                    return new SyntaxToken(SyntaxKind.CaretToken, NextPosition(), "^", null);
                case '!':
                    if (LookAhead == '=')
                        return new SyntaxToken(SyntaxKind.BangEqualsToken, _position += 2, "!=", null);
                    else
                        return new SyntaxToken(SyntaxKind.BangToken, NextPosition(), "!", null);
                case '=':
                    if (LookAhead == '=')
                        return new SyntaxToken(SyntaxKind.EqualsEqualsToken, _position += 2, "==", null);
                    break;
                case '&':
                    if (LookAhead == '&')
                        return new SyntaxToken(SyntaxKind.AmpersansAmpersandToken, _position += 2, "&&", null);
                    break;
                case '|':
                    if (LookAhead == '|')
                        return new SyntaxToken(SyntaxKind.PipePipeToken, _position += 2, "||", null);
                    break;
            }

            _diagnostics.Add($"ERROR: Bad syntax token: {Current}");
            return new SyntaxToken(SyntaxKind.BadToken, NextPosition(), null, null);
        }

    }
}
