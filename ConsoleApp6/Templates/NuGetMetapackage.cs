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
        public List<CSharpTypeMembers.Base> Bases { get; set; }
        public List<CSharpTypeMembers.ConstraintClause> ConstraintClauses { get; set; }
        public List<CSharpTypeMembers.Method> Methods { get; set; }
        public List<CSharpTypeMembers.Property> Properties { get; set; }
        public string TypeName { get; set; }
        public List<string> TypeParameters { get; set; }
        public List<string> UsingDirectives { get; set; }
        public List<string> UsingStaticDirectives { get; set; }

        protected AttributeListCollection GetAttributeListCollection()
            => !Any(Attributes) ? null
                : AttributeLists.Create(Attributes.Select(attr => new DevOps.Primitives.CSharp.Attribute(attr.Name, attr.ArgumentListExpression)).ToArray());

        protected BaseList GetBaseList()
            => !Any(Bases) ? null
                : BaseLists.Create(
                    Bases.Select(b => new BaseType(b.Name,
                        Any(b.TypeArguments) ? new TypeArgumentList(
                            b.TypeArguments.Select(typeArgument => new TypeArgumentListAssociation(typeArgument)).ToList()) : null)).ToArray());

        protected ConstraintClauseList GetConstraintClauseList()
            => !Any(ConstraintClauses) ? null
                : new ConstraintClauseList(
                    ConstraintClauses.Select(clause => new ConstraintClauseListAssociation(new DevOps.Primitives.CSharp.ConstraintClause(clause.Name, new ConstraintList(clause.Constraints.Select(constraint => new ConstraintListAssociation(new Constraint(constraint))).ToList())))).ToList());

        protected MethodList GetMethodList()
            => Any(Methods)
                ? MethodLists.Create(Methods.Select(method => string.IsNullOrWhiteSpace(method.ArrowClauseExpression) ? new DevOps.Primitives.CSharp.Method(method.Name, method.Type,
                    GetMethodParameterList(method), !Any(method.Block) ? Blocks.Empty : Blocks.Create(method.Block.ToArray()), GetMethodModifierList(method), Comments.Summary(method.Comment), GetMethodAttributeListCollection(method)) : new DevOps.Primitives.CSharp.Method(method.Name, method.Type, method.ArrowClauseExpression,
                    GetMethodParameterList(method), GetMethodModifierList(method), Comments.Summary(method.Comment), GetMethodAttributeListCollection(method))).ToArray())
                : null;

        protected AttributeListCollection GetMethodAttributeListCollection(CSharpTypeMembers.Method method)
            => !Any(method.Attributes) ? null
                : AttributeLists.Create(method.Attributes.Select(attr => new DevOps.Primitives.CSharp.Attribute(attr.Name, attr.ArgumentListExpression)).ToArray());

        //protected PropertyList GetPropertyList()
        //    => Any(Properties)
        //        ? PropertyLists.Create(Properties.Select(property => new DevOps.Primitives.CSharp.Property(property.Name, property.Type, )).ToArray())
        //        : null;

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

        private static ModifierList GetMethodModifierList(CSharpTypeMembers.Method method)
            => string.IsNullOrWhiteSpace(method.Modifiers)
                ? ModifierLists.Public
                : GetMethodModifierList(method.Modifiers);

        private static ModifierList GetMethodModifierList(string modifiers)
        {
            var tokens = modifiers.Split(' ').Select(each => $"{each.ToUpper().First()}{each.Substring(1)}"); // "public static" => ["Public", "Static"]
            var memberName = string.Join(string.Empty, tokens); // => PublicStatic
            switch (memberName)
            {
                case nameof(ModifierLists.Internal):
                    return ModifierLists.Internal;
                case nameof(ModifierLists.ProtectedInternalSealedOverride):
                    return ModifierLists.ProtectedInternalSealedOverride;
                case nameof(ModifierLists.ProtectedInternalSealed):
                    return ModifierLists.ProtectedInternalSealed;
                case nameof(ModifierLists.ProtectedInternalReadonly):
                    return ModifierLists.ProtectedInternalReadonly;
                case nameof(ModifierLists.ProtectedInternalOverrideAsync):
                    return ModifierLists.ProtectedInternalOverrideAsync;
                case nameof(ModifierLists.ProtectedInternalOverride):
                    return ModifierLists.ProtectedInternalOverride;
                case nameof(ModifierLists.ProtectedInternalEvent):
                    return ModifierLists.ProtectedInternalEvent;
                case nameof(ModifierLists.ProtectedInternalDelegate):
                    return ModifierLists.ProtectedInternalDelegate;
                case nameof(ModifierLists.ProtectedInternalConst):
                    return ModifierLists.ProtectedInternalConst;
                case nameof(ModifierLists.ProtectedInternalAsync):
                    return ModifierLists.ProtectedInternalAsync;
                case nameof(ModifierLists.ProtectedInternalAbstractOverrideAsync):
                    return ModifierLists.ProtectedInternalAbstractOverrideAsync;
                case nameof(ModifierLists.ProtectedInternalAbstractOverride):
                    return ModifierLists.ProtectedInternalAbstractOverride;
                case nameof(ModifierLists.ProtectedInternalAbstractAsync):
                    return ModifierLists.ProtectedInternalAbstractAsync;
                case nameof(ModifierLists.ProtectedInternalAbstract):
                    return ModifierLists.ProtectedInternalAbstract;
                case nameof(ModifierLists.ProtectedInternal):
                    return ModifierLists.ProtectedInternal;
                case nameof(ModifierLists.ProtectedVolatile):
                    return ModifierLists.ProtectedVolatile;
                case nameof(ModifierLists.ProtectedVirtualAsync):
                    return ModifierLists.ProtectedVirtualAsync;
                case nameof(ModifierLists.ProtectedVirtual):
                    return ModifierLists.ProtectedVirtual;
                case nameof(ModifierLists.ProtectedUnsafeStatic):
                    return ModifierLists.ProtectedUnsafeStatic;
                case nameof(ModifierLists.ProtectedStaticReadonly):
                    return ModifierLists.ProtectedStaticReadonly;
                case nameof(ModifierLists.ProtectedStaticExtern):
                    return ModifierLists.ProtectedStaticExtern;
                case nameof(ModifierLists.ProtectedStaticAsync):
                    return ModifierLists.ProtectedStaticAsync;
                case nameof(ModifierLists.ProtectedStatic):
                    return ModifierLists.ProtectedStatic;
                case nameof(ModifierLists.ProtectedSealedOverride):
                    return ModifierLists.ProtectedSealedOverride;
                case nameof(ModifierLists.ProtectedInternalStatic):
                    return ModifierLists.ProtectedInternalStatic;
                case nameof(ModifierLists.ProtectedInternalStaticAsync):
                    return ModifierLists.ProtectedInternalStaticAsync;
                case nameof(ModifierLists.ProtectedInternalStaticExtern):
                    return ModifierLists.ProtectedInternalStaticExtern;
                case nameof(ModifierLists.ProtectedInternalStaticReadonly):
                    return ModifierLists.ProtectedInternalStaticReadonly;
                case nameof(ModifierLists.PublicVirtual):
                    return ModifierLists.PublicVirtual;
                case nameof(ModifierLists.PublicUnsafeStatic):
                    return ModifierLists.PublicUnsafeStatic;
                case nameof(ModifierLists.PublicStaticReadonly):
                    return ModifierLists.PublicStaticReadonly;
                case nameof(ModifierLists.PublicStaticExtern):
                    return ModifierLists.PublicStaticExtern;
                case nameof(ModifierLists.PublicStaticAsync):
                    return ModifierLists.PublicStaticAsync;
                case nameof(ModifierLists.PublicStatic):
                    return ModifierLists.PublicStatic;
                case nameof(ModifierLists.PublicSealedOverride):
                    return ModifierLists.PublicSealedOverride;
                case nameof(ModifierLists.PublicSealed):
                    return ModifierLists.PublicSealed;
                case nameof(ModifierLists.PublicReadonly):
                    return ModifierLists.PublicReadonly;
                case nameof(ModifierLists.PublicOverrideAsync):
                    return ModifierLists.PublicOverrideAsync;
                case nameof(ModifierLists.PublicOverride):
                    return ModifierLists.PublicOverride;
                case nameof(ModifierLists.ProtectedSealed):
                    return ModifierLists.ProtectedSealed;
                case nameof(ModifierLists.PublicEvent):
                    return ModifierLists.PublicEvent;
                case nameof(ModifierLists.PublicConst):
                    return ModifierLists.PublicConst;
                case nameof(ModifierLists.PublicAsync):
                    return ModifierLists.PublicAsync;
                case nameof(ModifierLists.PublicAbstractOverrideAsync):
                    return ModifierLists.PublicAbstractOverrideAsync;
                case nameof(ModifierLists.PublicAbstractOverride):
                    return ModifierLists.PublicAbstractOverride;
                case nameof(ModifierLists.PublicAbstractAsync):
                    return ModifierLists.PublicAbstractAsync;
                case nameof(ModifierLists.PublicAbstract):
                    return ModifierLists.PublicAbstract;
                case nameof(ModifierLists.Public):
                    return ModifierLists.Public;
                case nameof(ModifierLists.ProtectedInternalVolatile):
                    return ModifierLists.ProtectedInternalVolatile;
                case nameof(ModifierLists.ProtectedInternalVirtualAsync):
                    return ModifierLists.ProtectedInternalVirtualAsync;
                case nameof(ModifierLists.ProtectedInternalVirtual):
                    return ModifierLists.ProtectedInternalVirtual;
                case nameof(ModifierLists.ProtectedInternalUnsafeStatic):
                    return ModifierLists.ProtectedInternalUnsafeStatic;
                case nameof(ModifierLists.PublicDelegate):
                    return ModifierLists.PublicDelegate;
                case nameof(ModifierLists.ProtectedReadonly):
                    return ModifierLists.ProtectedReadonly;
                case nameof(ModifierLists.ProtectedOverrideAsync):
                    return ModifierLists.ProtectedOverrideAsync;
                case nameof(ModifierLists.ProtectedOverride):
                    return ModifierLists.ProtectedOverride;
                case nameof(ModifierLists.PrivateAbstractAsync):
                    return ModifierLists.PrivateAbstractAsync;
                case nameof(ModifierLists.PrivateAbstract):
                    return ModifierLists.PrivateAbstract;
                case nameof(ModifierLists.Private):
                    return ModifierLists.Private;
                case nameof(ModifierLists.InternalVolatile):
                    return ModifierLists.InternalVolatile;
                case nameof(ModifierLists.InternalVirtualAsync):
                    return ModifierLists.InternalVirtualAsync;
                case nameof(ModifierLists.InternalVirtual):
                    return ModifierLists.InternalVirtual;
                case nameof(ModifierLists.InternalUnsafeStatic):
                    return ModifierLists.InternalUnsafeStatic;
                case nameof(ModifierLists.InternalStaticReadonly):
                    return ModifierLists.InternalStaticReadonly;
                case nameof(ModifierLists.InternalStaticExtern):
                    return ModifierLists.InternalStaticExtern;
                case nameof(ModifierLists.InternalStaticAsync):
                    return ModifierLists.InternalStaticAsync;
                case nameof(ModifierLists.InternalStatic):
                    return ModifierLists.InternalStatic;
                case nameof(ModifierLists.PrivateAbstractOverride):
                    return ModifierLists.PrivateAbstractOverride;
                case nameof(ModifierLists.InternalSealedOverride):
                    return ModifierLists.InternalSealedOverride;
                case nameof(ModifierLists.InternalReadonly):
                    return ModifierLists.InternalReadonly;
                case nameof(ModifierLists.InternalOverrideAsync):
                    return ModifierLists.InternalOverrideAsync;
                case nameof(ModifierLists.InternalOverride):
                    return ModifierLists.InternalOverride;
                case nameof(ModifierLists.InternalEvent):
                    return ModifierLists.InternalEvent;
                case nameof(ModifierLists.InternalDelegate):
                    return ModifierLists.InternalDelegate;
                case nameof(ModifierLists.InternalConst):
                    return ModifierLists.InternalConst;
                case nameof(ModifierLists.InternalAsync):
                    return ModifierLists.InternalAsync;
                case nameof(ModifierLists.InternalAbstractOverrideAsync):
                    return ModifierLists.InternalAbstractOverrideAsync;
                case nameof(ModifierLists.InternalAbstractOverride):
                    return ModifierLists.InternalAbstractOverride;
                case nameof(ModifierLists.InternalAbstractAsync):
                    return ModifierLists.InternalAbstractAsync;
                case nameof(ModifierLists.InternalAbstract):
                    return ModifierLists.InternalAbstract;
                case nameof(ModifierLists.InternalSealed):
                    return ModifierLists.InternalSealed;
                case nameof(ModifierLists.PublicVirtualAsync):
                    return ModifierLists.PublicVirtualAsync;
                case nameof(ModifierLists.PrivateAbstractOverrideAsync):
                    return ModifierLists.PrivateAbstractOverrideAsync;
                case nameof(ModifierLists.PrivateConst):
                    return ModifierLists.PrivateConst;
                case nameof(ModifierLists.ProtectedEvent):
                    return ModifierLists.ProtectedEvent;
                case nameof(ModifierLists.ProtectedDelegate):
                    return ModifierLists.ProtectedDelegate;
                case nameof(ModifierLists.ProtectedConst):
                    return ModifierLists.ProtectedConst;
                case nameof(ModifierLists.ProtectedAsync):
                    return ModifierLists.ProtectedAsync;
                case nameof(ModifierLists.ProtectedAbstractOverrideAsync):
                    return ModifierLists.ProtectedAbstractOverrideAsync;
                case nameof(ModifierLists.ProtectedAbstractOverride):
                    return ModifierLists.ProtectedAbstractOverride;
                case nameof(ModifierLists.ProtectedAbstractAsync):
                    return ModifierLists.ProtectedAbstractAsync;
                case nameof(ModifierLists.ProtectedAbstract):
                    return ModifierLists.ProtectedAbstract;
                case nameof(ModifierLists.Protected):
                    return ModifierLists.Protected;
                case nameof(ModifierLists.PrivateVolatile):
                    return ModifierLists.PrivateVolatile;
                case nameof(ModifierLists.PrivateVirtualAsync):
                    return ModifierLists.PrivateVirtualAsync;
                case nameof(ModifierLists.PrivateAsync):
                    return ModifierLists.PrivateAsync;
                case nameof(ModifierLists.PrivateVirtual):
                    return ModifierLists.PrivateVirtual;
                case nameof(ModifierLists.PrivateStaticReadonly):
                    return ModifierLists.PrivateStaticReadonly;
                case nameof(ModifierLists.PrivateStaticExtern):
                    return ModifierLists.PrivateStaticExtern;
                case nameof(ModifierLists.PrivateStaticAsync):
                    return ModifierLists.PrivateStaticAsync;
                case nameof(ModifierLists.PrivateStatic):
                    return ModifierLists.PrivateStatic;
                case nameof(ModifierLists.PrivateSealedOverride):
                    return ModifierLists.PrivateSealedOverride;
                case nameof(ModifierLists.PrivateSealed):
                    return ModifierLists.PrivateSealed;
                case nameof(ModifierLists.PrivateReadonly):
                    return ModifierLists.PrivateReadonly;
                case nameof(ModifierLists.PrivateOverrideAsync):
                    return ModifierLists.PrivateOverrideAsync;
                case nameof(ModifierLists.PrivateOverride):
                    return ModifierLists.PrivateOverride;
                case nameof(ModifierLists.PrivateEvent):
                    return ModifierLists.PrivateEvent;
                case nameof(ModifierLists.PrivateDelegate):
                    return ModifierLists.PrivateDelegate;
                case nameof(ModifierLists.PrivateUnsafeStatic):
                    return ModifierLists.PrivateUnsafeStatic;
                case nameof(ModifierLists.PublicVolatile):
                    return ModifierLists.PublicVolatile;
                default:
                    return ModifierLists.Public;
            }
        }

        private static ParameterList GetMethodParameterList(CSharpTypeMembers.Method method)
            => Any(method.Parameters)
                ? ParameterLists.Create(method.Parameters.Select(parameter =>
                    new DevOps.Primitives.CSharp.Parameter(parameter.Name, parameter.Type, string.IsNullOrWhiteSpace(parameter.DefaultValue) ? null : new Expression(parameter.DefaultValue), Any(parameter.Attributes) ? AttributeLists.Create(parameter.Attributes.Select(attribute => new DevOps.Primitives.CSharp.Attribute(attribute.Name, attribute.ArgumentListExpression)).ToArray()) : null)).ToArray())
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
                    Name, Description, Version, GetPackages(repositories), TypeName, GetEnvironmentVariables(), GetUsingDirectiveList(), Comments.Summary(Description), GetAttributeListCollection(), GetTypeParameterList(), GetConstraintClauseList(), GetBaseList(), GetMethodList(), null/*GetPropertyList()*/)); // TODO null => value
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
