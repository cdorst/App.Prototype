using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp6.Templates.CSharpTypeMembers;

namespace ConsoleApp6.Templates.StaticFunctions
{
    public class Function : CSharpClass
    {
        public Function()
        {
        }

        public Function(string @namespace, string typeName, string description, string version, List<EnvironmentVariable> environmentVariables, List<PackageReference> externalDependencies, IEnumerable<string> sameAccountDependencies, List<CSharpTypeMembers.Attribute> attributes, List<Base> bases, List<ConstraintClause> constraintClauses, List<Method> methods, List<Property> properties, List<string> typeParameters, List<string> usingDirectives, List<string> usingStaticDirectives) : base(@namespace, typeName, description, version, true, environmentVariables, externalDependencies, sameAccountDependencies, attributes, bases, constraintClauses, methods, properties, typeParameters, usingDirectives, usingStaticDirectives)
        {
        }
    }
}
