using ConsoleApp6.Templates.CSharpTypeMembers;
using Humanizer;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    public class CSharpStaticFunction : IRepositoryGroup
    {
        public string Description { get; set; }
        List<EnvironmentVariable> EnvironmentVariables { get; set; }
        public List<Field> Fields { get; set; }
        public string Name { get; set; }
        public List<PackageReference> PackageReferences { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string ReturnType { get; set; }
        public List<string> SameAccountDependencies { get; set; }
        public List<string> Statements { get; set; }
        public List<string> TypeParameters { get; set; }
        public List<string> UsingDirectives { get; set; }
        public List<string> UsingStaticDirectives { get; set; }
        public string Version { get; set; }

        public IEnumerable<ICodeGeneratable> GetRepositories()
        {
            var isAsync = IsFunctionAsync();
            var arrowClause = Statements.Count > 1 ? null
                : Statements.First();
            if (arrowClause != null && arrowClause.StartsWith("return "))
                arrowClause = arrowClause.Replace("return ", string.Empty);
            var block = arrowClause != null ? null : Statements;
            var names = GetTypeAndMethodNames();
            var methods = new List<Method>
            {
                new Method
                {
                    ArrowClauseExpression = arrowClause,
                    Block = block,
                    Comment = Description,
                    Modifiers = isAsync
                        ? "public static async"
                        : "public static",
                    Name = names.Last(),
                    Parameters = Parameters,
                    Type = ReturnType,
                    TypeParameters = TypeParameters
                }
            };
            yield return new StaticFunction(
                Name, names.First(), Description, Version, EnvironmentVariables, PackageReferences, SameAccountDependencies, fields: Fields, methods: methods, usingDirectives: UsingDirectives, usingStaticDirectives: UsingStaticDirectives);
        }

        private IEnumerable<string> GetTypeAndMethodNames()
        {
            // "Me.MyProduct.GetConnection" => "ConnectionGetter"
            var words = Name
                .Split('.').Last() // => "GetConnection"
                .Humanize(LetterCasing.Title) // => "Get Connection"
                .Split(' ');
            var agentNoun = words.First() // => "Get"
                .GetAgentNoun(); // => "Getter
            var methodName = string.Join(string.Empty, words.Skip(1));
            yield return $"{methodName}{agentNoun}"; // "ConnectionGetter"
            yield return methodName; // "Connection"
        }

        private bool IsFunctionAsync()
            => ReturnType == "Task" || ReturnType.StartsWith("Task<");
    }
}
