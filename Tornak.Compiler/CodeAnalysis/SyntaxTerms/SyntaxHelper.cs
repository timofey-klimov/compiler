namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public static class SyntaxHelper
    {
        public static int GetBinaryOperatorWeight(this SyntaxKind syntaxKind) =>
            syntaxKind switch
            {
                SyntaxKind.CaretToken => 6,
                SyntaxKind.AmpersansAmpersandToken => 5,
                SyntaxKind.PipePipeToken => 4,
                SyntaxKind.EqualsEqualsToken => 3,
                SyntaxKind.BangEqualsToken => 3,
                SyntaxKind.BadToken => 3,
                SyntaxKind.SlashToken => 2,
                SyntaxKind.StarToken => 2,
                SyntaxKind.MinusToken => 1,
                SyntaxKind.PlusToken => 1,
                _ => 0
            };

        public static int GetUnaryOperatorWeight(this SyntaxKind syntaxKind) =>
            syntaxKind switch
            {
                SyntaxKind.BangToken => 7,
                SyntaxKind.MinusToken => 6,
                SyntaxKind.PlusToken => 6,
                _ => 0
            };

        public static SyntaxKind GetKeyWordKind(this string text) =>
            text.ToLower() switch
            {
                "true" => SyntaxKind.TrueKeyword,
                "false" => SyntaxKind.FalseKeyword,
                _ => SyntaxKind.IdentifierToken
            };
    }
}
