using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp.Helpers.Common;
using Humanizer;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    public static class EntityDbContextBuilder
    {
        private const char dot = '.';

        public static Class Build(string entityNamespace, string entityTypeName, string version, string tableName, bool @static, string dependsOn)
        {
            var typeName = $"{entityTypeName}DbContext";
            return new Class(GetDbContextName(entityNamespace), typeName, $"EntityFrameworkCore database context for {entityTypeName} entities", version, sameAccountDependencies: GetSameAccountDependencies(entityNamespace, dependsOn), bases: GetBases(dependsOn), constructors: GetConstructors(typeName), methods: GetMethods(entityTypeName, @static), properties: GetProperties(entityTypeName, tableName), usingDirectives: GetUsings(dependsOn));
        }

        private static List<Base> GetBases(string dependsOn)
            => new List<Base>
            {
                new Base(string.IsNullOrEmpty(dependsOn) ? "DbContext" : $"{dependsOn.Split(dot).Last().Singularize()}DbContext")
            };

        private static List<Constructor> GetConstructors(string typeName)
            => new List<Constructor>
            {
                new Constructor
                {
                    Comment = $"Constructs {typeName} EntityFrameworkCore database context using given options",
                    ConstructorBaseInitializer = new ConstructorBaseInitializer
                    {
                        Arguments = new List<string> { "options" }
                    },
                    Modifiers = "public",
                    Parameters = new List<Parameter>
                    {
                        new Parameter("options", "DbContextOptions")
                    }
                }
            };

        private static string GetDbContextName(string entityNamespace)
            => $"{entityNamespace}.DatabaseContext";

        private static List<Method> GetMethods(string entityTypeName, bool @static)
            => !@static ? null : new List<Method>
            {
                new Method
                {
                    Block = new List<string>
                    {
                        "base.OnModelCreating(modelBuilder);",
                        $"modelBuilder.Entity<{entityTypeName}>().HasIndex(new {entityTypeName}().GetUniqueIndex()).IsUnique();"
                    },
                    Comment = "Configures EntityFramework database creation - adds unique index to model",
                    Modifiers = "protected override",
                    Name = "OnModelCreating",
                    Parameters = new List<Parameter>
                    {
                        new Parameter("modelBuilder", "ModelBuilder")
                    },
                    Type = TypeConstants.Void
                }
            };

        private static List<Property> GetProperties(string entityTypeName, string propertyName)
            => new List<Property>
            {
                new Property(propertyName, $"DbSet<{entityTypeName}>", $"Contains set of {entityTypeName} entities", "public")
            };

        private static IEnumerable<string> GetSameAccountDependencies(string entityNamespace, string dependsOn)
        {
            yield return entityNamespace;
            if (string.IsNullOrEmpty(dependsOn))
                yield return EntityNamespaceConstants.DevOpsCodeEntitiesMetapackagesEntityFrameworkCore;
            else
                yield return GetDbContextName(dependsOn);
        }

        private static List<string> GetUsings(string dependsOn)
        {
            var usings = new List<string>
            {
                EntityNamespaceConstants.MicrosoftEntityFrameworkCore
            };
            if (!string.IsNullOrEmpty(dependsOn))
            {
                usings.Add(GetDbContextName(dependsOn));
            }
            return usings;
        }
    }
}
