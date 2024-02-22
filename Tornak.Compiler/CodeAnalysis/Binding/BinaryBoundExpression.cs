namespace Tornak.Compiler.CodeAnalysis.Binding
{
    public sealed class BinaryBoundExpression : BoundExpression
    {
        public BinaryBoundExpression(BoundExpression left, BinaryBoundOperator binaryOperator, BoundExpression right)
        {
            Left = left;
            Operator = binaryOperator;
            Right = right;
        }

        public BoundExpression Left { get; }

        public BinaryBoundOperator Operator { get; }

        public BoundExpression Right { get; }
        public override Type Type => Operator.ResultType;

        public override BoundKind BoundKind => BoundKind.BinaryExpression;
    }

}
