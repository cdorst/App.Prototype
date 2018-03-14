using DevOps.Primitives.CSharp;
using DevOps.Primitives.CSharp.Helpers.EntityFramework;
using DevOps.Primitives.NuGet;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders;
using System.Collections.Generic;

namespace ConsoleApp6.Declarations
{
    public static class ProjectExtensions
    {
        public static Project Class(this Project project, string projectName, string version, string typeName, string description, List<NuGetReference> dependencies, IDictionary<string, string> environmentVariables = null, bool @static = true, UsingDirectiveList usingDirectiveList = null, DocumentationCommentList documentationCommentList = null, AttributeListCollection attributeListCollection = null, TypeParameterList typeParameterList = null, ConstraintClauseList constraintClauseList = null, BaseList baseList = null, ConstructorList constructorList = null, FieldList fieldList = null, MethodList methodList = null, PropertyList propertyList = null, Finalizer finalizer = null)
            => project.WithClass(new Class(projectName, description, version, dependencies, typeName, environmentVariables, @static, usingDirectiveList, documentationCommentList, attributeListCollection, typeParameterList, constraintClauseList, baseList, constructorList, fieldList, methodList, propertyList, finalizer));

        public static Project Entity(this Project project, string projectName, string version, string entityName, List<NuGetReference> dependencies, List<EntityProperty> properties, KeyType keyType = KeyType.Int, int? typeId = null, bool editable = false)
            => project.WithEntity(EntityBuilder.Entity(projectName, entityName, version, dependencies, properties, keyType, typeId, editable));

        public static Project Interface(this Project project, string projectName, string version, string typeName, string description, List<NuGetReference> dependencies, IDictionary<string, string> environmentVariables = null, UsingDirectiveList usingDirectiveList = null, DocumentationCommentList documentationCommentList = null, AttributeListCollection attributeListCollection = null, TypeParameterList typeParameterList = null, ConstraintClauseList constraintClauseList = null, BaseList baseList = null, MethodList methodList = null, PropertyList propertyList = null)
            => project.WithInterface(new Interface(projectName, description, version, dependencies, typeName, environmentVariables, usingDirectiveList, documentationCommentList, attributeListCollection, typeParameterList, constraintClauseList, baseList, methodList, propertyList));

        public static Project Metapackage(this Project project, string projectName, string version, string description, List<NuGetReference> dependencies, IDictionary<string, string> environmentVariables = null)
            => project.WithMetapackage(MetapackageBuilder.Metapackage(projectName, description, version, dependencies, environmentVariables));
    }
}
