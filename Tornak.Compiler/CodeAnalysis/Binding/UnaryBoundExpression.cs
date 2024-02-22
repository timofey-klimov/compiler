namespace Tornak.Compiler.CodeAnalysis.Binding
{
    public sealed class UnaryBoundExpression : BoundExpression
    {
        public UnaryBoundExpression(UnaryBoundOperator unaryOperator, BoundExpression operand)
        {
            Operand = operand;
            Operator = unaryOperator;
        }
        public BoundExpression Operand { get; }
        public UnaryBoundOperator Operator { get; }
        public override Type Type => Operator.ResultType;

        public override BoundKind BoundKind => BoundKind.UnaryExpression;
    }

}
