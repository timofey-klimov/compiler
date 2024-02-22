namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind SyntaxKind { get; }
        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
    public abstract class ExpressionSyntax : SyntaxNode { }
}
