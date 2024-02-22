namespace Tornak.Compiler.CodeAnalysis.Binding
{
    public sealed class LiteralBoundExpression : BoundExpression
    {
        public LiteralBoundExpression(object? value)
        {
            Value = value;
        }
        public object? Value { get; }
        public override Type? Type => Value?.GetType();

        public override BoundKind BoundKind => BoundKind.LiteralExpression;
    }

}
