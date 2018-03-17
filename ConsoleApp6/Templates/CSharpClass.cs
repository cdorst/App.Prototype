using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp.Helpers.Common;
using System;
using System.Collections.Generic;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.CodeBuilder;

namespace ConsoleApp6.Templates
{
    public class CSharpClass : CSharpType
    {
        public CSharpClass() { }
        public CSharpClass(string @namespace, string typeName, string description, string version, bool isStatic, List<EnvironmentVariable> environmentVariables, List<PackageReference> externalDependencies, IEnumerable<string> sameAccountDependencies, List<CSharpTypeMembers.Attribute> attributes, List<Base> bases, List<ConstraintClause> constraintClauses, List<Method> methods, List<Property> properties, List<string> typeParameters, List<string> usingDirectives, List<string> usingStaticDirectives) : base(@namespace, typeName, description, version, environmentVariables, externalDependencies, sameAccountDependencies, attributes, bases, constraintClauses, methods, properties, typeParameters, usingDirectives, usingStaticDirectives)
        {
            IsStatic = isStatic;
        }

        public bool IsStatic { get; set; }
        public List<string> FinalizerBlock { get; set; }

        public override Func<IDictionary<string, ITemplate>, TemplateContent> GetContent()
            => repositories
                => new TemplateContent(Class(
                    Name, Description, Version, GetPackages(repositories), TypeName, GetEnvironmentVariables(), IsStatic, GetUsingDirectiveList(), Comments.Summary(Description), GetAttributeListCollection(), GetTypeParameterList(), GetConstraintClauseList(), GetBaseList(), GetConstructorList(), GetFieldList(), GetMethodList(), GetPropertyList(), GetFinalizer(FinalizerBlock)));
    }
}
