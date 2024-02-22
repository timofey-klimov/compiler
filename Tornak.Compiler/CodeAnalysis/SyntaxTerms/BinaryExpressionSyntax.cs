namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public BinaryExpressionSyntax(SyntaxNode left, SyntaxToken @operator, SyntaxNode right)
        {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        public override SyntaxKind SyntaxKind => SyntaxKind.BinaryExpression;
        public SyntaxNode Left { get; }
        public SyntaxToken Operator { get; }
        public SyntaxNode Right { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return Operator;
            yield return Right;
        }
    }
}
