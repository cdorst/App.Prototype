using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp.Helpers.Common;
using System;
using System.Collections.Generic;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.CodeBuilder;

namespace ConsoleApp6.Templates
{
    public class CSharpInterface : CSharpType
    {
        public CSharpInterface() { }
        public CSharpInterface(string @namespace, string typeName, string description, string version, List<PackageReference> externalDependencies, IEnumerable<string> sameAccountDependencies, List<CSharpTypeMembers.Attribute> attributes, List<Base> bases, List<ConstraintClause> constraintClauses, List<Method> methods, List<Property> properties, List<string> typeParameters, List<string> usingDirectives) : base(@namespace, typeName, description, version, null, externalDependencies, sameAccountDependencies, attributes, bases, constraintClauses, methods, properties, typeParameters, usingDirectives, null)
        {
        }

        public override Func<IDictionary<string, ITemplate>, TemplateContent> GetContent()
            => repositories
                => new TemplateContent(Interface(
                    Name, Description, Version, GetPackages(repositories), TypeName, GetEnvironmentVariables(), GetUsingDirectiveList(), Comments.Summary(Description), GetAttributeListCollection(), GetTypeParameterList(), GetConstraintClauseList(), GetBaseList(), GetMethodList(), GetPropertyList()));
    }
}
