using Tornak.Compiler.CodeAnalysis.SyntaxTerms;

namespace Tornak.Compiler.CodeAnalysis.Binding
{
    public class UnaryBoundOperator
    {
        public Type OperandType { get; }

        public Type ResultType { get; }

        public SyntaxKind SyntaxKind { get; }

        public BoundUnaryOperatorKind UnaryOperatorKind { get; }

        private UnaryBoundOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind operatorKind, Type operandType)
            :this(syntaxKind, operatorKind, operandType, operandType) { }

        private UnaryBoundOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind operatorKind, Type operandType, Type resultType)
        {
            OperandType = operandType;
            ResultType = resultType;
            SyntaxKind = syntaxKind;
            UnaryOperatorKind = operatorKind;
        }

        public static UnaryBoundOperator? Bind(SyntaxKind kind, Type operandType)
        {
            return _operators.FirstOrDefault(op => op.SyntaxKind == kind && op.OperandType == operandType);
        }

        private static UnaryBoundOperator[] _operators =
        {
            new UnaryBoundOperator(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Indentity, typeof(int)),
            new UnaryBoundOperator(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int)),
            new UnaryBoundOperator(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicalNot, typeof(bool)),
        };
    }

    public class BinaryBoundOperator
    {
        public Type Left { get; }

        public Type Right { get; }

        public Type ResultType { get; }

        public SyntaxKind SyntaxKind { get; }

        public BoundBinaryOperatorKind BinaryOperatorKind { get; }

        private BinaryBoundOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind binaryOperator, Type left, Type right, Type resultType)
        {
            Left = left;
            Right = right;
            ResultType = resultType;
            SyntaxKind = syntaxKind;
            BinaryOperatorKind = binaryOperator;
        }

        private BinaryBoundOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind binaryOperator, Type operandType, Type resultType)
            :this(syntaxKind, binaryOperator, operandType, operandType, resultType) { }

        private BinaryBoundOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind binaryOperator, Type type)
            : this(syntaxKind, binaryOperator, type, type, type) { }


        public static BinaryBoundOperator? Bind(SyntaxKind syntaxKind, Type left, Type right) =>
            _operators.FirstOrDefault(op => op.SyntaxKind == syntaxKind && op.Left == left && op.Right == right);
        


        private static BinaryBoundOperator[] _operators =
        {
            new BinaryBoundOperator(SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
            new BinaryBoundOperator(SyntaxKind.MinusToken, BoundBinaryOperatorKind.Substraction, typeof(int)),
            new BinaryBoundOperator(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
            new BinaryBoundOperator(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(string), typeof(int), typeof(string)),
            new BinaryBoundOperator(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int), typeof(string), typeof(string)),
            new BinaryBoundOperator(SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int), typeof(double)),
            new BinaryBoundOperator(SyntaxKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),
            new BinaryBoundOperator(SyntaxKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(bool)),
            new BinaryBoundOperator(SyntaxKind.AmpersansAmpersandToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
            new BinaryBoundOperator(SyntaxKind.PipePipeToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
            new BinaryBoundOperator(SyntaxKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquaks, typeof(bool)),
            new BinaryBoundOperator(SyntaxKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquaks, typeof(int), typeof(bool)),
            new BinaryBoundOperator(SyntaxKind.CaretToken, BoundBinaryOperatorKind.Exponention, typeof(int)),
        };
    }
}
