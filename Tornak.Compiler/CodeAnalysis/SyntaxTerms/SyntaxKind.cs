namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public enum SyntaxKind
    {
        //Tokens
        LiteralToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthisToken,
        CloseParenthesisToken,
        EndOfFileToken,
        BadToken,
        WhiteSpaceToken,
        IdentifierToken,
        BangToken,
        BangEqualsToken,
        EqualsEqualsToken,
        AmpersansAmpersandToken,
        PipePipeToken,
        CaretToken,

        //Keywords
        TrueKeyword,
        FalseKeyword,

        //Expressions
        LiteralExpression,
        BinaryExpression,
        ParenthisedExpression,
        UnaryExpression
    }
}
