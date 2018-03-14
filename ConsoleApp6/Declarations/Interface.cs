using DevOps.Primitives.CSharp;
using DevOps.Primitives.NuGet;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated;
using System.Collections.Generic;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.CodeBuilder;

namespace ConsoleApp6.Declarations
{
    public class Interface
    {
        public Interface() { }
        public Interface(string projectName, string description, string version, List<NuGetReference> dependencies, string typeName, IDictionary<string, string> environmentVariables = null, UsingDirectiveList usingDirectiveList = null, DocumentationCommentList documentationCommentList = null, AttributeListCollection attributeListCollection = null, TypeParameterList typeParameterList = null, ConstraintClauseList constraintClauseList = null, BaseList baseList = null, MethodList methodList = null, PropertyList propertyList = null)
        {
            ProjectName = projectName;
            Description = description;
            Version = version;
            Dependencies = dependencies;
            EnvironmentVariables = environmentVariables;
            TypeName = typeName;
            UsingDirectiveList = usingDirectiveList;
            DocumentationCommentList = documentationCommentList;
            AttributeListCollection = attributeListCollection;
            TypeParameterList = typeParameterList;
            ConstraintClauseList = constraintClauseList;
            BaseList = baseList;
            MethodList = methodList;
            PropertyList = propertyList;
        }

        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public List<NuGetReference> Dependencies { get; set; }
        public IDictionary<string, string> EnvironmentVariables { get; set; }
        public string TypeName { get; set; }
        public UsingDirectiveList UsingDirectiveList { get; set; }
        public DocumentationCommentList DocumentationCommentList { get; set; }
        public AttributeListCollection AttributeListCollection { get; set; }
        public TypeParameterList TypeParameterList { get; set; }
        public ConstraintClauseList ConstraintClauseList { get; set; }
        public BaseList BaseList { get; set; }
        public MethodList MethodList { get; set; }
        public PropertyList PropertyList { get; set; }

        public Code GetCode()
            => Interface(
                ProjectName,
                Description,
                Version,
                Dependencies,
                TypeName,
                EnvironmentVariables,
                UsingDirectiveList,
                DocumentationCommentList,
                AttributeListCollection,
                TypeParameterList,
                ConstraintClauseList,
                BaseList,
                MethodList,
                PropertyList);
    }
}
