using Tornak.Compiler.CodeAnalysis.SyntaxTerms;

namespace Tornak.Compiler.CodeAnalysis.Binding
{
    public abstract class BoundNode
    {
        public abstract BoundKind BoundKind { get; }
    }

    public enum BoundKind
    {
        UnaryExpression,
        BinaryExpression,
        LiteralExpression
    }

    public abstract class BoundExpression : BoundNode
    {
        public abstract Type? Type { get; }
    }

    internal class Binder
    {
        private List<string> _diagnostics = new List<string>();
        public IEnumerable<string> Diagnostics => _diagnostics;
        public BoundExpression BindExpression(SyntaxNode syntax)
        {
            return syntax switch
            {
                LiteralExpressionSyntax literal => BindLiteralExpression(literal),
                UnaryExpressionSyntax unary => BindUnaryExpression(unary),
                BinaryExpressionSyntax binary => BindBinaryExpression(binary),
                ParenthisedExpression parenthised => BindExpression(parenthised.Expression),
                _ => throw new Exception($"Invalud syntax kind <{syntax.SyntaxKind}>")
            };
        }

        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax binary)
        {
            var left = BindExpression(binary.Left);
            var right = BindExpression(binary.Right);
            var binaryOperator = BinaryBoundOperator.Bind(binary.Operator.SyntaxKind, left.Type, right.Type);
            if (binaryOperator == null)
            {
                _diagnostics.Add($"Operator {binary.Operator.Text} is not defined for type {left.Type} and {right.Type}");
                return left;
            }
            return new BinaryBoundExpression(left, binaryOperator, right);
        }

        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax unary)
        {
            var operand = BindExpression(unary.Operand);
            var unaryOperator = UnaryBoundOperator.Bind(unary.Operator.SyntaxKind, operand.Type);
            if (unaryOperator is null)
            {
                _diagnostics.Add($"Operator {unary.Operator.Text} is not defined for type {operand.Type}");
                return operand;
            }
            return new UnaryBoundExpression(unaryOperator, operand);
        }

        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax literal)
        {
            var value = literal.LiteralToken.Value;
            return new LiteralBoundExpression(value);
        }
    }
}
