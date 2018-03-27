using ConsoleApp6.Templates.CodeGenDeclarations;
using ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups;
using ConsoleApp6.Templates.CSharpTypeMembers;
using System.Collections.Generic;

namespace ConsoleApp6.Templates.Implementation
{
    public static class CDorstGitHubAccount
    {
        public static List<IRepositoryGroup> GetRepositoryGroups()
            => new List<IRepositoryGroup>
            {

            };

        public static List<ICodeGeneratable> GetRepositories()
            => new List<ICodeGeneratable>
            {
                new Metapackage(
                    "DevOps.Code.Entities.Metapackages.EntityFrameworkCore",
                    "Metapackage for EntityFrameworkCore dependencies",
                    "1.0.0", null,
                    new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "Microsoft.EntityFrameworkCore",
                            Version = "2.1.0-preview1-final"
                        }
                    }),
                new Interface(
                    "DevOps.Code.Entities.Interfaces.Entity",
                    "Common interface for code-generated entity types",
                    "1.0.0", typeParameters: new List<string> { "TKey" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Comment = "Returns the unqiue identifier of the entity type",
                            Name = "GetEntityType",
                            Type = "int"
                        },
                        new Method
                        {
                            Comment = "Returns the unqiue identifier of this entity instance",
                            Name = "GetKey",
                            Type = "TKey"
                        }
                    }),
                new Interface(
                    "DevOps.Code.Entities.Interfaces.StaticEntity",
                    "Common interface for code-generated uneditable entity types",
                    "1.0.0",
                    sameAccountDependencies: new[]
                    {
                        "DevOps.Code.Entities.Interfaces.Entity"
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.Interfaces.Entity",
                        "System",
                        "System.Linq.Expressions"
                    },
                    typeParameters: new List<string> { "TEntity", "TKey" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Comment = "Returns an expression that EntityFrameworkCore uses to set a unique index on this entity type",
                            Name = "GetUniqueIndex",
                            Type = "Expression<Func<TEntity, object>>"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Connection.GetConnectionString",
                    "ConnectionStringGetter",
                    "Function returns the AZURE_STORAGE_CONNECTION_STRING environment variable value",
                    "4.0.5",
                    new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Description = "Connection string to your Azure Storage instance",
                            Name = "AZURE_STORAGE_CONNECTION_STRING"
                        }
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "System.Environment"
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            Comment = "Name of the environment variable containing the Azure Storage connection string",
                            DefaultValue = "\"AZURE_STORAGE_CONNECTION_STRING\"",
                            Modifiers = "private const",
                            Name = "EnvironmentVariableName",
                            Type = "string"
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "GetEnvironmentVariable(EnvironmentVariableName)",
                            Comment = "Returns the AZURE_STORAGE_CONNECTION_STRING environment variable value",
                            Modifiers = "public static",
                            Name = "ConnectionString",
                            Type = "string"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Connection.GetCloudStorageAccount",
                    "CloudStorageAccountGetter",
                    "Function returns an instance of Microsoft Azure CloudStorageAccount using the given connection string",
                    "4.0.5",
                    externalDependencies: new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "WindowsAzure.Storage",
                            Version = "9.1.0"
                        }
                    },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "CloudStorageAccount.Parse(connectionString)",
                            Comment = "Returns an instance of Microsoft Azure CloudStorageAccount using the given connection string",
                            Modifiers = "public static",
                            Name = "StorageAccount",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                }
                            },
                            Type = "CloudStorageAccount"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetTableClient",
                    "TableClientGetter",
                    "Function returns an instance of Microsoft Azure CloudTableClient using the given connection string",
                    "4.0.5",
                    sameAccountDependencies: new[] { "Azure.Storage.Connection.GetCloudStorageAccount" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Connection.GetCloudStorageAccount.CloudStorageAccountGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "StorageAccount(connectionString).CreateCloudTableClient()",
                            Comment = "Returns an instance of Microsoft Azure CloudTableClient using the given connection string",
                            Modifiers = "public static",
                            Name = "TableClient",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                }
                            },
                            Type = "CloudTableClient"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetTableReference",
                    "TableReferenceGetter",
                    "Function returns a reference of a Microsoft Azure CloudTable using the given connection string and table name",
                    "4.0.5",
                    sameAccountDependencies: new[] { "Azure.Storage.Table.GetTableClient" },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetTableClient.TableClientGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "TableClient(connectionString).GetTableReference(tableName)",
                            Comment = "Returns a reference of a Microsoft Azure CloudTable using the given connection string and table name",
                            Modifiers = "public static",
                            Name = "TableReference",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "tableName",
                                    Type = "string"
                                }
                            },
                            Type = "CloudTable"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetOrCreateTableReference",
                    "TableReferenceGetterOrCreator",
                    "Function returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name",
                    "4.0.5",
                    sameAccountDependencies: new[] { "Azure.Storage.Table.GetTableReference" },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetTableReference.TableReferenceGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block = new List<string>
                            {
                                "var table = TableReference(connectionString, tableName);",
                                "await table.CreateIfNotExistsAsync();",
                                "return table;"
                            },
                            Comment = "Returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name",
                            Modifiers = "public static async",
                            Name = "GetOrCreateAzureTable",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "connectionString",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "tableName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<CloudTable>"
                        }
                    }),
                new StaticFunction(
                    "Azure.Storage.Table.GetAzureTable",
                    "AzureTableGetter",
                    "Function returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name",
                    "4.0.5",
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Description = "Connection string to your Azure Storage instance",
                            Name = "AZURE_STORAGE_CONNECTION_STRING"
                        }
                    },
                    sameAccountDependencies: new[]
                    {
                        "Azure.Storage.Connection.GetConnectionString",
                        "Azure.Storage.Table.GetOrCreateTableReference"
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Connection.GetConnectionString.ConnectionStringGetter",
                        "Azure.Storage.Table.GetOrCreateTableReference.TableReferenceGetterOrCreator"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "await GetOrCreateAzureTable(ConnectionString(), tableName)",
                            Comment = "Returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name",
                            Modifiers = "public static async",
                            Name = "AzureTable",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "tableName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<CloudTable>"
                        }
                    }),
                new Class(
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger",
                    "AppveyorBuildTable",
                    "Azure Table Storage entity representing a successfully completed AppVeyor build",
                    "4.0.6",
                    externalDependencies: new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "WindowsAzure.Storage",
                            Version = "9.1.0"
                        }
                    },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    bases: new List<Base>
                    {
                        new Base
                        {
                            Name = "TableEntity"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor { Modifiers = "public" },
                        new Constructor
                        {
                            Block = new List<string>
                            {
                                "Dependencies = dependencies;",
                                "FileHashes = fileHashes;",
                                "PartitionKey = name;",
                                "RowKey = version;"
                            },
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    DefaultValue = "null",
                                    Name = "dependencies",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "fileHashes",
                                    Type = "string",
                                    DefaultValue = "null"
                                }
                            }
                        }
                    },
                    properties: new List<Property>
                    {
                        new Property
                        {
                            Comment = "Comma-delimited string of repository dependencies in {name}|{version} format",
                            Name = "Dependencies",
                            Type = "string",
                            Modifiers = "public"
                        },
                        new Property
                        {
                            Comment = "Comma-delimited string of repository file hashes",
                            Name = "FileHashes",
                            Type = "string",
                            Modifiers = "public"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder",
                    "AppveyorBuildTableHelper",
                    "Function returns an instance of AppveyorBuildTable",
                    "4.0.6",
                    sameAccountDependencies: new[] { "DevOps.Build.AppVeyor.AzureStorageTableLedger" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "new AppveyorBuildTable(name, version, dependencies, fileHashes)",
                            Comment = "Returns an instance of AppveyorBuildTable",
                            Modifiers = "public static",
                            Name = "BuildTableEntry",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "dependencies",
                                    Type = "string",
                                    DefaultValue = "null"
                                },
                                new Parameter
                                {
                                    Name = "fileHashes",
                                    Type = "string",
                                    DefaultValue = "null"
                                }
                            },
                            Type = "AppveyorBuildTable",
                        }
                    }),
                new Class(
                    "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger",
                    "RepositoryVersionTable",
                    "Azure Table Storage entity representing a current repository version",
                    "1.0.6",
                    externalDependencies: new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "WindowsAzure.Storage",
                            Version = "9.1.0"
                        }
                    },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    bases: new List<Base>
                    {
                        new Base
                        {
                            Name = "TableEntity"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor { Modifiers = "public" },
                        new Constructor
                        {
                            Block = new List<string>
                            {
                                "PartitionKey = accountName;",
                                "RowKey = repositoryName;",
                                "Version = version;"
                            },
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            }
                        }
                    },
                    properties: new List<Property>
                    {
                        new Property
                        {
                            Comment = "Current version of repository",
                            Name = "Version",
                            Type = "string",
                            Modifiers = "public"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger.Builder",
                    "RepositoryVersionTableHelper",
                    "Function returns an instance of RepositoryVersionTable",
                    "1.0.6",
                    sameAccountDependencies: new[] { "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "new RepositoryVersionTable(accountName, repositoryName, version)",
                            Comment = "Returns an instance of RepositoryVersionTable",
                            Modifiers = "public static",
                            Name = "RepositoryVersionTableEntry",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            },
                            Type = "RepositoryVersionTable",
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.GetAzureTable",
                    "AzureTableGetter",
                    "Function returns an Azure CloudTable reference for a table named appveyor",
                    "1.0.6",
                    sameAccountDependencies: new[] {
                        "Azure.Storage.Table.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetAzureTable.AzureTableGetter"
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            DefaultValue = "nameof(appveyor)",
                            Modifiers = "private const",
                            Name = "appveyor",
                            Type = "string"
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "await AzureTable(appveyor)",
                            Comment = "Returns an Azure CloudTable reference for a table named \"appveyor\" in the storage account for the connection string stored in the environment",
                            Modifiers = "public static async",
                            Name = "GetTable",
                            Type = "Task<CloudTable>"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AddBuild",
                    "BuildAdder",
                    "Function adds the given repository build information to an Azure Storage Table ledger",
                    "1.0.6",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger.Builder.AppveyorBuildTableHelper"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var entry = BuildTableEntry(name, version, dependencies, fileHashes);",
                                "var operation = TableOperation.InsertOrReplace(entry);",
                                "var table = await GetTable();",
                                "await table.ExecuteAsync(operation);"
                            },
                            Comment = "Adds a table entry to an Azure Table named \"appveyor\"",
                            Modifiers = "public static async",
                            Name = "AddBuildAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "dependencies",
                                    Type = "string",
                                    DefaultValue = "null"
                                },
                                new Parameter
                                {
                                    Name = "fileHashes",
                                    Type = "string",
                                    DefaultValue = "null"
                                }
                            },
                            Type = "Task"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.GetBuildRecord",
                    "BuildRecordGetter",
                    "Function gets the given repository's build record from the Azure Storage Table AppVeyor build ledger",
                    "1.0.6",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger",
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var operation = TableOperation.Retrieve<AppveyorBuildTable>(name, version);",
                                "var table = await GetTable();",
                                "var result = await table.ExecuteAsync(operation);",
                                "if (result?.Result == null) return null;",
                                "return (AppveyorBuildTable)result.Result;"
                            },
                            Comment = "Returns the given repository's dependency string from the Azure Storage Table AppVeyor build ledger",
                            Modifiers = "public static async",
                            Name = "GetBuildRecordAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "name",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            },
                            Type = "Task<AppveyorBuildTable>"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.AddRepositoryVersion",
                    "RepositoryVersionAdder",
                    "Function adds the given repository build information to an Azure Storage Table ledger",
                    "1.0.6",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger.Builder"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter",
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger.Builder.RepositoryVersionTableHelper"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var entry = RepositoryVersionTableEntry(accountName, repositoryName, version);",
                                "var operation = TableOperation.InsertOrReplace(entry);",
                                "var table = await GetTable();",
                                "await table.ExecuteAsync(operation);"
                            },
                            Comment = "Adds a table entry to an Azure Table named \"appveyor\"",
                            Modifiers = "public static async",
                            Name = "AddRepositoryVersionAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "version",
                                    Type = "string"
                                }
                            },
                            Type = "Task"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Build.AppVeyor.GetRepositoryVersionRecord",
                    "RepositoryVersionRecordGetter",
                    "Function gets the given repository's version record from the Azure Storage Table AppVeyor build ledger",
                    "2.0.10",
                    sameAccountDependencies: new[] {
                        "DevOps.Build.AppVeyor.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.AzureStorageTableVersionLedger",
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Build.AppVeyor.GetAzureTable.AzureTableGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var operation = TableOperation.Retrieve<RepositoryVersionTable>(accountName, repositoryName);",
                                "var table = await GetTable();",
                                "var result = await table.ExecuteAsync(operation);",
                                "if (result?.Result == null) return null;",
                                "return (RepositoryVersionTable)result.Result;"
                            },
                            Comment = "Returns the given repository's version record from the Azure Storage Table AppVeyor build ledger",
                            Modifiers = "public static async",
                            Name = "GetRepositoryVersionRecordAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<RepositoryVersionTable>"
                        }
                    }),
                new Class(
                    "DevOps.Code.Entities.EntityTypeLedger",
                    "EntityTypeTable",
                    "Azure Table Storage entity representing an entity-type",
                    "1.0.0",
                    externalDependencies: new List<PackageReference>
                    {
                        new PackageReference
                        {
                            Name = "WindowsAzure.Storage",
                            Version = "9.1.0"
                        }
                    },
                    usingDirectives: new List<string> { "Microsoft.WindowsAzure.Storage.Table" },
                    bases: new List<Base>
                    {
                        new Base
                        {
                            Name = "TableEntity"
                        }
                    },
                    constructors: new List<Constructor>
                    {
                        new Constructor { Modifiers = "public" },
                        new Constructor
                        {
                            Block = new List<string>
                            {
                                "EntityTypeId = entityTypeId;",
                                "PartitionKey = accountName;",
                                "RowKey = repositoryName;"
                            },
                            Modifiers = "public",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "entityTypeId",
                                    Type = "int"
                                }
                            }
                        }
                    },
                    properties: new List<Property>
                    {
                        new Property
                        {
                            Comment = "Entity type unique identifier",
                            Name = "EntityTypeId",
                            Type = "int",
                            Modifiers = "public"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.EntityTypeLedger.Builder",
                    "EntityTypeTableHelper",
                    "Function returns an instance of EntityTypeTable",
                    "1.0.0",
                    sameAccountDependencies: new[] { "DevOps.Code.Entities.EntityTypeLedger" },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "new EntityTypeTable(accountName, repositoryName, entityTypeId)",
                            Comment = "Returns an instance of EntityTypeTable",
                            Modifiers = "public static",
                            Name = "EntityTypeTableEntry",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "entityTypeId",
                                    Type = "int"
                                }
                            },
                            Type = "EntityTypeTable",
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.GetAzureTable",
                    "AzureTableGetter",
                    "Function returns an Azure CloudTable reference for a table named entities",
                    "1.0.0",
                    sameAccountDependencies: new[] {
                        "Azure.Storage.Table.GetAzureTable",
                        "DevOps.Build.AppVeyor.AzureStorageTableLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "Azure.Storage.Table.GetAzureTable.AzureTableGetter"
                    },
                    fields: new List<Field>
                    {
                        new Field
                        {
                            DefaultValue = "nameof(entities)",
                            Modifiers = "private const",
                            Name = "entities",
                            Type = "string"
                        }
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            ArrowClauseExpression = "await AzureTable(entities)",
                            Comment = "Returns an Azure CloudTable reference for a table named \"entities\" in the storage account for the connection string stored in the environment",
                            Modifiers = "public static async",
                            Name = "GetTable",
                            Type = "Task<CloudTable>"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.AddEntityTypeRecord",
                    "EntityTypeRecordAdder",
                    "Function adds the given entity type information to an Azure Storage Table ledger",
                    "1.0.0",
                    sameAccountDependencies: new[] {
                        "DevOps.Code.Entities.GetAzureTable",
                        "DevOps.Code.Entities.EntityTypeLedger.Builder"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.GetAzureTable.AzureTableGetter",
                        "DevOps.Code.Entities.EntityTypeLedger.Builder.EntityTypeTableHelper"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var entry = EntityTypeTableEntry(accountName, repositoryName, entityTypeId);",
                                "var operation = TableOperation.InsertOrReplace(entry);",
                                "var table = await GetTable();",
                                "await table.ExecuteAsync(operation);"
                            },
                            Comment = "Adds a table entry to an Azure Table named \"entities\"",
                            Modifiers = "public static async",
                            Name = "AddEntityTypeAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "entityTypeId",
                                    Type = "int"
                                }
                            },
                            Type = "Task"
                        }
                    }),
                new StaticFunction(
                    "DevOps.Code.Entities.GetEntityTypeRecord",
                    "EntityTypeRecordGetter",
                    "Function gets the given entity-type's ID record from the Azure Storage Table entity-types ledger",
                    "1.0.1",
                    sameAccountDependencies: new[] {
                        "DevOps.Code.Entities.GetAzureTable",
                        "DevOps.Code.Entities.EntityTypeLedger"
                    },
                    environmentVariables: new List<EnvironmentVariable>
                    {
                        new EnvironmentVariable
                        {
                            Name = "AZURE_STORAGE_CONNECTION_STRING",
                            Description = "Connection string to your Azure Storage instance"
                        }
                    },
                    usingDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.EntityTypeLedger",
                        "Microsoft.WindowsAzure.Storage.Table",
                        "System.Threading.Tasks"
                    },
                    usingStaticDirectives: new List<string>
                    {
                        "DevOps.Code.Entities.GetAzureTable.AzureTableGetter"
                    },
                    methods: new List<Method>
                    {
                        new Method
                        {
                            Block =  new List<string>
                            {
                                "var operation = TableOperation.Retrieve<EntityTypeTable>(accountName, repositoryName);",
                                "var table = await GetTable();",
                                "var result = await table.ExecuteAsync(operation);",
                                "if (result?.Result == null) return null;",
                                "return (EntityTypeTable)result.Result;"
                            },
                            Comment = "Returns the given entity-type's ID record from the Azure Storage Table entity-types ledger",
                            Modifiers = "public static async",
                            Name = "GetEntityTypeRecordAsync",
                            Parameters = new List<Parameter>
                            {
                                new Parameter
                                {
                                    Name = "accountName",
                                    Type = "string"
                                },
                                new Parameter
                                {
                                    Name = "repositoryName",
                                    Type = "string"
                                }
                            },
                            Type = "Task<EntityTypeTable>"
                        }
                    })
            };
    }
}
