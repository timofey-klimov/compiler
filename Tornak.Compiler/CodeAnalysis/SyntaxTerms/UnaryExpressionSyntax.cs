namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public sealed class UnaryExpressionSyntax : ExpressionSyntax
    {
        public UnaryExpressionSyntax(SyntaxToken @operator, SyntaxNode operand)
        {
            Operator = @operator;
            Operand = operand;
        }
        public SyntaxToken Operator { get; }

        public SyntaxNode Operand { get; }

        public override SyntaxKind SyntaxKind => SyntaxKind.UnaryExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Operator;
            yield return Operand;
        }
    }
}
