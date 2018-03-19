using System.Collections.Generic;

namespace ConsoleApp6.Templates.Implementation
{
    public static class CDorst
    {
        public static IEnumerable<RepositoryDeclaration> GetRepositoryDeclarations()
        {
            yield return new NuGetMetapackage(
                "DevOps.Code.EntityModel.Common.Metapackages.EntityFrameworkCore",
                "Metapackage for EntityFrameworkCore dependencies",
                "3.0.0", null,
                new List<CSharpTypeMembers.PackageReference>
                {
                    new CSharpTypeMembers.PackageReference
                    {
                        Name = "Microsoft.EntityFrameworkCore",
                        Version = "2.1.0-preview1-final"
                    }
                });

            yield return new CSharpInterface(
                "DevOps.Code.EntityModel.Common.Interfaces.Entity",
                "IEntity",
                "Common interface for code-generated entity types",
                "3.0.0", typeParameters: new List<string> { "TKey" },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        Comment = "Returns the unqiue identifier of the entity type",
                        Name = "GetEntityType",
                        Type = "int"
                    },
                    new CSharpTypeMembers.Method
                    {
                        Comment = "Returns the unqiue identifier of this entity instance",
                        Name = "GetKey",
                        Type = "TKey"
                    }
                });

            yield return new CSharpInterface(
                "DevOps.Code.EntityModel.Common.Interfaces.StaticEntity",
                "IStaticEntity",
                "Common interface for code-generated uneditable entity types",
                "3.0.0",
                null,
                new[]
                {
                    "DevOps.Code.EntityModel.Common.Interfaces.Entity"
                },
                usingDirectives: new List<string>
                {
                    "DevOps.Code.EntityModel.Common.Interfaces.Entity",
                    "System",
                    "System.Linq.Expressions"
                },
                typeParameters: new List<string> { "TEntity", "TKey" },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        Comment = "Returns an expression that EntityFrameworkCore uses to set a unique index on this entity type",
                        Name = "GetUniqueIndex",
                        Type = "Expression<Func<TEntity, object>>"
                    }
                });

            yield return new CSharpClass(
                "Cloud.Azure.Storage.Connection.Functions.GetConnectionString",
                "ConnectionStringGetter",
                "Function returns the AZURE_STORAGE_CONNECTION_STRING environment variable value",
                "3.0.0", isStatic: true,
                new List<CSharpTypeMembers.EnvironmentVariable>
                {
                    new CSharpTypeMembers.EnvironmentVariable
                    {
                        Description = "Connection string to your Azure Storage instance",
                        Name = "AZURE_STORAGE_CONNECTION_STRING"
                    }
                },
                usingStaticDirectives: new List<string>
                {
                    "System.Environment"
                },
                fields: new List<CSharpTypeMembers.Field>
                {
                    new CSharpTypeMembers.Field
                    {
                        Comment = "Name of the environment variable containing the Azure Storage connection string",
                        DefaultValue = "\"AZURE_STORAGE_CONNECTION_STRING\"",
                        Name = "EnvironmentVariableName",
                        Type = "string"
                    }
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        ArrowClauseExpression = "GetEnvironmentVariable(EnvironmentVariableName)",
                        Comment = "Returns the AZURE_STORAGE_CONNECTION_STRING environment variable value",
                        Modifiers = "public static",
                        Name = "ConnectionString",
                        Type = "string"
                    }
                });

            yield return new NuGetMetapackage(
                "Cloud.Azure.Storage.Connection.Metapackages.ConfigurationManager",
                "Metapackage for Azure ConfigurationManager dependencies",
                "3.0.0",
                externalDependencies: new List<CSharpTypeMembers.PackageReference>
                {
                    new CSharpTypeMembers.PackageReference
                    {
                        Name = "Microsoft.WindowsAzure.ConfigurationManager",
                        Version = "3.2.3"
                    }
                });

            yield return new CSharpClass(
                "Cloud.Azure.Storage.Connection.Functions.GetCloudStorageAccount",
                "ConfigurationManagerGetter",
                "Function returns an instance of Microsoft Azure ConfigurationManager using the given connection string",
                "3.0.0",
                sameAccountDependencies: new[] { "Cloud.Azure.Storage.Connection.Metapackages.ConfigurationManager" },
                usingDirectives: new List<string> { "Microsoft.Azure" },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        ArrowClauseExpression = "CloudStorageAccount.Parse(connectionString)",
                        Comment = "Returns an instance of Microsoft Azure ConfigurationManager using the given connection string",
                        Modifiers = "public static",
                        Name = "ConfigurationManager",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "connectionString",
                                Type = "string"
                            }
                        },
                        Type = "CloudStorageAccount"
                    }
                });

            yield return new CSharpClass(
                "Cloud.Azure.Storage.Table.Functions.GetTableClient",
                "TableClientGetter",
                "Function returns an instance of Microsoft Azure CloudTableClient using the given connection string",
                "3.0.0",
                sameAccountDependencies: new[] { "Cloud.Azure.Storage.Connection.Functions.GetCloudStorageAccount" },
                usingDirectives: new List<string> { "Microsoft.Azure" },
                usingStaticDirectives: new List<string>
                {
                    "Cloud.Azure.Storage.Connection.Functions.GetCloudStorageAccount.ConfigurationManagerGetter"
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        ArrowClauseExpression = "ConfigurationManager(connectionString).CreateCloudTableClient()",
                        Comment = "Returns an instance of Microsoft Azure CloudTableClient using the given connection string",
                        Modifiers = "public static",
                        Name = "TableClient",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "connectionString",
                                Type = "string"
                            }
                        },
                        Type = "CloudTableClient"
                    }
                });

            yield return new CSharpClass(
                "Cloud.Azure.Storage.Table.Functions.GetTableReference",
                "TableReferenceGetter",
                "Function returns a reference of a Microsoft Azure CloudTable using the given connection string and table name",
                "3.0.0",
                sameAccountDependencies: new[] { "Cloud.Azure.Storage.Table.Functions.GetTableClient" },
                usingDirectives: new List<string> { "Microsoft.Azure" },
                usingStaticDirectives: new List<string>
                {
                    "Cloud.Azure.Storage.Table.Functions.GetTableClient.TableClientGetter"
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        ArrowClauseExpression = "TableClient(connectionString).GetTableReference(tableName)",
                        Comment = "Returns a reference of a Microsoft Azure CloudTable using the given connection string and table name",
                        Modifiers = "public static",
                        Name = "TableReference",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "connectionString",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "tableName",
                                Type = "string"
                            }
                        },
                        Type = "CloudTable"
                    }
                });

            yield return new CSharpClass(
                "Cloud.Azure.Storage.Table.Functions.GetOrCreateTableReference",
                "TableReferenceGetterOrCreator",
                "Function returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name",
                "3.0.0",
                sameAccountDependencies: new[] { "Cloud.Azure.Storage.Table.Functions.GetTableReference" },
                usingDirectives: new List<string> { "Microsoft.Azure" },
                usingStaticDirectives: new List<string>
                {
                    "Cloud.Azure.Storage.Table.Functions.GetTableReference.TableReferenceGetter"
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        Block = new List<string>
                        {
                            "var table = TableReference(connectionString, tableName);",
                            "table.CreateIfNotExists();",
                            "return table;"
                        },
                        Comment = "Returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name",
                        Modifiers = "public static",
                        Name = "GetOrCreateAzureTable",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "connectionString",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "tableName",
                                Type = "string"
                            }
                        },
                        Type = "CloudTable"
                    }
                });

            yield return new CSharpClass(
                "Cloud.Azure.Storage.Table.Functions.GetAzureTable",
                "AzureTableGetter",
                "Function returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name",
                "3.0.0",
                environmentVariables: new List<CSharpTypeMembers.EnvironmentVariable>
                {
                    new CSharpTypeMembers.EnvironmentVariable
                    {
                        Description = "Connection string to your Azure Storage instance",
                        Name = "AZURE_STORAGE_CONNECTION_STRING"
                    }
                },
                sameAccountDependencies: new[] { "Cloud.Azure.Storage.Table.Functions.GetOrCreateTableReference" },
                usingDirectives: new List<string> { "Microsoft.Azure" },
                usingStaticDirectives: new List<string>
                {
"Cloud.Azure.Storage.Connection.Functions.GetConnectionString.ConnectionStringGetter",
                    "Cloud.Azure.Storage.Table.Functions.GetOrCreateTableReference.TableReferenceGetterOrCreator"
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        ArrowClauseExpression = "GetOrCreateAzureTable(ConnectionString(), tableName)",
                        Comment = "Returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name",
                        Modifiers = "public static",
                        Name = "AzureTable",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "tableName",
                                Type = "string"
                            }
                        },
                        Type = "CloudTable"
                    }
                });

            yield return new CSharpClass(
                "DevOps.Build.AppVeyor.AzureStorageTableLedger",
                "AppveyorBuildTable",
                "Azure Table Storage entity representing a successfully completed AppVeyor build",
                "3.0.0",
                isStatic: false,
                usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                bases: new List<CSharpTypeMembers.Base>
                {
                    new CSharpTypeMembers.Base
                    {
                        Name = "TableEntity"
                    }
                },
                constructors: new List<CSharpTypeMembers.Constructor>
                {
                    new CSharpTypeMembers.Constructor { },
                    new CSharpTypeMembers.Constructor
                    {
                        Block = new List<string>
                        {
                            "Dependencies = dependencies;",
                            "PartitionKey = name;",
                            "RowKey = version;"
                        },
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "name",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "version",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                DefaultValue = "null",
                                Name = "dependencies",
                                Type = "string"
                            }
                        }
                    }
                },
                properties: new List<CSharpTypeMembers.Property>
                {
                    new CSharpTypeMembers.Property
                    {
                        Comment = "Comma-delimited string of repository dependencies in {name}|{version} format",
                        Name = "Dependencies",
                        Type = "string",
                    }
                });

            yield return new CSharpClass(
                "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder",
                "AppveyorBuildTableHelper",
                "Function returns an instance of AppveyorBuildTable",
                "3.0.0",
                sameAccountDependencies: new[] { "DevOps.Build.AppVeyor.AzureStorageTableLedger" },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        ArrowClauseExpression = "new AppveyorBuildTable(name, version, dependencies)",
                        Comment = "Returns an instance of AppveyorBuildTable",
                        Modifiers = "public static",
                        Name = "BuildTableEntry",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "name",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "version",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "dependencies",
                                Type = "string",
                                DefaultValue = "null"
                            }
                        },
                        Type = "AppveyorBuildTable",
                    }
                });

            yield return new CSharpClass(
                "DevOps.Build.AppVeyor.Functions.GetAzureTable",
                "BuildTableGetter",
                "Function returns an Azure CloudTable reference for a table named \"nuget\" in the storage account for the connection string stored in the environment",
                "3.0.0",
                sameAccountDependencies: new[] {
                    "Cloud.Azure.Storage.Table.Functions.GetAzureTable",
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                },
                environmentVariables: new List<CSharpTypeMembers.EnvironmentVariable>
                {
                    new CSharpTypeMembers.EnvironmentVariable
                    {
                        Name = "AZURE_STORAGE_CONNECTION_STRING",
                        Description = "Connection string to your Azure Storage instance"
                    }
                },
                usingDirectives: new List<string>
                {
                    "Microsoft.WindowsAzure.Storage.Table"
                },
                usingStaticDirectives: new List<string>
                {
                    "Cloud.Azure.Storage.Table.Functions.GetAzureTable.AzureTableGetter"
                },
                fields: new List<CSharpTypeMembers.Field>
                {
                    new CSharpTypeMembers.Field
                    {
                        DefaultValue = "nameof(nuget)",
                        Modifiers = "private const",
                        Name = "nuget",
                        Type = "string"
                    }
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        ArrowClauseExpression = "GetOrCreateAzureTable(nuget)",
                        Comment = "Returns an Azure CloudTable reference for a table named \"nuget\" in the storage account for the connection string stored in the environment",
                        Modifiers = "public static",
                        Name = "GetTable",
                        Type = "CloudTable"
                    }
                });

            yield return new CSharpClass(
                "DevOps.Build.AppVeyor.Functions.AddBuild",
                "AppveyorBuildAdder",
                "Function adds the given repository build information to an Azure Storage Table ledger",
                "3.0.0",
                sameAccountDependencies: new[] {
                    "Cloud.Azure.Storage.Table.Functions.GetAzureTable",
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder"
                },
                environmentVariables: new List<CSharpTypeMembers.EnvironmentVariable>
                {
                    new CSharpTypeMembers.EnvironmentVariable
                    {
                        Name = "AZURE_STORAGE_CONNECTION_STRING",
                        Description = "Connection string to your Azure Storage instance"
                    }
                },
                usingStaticDirectives: new List<string>
                {
                    "Cloud.Azure.Storage.Table.Functions.GetAzureTable.BuildTableGetter",
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder.AppveyorBuildTableHelper"
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        Block =  new List<string>
                        {
                            "var entry = BuildTableEntry(name, version, dependencies);",
                            "var operation = TableOperation.InsertOrReplace(entry);",
                            "var table = GetTable();",
                            "await table.ExecuteAsync(operation);"
                        },
                        Comment = "Returns an Azure CloudTable reference for a table named \"nuget\" in the storage account for the connection string stored in the environment",
                        Modifiers = "public static async",
                        Name = "AddBuildAsync",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "name",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "version",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "dependencies",
                                Type = "string",
                                DefaultValue = "null"
                            }
                        },
                        Type = "Task"
                    }
                });

            yield return new CSharpClass(
                "DevOps.Build.AppVeyor.Functions.GetBuildDependencyString",
                "BuildDependencyStringGetter",
                "Function gets the given repository's dependency string from the Azure Storage Table AppVeyor build ledger",
                "3.0.0",
                sameAccountDependencies: new[] {
                    "Cloud.Azure.Storage.Table.Functions.GetAzureTable",
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                },
                environmentVariables: new List<CSharpTypeMembers.EnvironmentVariable>
                {
                    new CSharpTypeMembers.EnvironmentVariable
                    {
                        Name = "AZURE_STORAGE_CONNECTION_STRING",
                        Description = "Connection string to your Azure Storage instance"
                    }
                },
                usingStaticDirectives: new List<string>
                {
                    "Cloud.Azure.Storage.Table.Functions.GetAzureTable.BuildTableGetter"
                },
                methods: new List<CSharpTypeMembers.Method>
                {
                    new CSharpTypeMembers.Method
                    {
                        Block =  new List<string>
                        {
                            "var operation = TableOperation.Retrieve<AppveyorBuildTable>(name, version);",
                            "var table = GetTable();",
                            "var result = await table.ExecuteAsync(operation);",
                            "if (result?.Result == null) return null;",
                            "return ((AppveyorBuildTable)result.Result)).Dependencies;"
                        },
                        Comment = "Returns the given repository's dependency string from the Azure Storage Table AppVeyor build ledger",
                        Modifiers = "public static async",
                        Name = "GetDependencyStringAsync",
                        Parameters = new List<CSharpTypeMembers.Parameter>
                        {
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "name",
                                Type = "string"
                            },
                            new CSharpTypeMembers.Parameter
                            {
                                Name = "version",
                                Type = "string"
                            }
                        },
                        Type = "Task<string>"
                    }
                });
        }
    }
}
