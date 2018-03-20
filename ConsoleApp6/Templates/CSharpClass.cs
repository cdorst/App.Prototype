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
        public CSharpClass(string @namespace, string typeName, string description, string version, bool isStatic = true, List<EnvironmentVariable> environmentVariables = null, List<PackageReference> externalDependencies = null, IEnumerable<string> sameAccountDependencies = null, List<CSharpTypeMembers.Attribute> attributes = null, List<Base> bases = null, List<Constructor> constructors = null, List<Field> fields = null, List<ConstraintClause> constraintClauses = null, List<Method> methods = null, List<Property> properties = null, List<string> typeParameters = null, List<string> usingDirectives = null, List<string> usingStaticDirectives = null, List<string> finalizerBlock = null) : base(@namespace, typeName, description, version, environmentVariables, externalDependencies, sameAccountDependencies, attributes, bases, constraintClauses, methods, properties, typeParameters, usingDirectives, usingStaticDirectives, constructors, fields)
        {
            FinalizerBlock = finalizerBlock;
            IsStatic = isStatic;
        }

        public List<string> FinalizerBlock { get; set; }
        public bool IsStatic { get; set; }

        public override Func<string, IDictionary<string, ITemplate>, TemplateContent> GetContent()
            => (accountName, repositories)
                => new TemplateContent(Class(
                    Name, Description, Version, GetPackages(repositories, accountName), TypeName, GetEnvironmentVariables(), IsStatic, GetUsingDirectiveList(), Comments.Summary(Description), GetAttributeListCollection(), GetTypeParameterList(), GetConstraintClauseList(), GetBaseList(), GetConstructorList(), GetFieldList(), GetMethodList(), GetPropertyList(), GetFinalizer(FinalizerBlock)));
    }
}
