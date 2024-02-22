namespace Tornak.Compiler.CodeAnalysis.SyntaxTerms
{
    public record SyntaxTree(SyntaxNode Root, SyntaxToken EndOfFile, IEnumerable<string> Diagnostics);
}
