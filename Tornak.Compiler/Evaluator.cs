using Tornak.Compiler.CodeAnalysis.Binding;

namespace Tornak.Compiler
{
    public static class Evaluator
    {
        public static object? Evaluate(BoundExpression boundExpression)
        {
            if (boundExpression is LiteralBoundExpression literal)
                return literal.Value;


            if (boundExpression is UnaryBoundExpression unary)
            {
                var value = Evaluate(unary.Operand);
                return unary.Operator.UnaryOperatorKind switch
                {
                    BoundUnaryOperatorKind.Negation => -(int)value,
                    BoundUnaryOperatorKind.Indentity => (int)value,
                    BoundUnaryOperatorKind.LogicalNot => !(bool)value,
                    _ => throw new Exception($"Invalid unary operator <{unary.Operator}>")
                };
            }

            var binaryExp = boundExpression as BinaryBoundExpression;
            var left = Evaluate(binaryExp.Left);
            var right = Evaluate(binaryExp.Right);

            int Exponent(int number, int degree)
            {
                for (int i = 1; i <= degree; i++)
                {
                    number *= i;
                }
                return number;
            }

            return binaryExp.Operator.BinaryOperatorKind switch
            {
                BoundBinaryOperatorKind.Addition => (int)left! + (int)right!,
                BoundBinaryOperatorKind.Substraction => (int)left! - (int)right!,
                BoundBinaryOperatorKind.Multiplication => (int)left! * (int)right!,
                BoundBinaryOperatorKind.Division => (int)left! / (int)right!,
                BoundBinaryOperatorKind.Equals => left!.Equals(right),
                BoundBinaryOperatorKind.LogicalAnd => (bool)left! && (bool)right!,
                BoundBinaryOperatorKind.LogicalOr => (bool)left! || (bool)right!,
                BoundBinaryOperatorKind.NotEquaks => !left!.Equals(right),
                BoundBinaryOperatorKind.Exponention => Exponent((int)left, (int)right),
                _ => throw new Exception($"Invalid binary operator <{binaryExp.Operator}>")
            };

        }
    }
}
