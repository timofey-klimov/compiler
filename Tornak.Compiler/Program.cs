using Tornak.Compiler.CodeAnalysis.Binding;
using Tornak.Compiler.CodeAnalysis.SyntaxTerms;

namespace Tornak.Compiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("< ");
                var text = Console.ReadLine();
                var lexer = new Lexer(text);
                var parser = new Parser(lexer);
                var syntaxTree = parser.Parse();
                PrettyPrint(syntaxTree.Root);
                var binder = new Binder();
                var boundExpression = binder.BindExpression(syntaxTree.Root);
                var diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics).ToArray();
                if (diagnostics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var diagnostic in diagnostics)
                        Console.WriteLine(diagnostic);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    continue;
                }
;               var result = Evaluator.Evaluate(boundExpression);
                Console.WriteLine(result);
            }
        }

        static void PrettyPrint(SyntaxNode node, string depth = "")
        {
            Console.Write(depth);
            Console.Write(node.SyntaxKind);
            if (node is SyntaxToken token)
            {
                if (token.Text != null)
                {
                    Console.Write(" ");
                    Console.WriteLine(token.Value);
                }

            }
            else
            {
                Console.WriteLine();
                var chidren = node.GetChildren();
                depth += "   ";
                foreach (var child in chidren)
                    PrettyPrint(child, depth);
            }
        }
    }
}