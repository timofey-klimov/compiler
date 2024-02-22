namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public sealed class ParenthisedExpression : ExpressionSyntax
    {
        public ParenthisedExpression(SyntaxToken openParenthis, SyntaxNode expression, SyntaxToken closeParenthis)
        {
            OpenParenthis = openParenthis;
            Expression = expression;
            CloseParenthis = closeParenthis;
        }

        public SyntaxToken OpenParenthis { get; }

        public SyntaxNode Expression { get; }

        public SyntaxToken CloseParenthis { get; }

        public override SyntaxKind SyntaxKind => SyntaxKind.ParenthisedExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthis;
            yield return Expression;
            yield return CloseParenthis;
        }
    }
}
