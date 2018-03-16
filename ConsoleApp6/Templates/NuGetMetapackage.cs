using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp;
using DevOps.Primitives.CSharp.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.CodeBuilder;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.MetapackageBuilder;

namespace ConsoleApp6.Templates
{
    public abstract class CSharpType : RepositoryDeclaration
    {
        public CSharpType() { }
        public CSharpType(string @namespace, string typeName, string description, string version, List<EnvironmentVariable> environmentVariables, List<PackageReference> externalDependencies, params string[] sameAccountDependencies) : base(@namespace, description, version, environmentVariables, externalDependencies, sameAccountDependencies)
        {
            TypeName = typeName;
        }

        public List<CSharpTypeMembers.Attribute> Attributes { get; set; }
        public List<CSharpTypeMembers.ConstraintClause> ConstraintClauses { get; set; }
        public string TypeName { get; set; }
        public List<string> TypeParameters { get; set; }
        public List<string> UsingDirectives { get; set; }
        public List<string> UsingStaticDirectives { get; set; }

        protected AttributeListCollection GetAttributeListCollection()
            => !Any(Attributes) ? null
                : AttributeLists.Create(Attributes.Select(attr => new DevOps.Primitives.CSharp.Attribute(attr.Name, attr.ArgumentListExpression)).ToArray());

        protected ConstraintClauseList GetConstraintClauseList()
            => !Any(ConstraintClauses) ? null
                : new ConstraintClauseList(
                    ConstraintClauses.Select(clause => new ConstraintClauseListAssociation(new DevOps.Primitives.CSharp.ConstraintClause(clause.Name, new ConstraintList(clause.Constraints.Select(constraint => new ConstraintListAssociation(new Constraint(constraint))).ToList())))).ToList());

        protected TypeParameterList GetTypeParameterList()
            => Any(TypeParameters)
                ? TypeParameterLists.Create(TypeParameters.ToArray())
                : null;

        protected UsingDirectiveList GetUsingDirectiveList()
            => Any(UsingDirectives) || Any(UsingStaticDirectives)
                ? UsingDirectiveLists.Create(
                    (UsingDirectives?.Select(each => DevOps.Primitives.CSharp.Helpers.Common.UsingDirectives.Using(each)) ?? new UsingDirective[] { })
                    .Concat((UsingStaticDirectives?.Select(each => DevOps.Primitives.CSharp.Helpers.Common.UsingDirectives.UsingStatic(each)) ?? new UsingDirective[] { })).ToArray())
                : null;
    }

    public class CSharpInterface : CSharpType
    {
        public CSharpInterface() { }
        public CSharpInterface(string @namespace, string typeName, string description, string version, List<EnvironmentVariable> environmentVariables, List<PackageReference> externalDependencies, params string[] sameAccountDependencies) : base(@namespace, typeName, description, version, environmentVariables, externalDependencies, sameAccountDependencies)
        {
        }

        public override Func<IDictionary<string, ITemplate>, TemplateContent> GetContent()
            => repositories
                => new TemplateContent(Interface(
                    Name, Description, Version, GetPackages(repositories), TypeName, GetEnvironmentVariables(), GetUsingDirectiveList(), Comments.Summary(Description), GetAttributeListCollection(), GetTypeParameterList(), GetConstraintClauseList(), null, null, null)); // TODO null => value
    }

    public class NuGetMetapackage : RepositoryDeclaration
    {
        public NuGetMetapackage() { }
        public NuGetMetapackage(string name, string description, string version, List<EnvironmentVariable> environmentVariables, List<PackageReference> externalDependencies, params string[] sameAccountDependencies) : base(name, description, version, environmentVariables, externalDependencies, sameAccountDependencies)
        {
        }

        public override Func<IDictionary<string, ITemplate>, TemplateContent> GetContent()
            => repositories
                => new TemplateContent(Metapackage(
                    Name, Description, Version, GetPackages(repositories), GetEnvironmentVariables()));
    }
}
