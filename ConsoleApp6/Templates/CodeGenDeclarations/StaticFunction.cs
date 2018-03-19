using ConsoleApp6.Templates.CSharpTypeMembers;
using Humanizer;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CodeGenDeclarations
{
    public class StaticFunction : ICodeGeneratable
    {
        public StaticFunction() { }
        public StaticFunction(string @namespace, string description, string version, List<EnvironmentVariable> environmentVariables = null, List<PackageReference> externalDependencies = null, IEnumerable<string> sameAccountDependencies = null, List<Attribute> attributes = null, List<Base> bases = null, List<Constructor> constructors = null, List<Field> fields = null, List<ConstraintClause> constraintClauses = null, List<Method> methods = null, List<Property> properties = null, List<string> typeParameters = null, List<string> usingDirectives = null, List<string> usingStaticDirectives = null, List<string> finalizerBlock = null)
        {
            Description = description;
            Name = @namespace;
            Version = version;
            PackageReferences = externalDependencies;
            SameAccountDependencies = sameAccountDependencies?.ToList();
            Attributes = attributes;
            Bases = bases;
            ConstraintClauses = constraintClauses;
            Constructors = constructors;
            Fields = fields;
            FinalizerBlock = finalizerBlock;
            Methods = methods;
            Properties = properties;
            TypeParameters = typeParameters;
            UsingDirectives = usingDirectives;
            UsingStaticDirectives = usingStaticDirectives;
        }

        public string Description { get; set; }
        public List<EnvironmentVariable> EnvironmentVariables { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public List<PackageReference> PackageReferences { get; set; }
        public List<string> SameAccountDependencies { get; set; }

        public List<CSharpTypeMembers.Attribute> Attributes { get; set; }
        public List<Base> Bases { get; set; }
        public List<ConstraintClause> ConstraintClauses { get; set; }
        public List<Constructor> Constructors { get; set; }
        public List<Field> Fields { get; set; }
        public List<string> FinalizerBlock { get; set; }
        public List<Method> Methods { get; set; }
        public List<Property> Properties { get; set; }
        public List<string> TypeParameters { get; set; }
        public List<string> UsingDirectives { get; set; }
        public List<string> UsingStaticDirectives { get; set; }

        public string GetTypeName() // "Me.MyProduct.FooBarDoer" => "DoFooBar"
            => Name
                .Split('.').Last() // => "FooBarDoer"
                .Humanize(LetterCasing.Title) // => "Foo Bar Doer"
                .Split(' ').Last()
                .GetVerb(); // => "Doer"

        public RepositoryDeclaration GetDeclaration()
            => new CSharpClass(Name, GetTypeName(), Description, Version, isStatic: true, EnvironmentVariables, PackageReferences, SameAccountDependencies.ToArray(), Attributes, Bases, Constructors, Fields, ConstraintClauses, Methods, Properties, TypeParameters, UsingDirectives, UsingStaticDirectives, FinalizerBlock);
    }
}
