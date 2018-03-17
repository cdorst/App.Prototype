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
        public CSharpInterface(string @namespace, string typeName, string description, string version, List<PackageReference> externalDependencies = null, IEnumerable<string> sameAccountDependencies = null, List<CSharpTypeMembers.Attribute> attributes = null, List<Base> bases = null, List<ConstraintClause> constraintClauses = null, List<Method> methods = null, List<Property> properties = null, List<string> typeParameters = null, List<string> usingDirectives = null) : base(@namespace, typeName, description, version, null, externalDependencies, sameAccountDependencies, attributes, bases, constraintClauses, methods, properties, typeParameters, usingDirectives, null)
        {
        }

        public override Func<IDictionary<string, ITemplate>, TemplateContent> GetContent()
            => repositories
                => new TemplateContent(Interface(
                    Name, Description, Version, GetPackages(repositories), TypeName, GetEnvironmentVariables(), GetUsingDirectiveList(), Comments.Summary(Description), GetAttributeListCollection(), GetTypeParameterList(), GetConstraintClauseList(), GetBaseList(), GetMethodList(), GetPropertyList()));
    }
}
