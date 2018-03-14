using DevOps.Primitives.CSharp;
using DevOps.Primitives.NuGet;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated;
using System.Collections.Generic;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.CodeBuilder;

namespace ConsoleApp6.Declarations
{
    public class Class
    {
        public Class() { }
        public Class(string projectName, string description, string version, List<NuGetReference> dependencies, string typeName, IDictionary<string, string> environmentVariables = null, bool @static = true, UsingDirectiveList usingDirectiveList = null, DocumentationCommentList documentationCommentList = null, AttributeListCollection attributeListCollection = null, TypeParameterList typeParameterList = null, ConstraintClauseList constraintClauseList = null, BaseList baseList = null, ConstructorList constructorList = null, FieldList fieldList = null, MethodList methodList = null, PropertyList propertyList = null, Finalizer finalizer = null)
        {
            ProjectName = projectName;
            Description = description;
            Version = version;
            Dependencies = dependencies;
            EnvironmentVariables = environmentVariables;
            TypeName = typeName;
            Static = @static;
            UsingDirectiveList = usingDirectiveList;
            DocumentationCommentList = documentationCommentList;
            AttributeListCollection = attributeListCollection;
            TypeParameterList = typeParameterList;
            ConstraintClauseList = constraintClauseList;
            BaseList = baseList;
            ConstructorList = constructorList;
            FieldList = fieldList;
            MethodList = methodList;
            PropertyList = propertyList;
            Finalizer = finalizer;
        }

        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public List<NuGetReference> Dependencies { get; set; }
        public IDictionary<string, string> EnvironmentVariables { get; set; }
        public string TypeName { get; set; }
        public bool Static { get; set; }
        public UsingDirectiveList UsingDirectiveList { get; set; }
        public DocumentationCommentList DocumentationCommentList { get; set; }
        public AttributeListCollection AttributeListCollection { get; set; }
        public TypeParameterList TypeParameterList { get; set; }
        public ConstraintClauseList ConstraintClauseList { get; set; }
        public BaseList BaseList { get; set; }
        public ConstructorList ConstructorList { get; set; }
        public FieldList FieldList { get; set; }
        public MethodList MethodList { get; set; }
        public PropertyList PropertyList { get; set; }
        public Finalizer Finalizer { get; set; }

        public Code GetCode()
            => Class(
                ProjectName,
                Description,
                Version,
                Dependencies,
                TypeName,
                EnvironmentVariables,
                Static,
                UsingDirectiveList,
                DocumentationCommentList,
                AttributeListCollection,
                TypeParameterList,
                ConstraintClauseList,
                BaseList,
                ConstructorList,
                FieldList,
                MethodList,
                PropertyList,
                Finalizer);
    }
}
