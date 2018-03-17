using ConsoleApp6.Declarations;
using DevOps.Primitives.CSharp;
using DevOps.Primitives.CSharp.Helpers.Common;
using DevOps.Primitives.CSharp.Helpers.EntityFramework;
using DevOps.Primitives.NuGet;
using System.Collections.Generic;
using static Common.Functions.CreateNewArrayFromSingleElement.ArrayCreator;
using static ConsoleApp6.CDorstProjectNames;
using static ConsoleApp6.Declarations.Helpers.NuGetDependencies;

namespace ConsoleApp6
{
    public static class CDorstProjectExtensions
    {
        private const string UnifiedVersion = "2.0.1";

        public static Project AddRepositories(this Project project)
            => project
                .Cloud_Azure_Storage_Connection_Functions_GetCloudStorageAccount()
                .Cloud_Azure_Storage_Connection_Functions_GetConnectionString()
                .Cloud_Azure_Storage_Connection_Metapackages_ConfigurationManager()
                .Cloud_Azure_Storage_Table_Functions_GetAzureTable()
                .Cloud_Azure_Storage_Table_Functions_GetOrCreateTableReference()
                .Cloud_Azure_Storage_Table_Functions_GetTableClient()
                .Cloud_Azure_Storage_Table_Functions_GetTableReference()
                .DevOps_Build_AppVeyor_AzureTableStorageLedger()
                .DevOps_Build_AppVeyor_AzureTableStorageLedger_Builder()
                .DevOps_Build_AppVeyor_Functions_AddBuild()
                .DevOps_Build_AppVeyor_Functions_GetAzureTable()
                .DevOps_Build_AppVeyor_Functions_GetBuildDependencyString()
                .DevOps_Code_EntityModel_Common_Metapackages_EntityFrameworkCore()
                .DevOps_Code_EntityModel_Common_Interfaces_Entity()
                .DevOps_Code_EntityModel_Common_Interfaces_StaticEntity()
                .DevOps_Code_EntityModel_Strings_Ascii()
                .DevOps_Code_EntityModel_Strings_Unicode()
            ;

        private static Project DevOps_Code_EntityModel_Common_Metapackages_EntityFrameworkCore(this Project project)
            => project.Metapackage(Name(nameof(DevOps_Code_EntityModel_Common_Metapackages_EntityFrameworkCore)), UnifiedVersion,
                "Metapackage for EntityFrameworkCore dependencies",
                Dependencies(NuGet("Microsoft.EntityFrameworkCore", "2.1.0-preview1-final")));

        private static Project DevOps_Code_EntityModel_Common_Interfaces_Entity(this Project project)
            => project.Interface(Name(nameof(DevOps_Code_EntityModel_Common_Interfaces_Entity)), UnifiedVersion,
                "IEntity",
                "Common interface for code-generated entity types", dependencies: null,
                typeParameterList: TypeParameterLists.Create("TKey"),
                methodList: MethodLists.Create(
                    Methods.Declaration("GetEntityType", TypeConstants.Int),
                    Methods.Declaration("GetKey", "TKey")));

        private static Project DevOps_Code_EntityModel_Common_Interfaces_StaticEntity(this Project project)
            => project.Interface(Name(nameof(DevOps_Code_EntityModel_Common_Interfaces_StaticEntity)), UnifiedVersion,
                "IStaticEntity",
                "Common interface for code-generated uneditable entity types",
                Dependencies(Package(nameof(DevOps_Code_EntityModel_Common_Interfaces_Entity))),
                usingDirectiveList: UsingDirectiveLists.Create(
                    "DevOps.Code.EntityModel.Common.Interfaces.Entity",
                    "System",
                    "System.Linq.Expressions"),
                typeParameterList: TypeParameterLists.Create("TEntity", "TKey"),
                baseList: BaseLists.Create(new BaseType("IEntity", TypeArgumentLists.Create("TKey"))),
                methodList: MethodLists.Create(Methods.Declaration("GetUniqueIndex", "Expression<Func<TEntity, object>>")));

        private static Project DevOps_Code_EntityModel_Strings_Ascii(this Project project)
            => project.Entity(Name(nameof(DevOps_Code_EntityModel_Strings_Ascii)), UnifiedVersion,
                "AsciiStringReference",
                Dependencies(Package(nameof(DevOps_Code_EntityModel_Common_Interfaces_StaticEntity))),
                new List<EntityProperty> { EntityProperties.Create("Value", "string", "Contains the string value") },
                KeyType.Long, (int)CDorstKnownTypeIds.DevOps_Code_EntityModel_Strings_Ascii);

        private static Project DevOps_Code_EntityModel_Strings_Unicode(this Project project)
            => project.Entity(Name(nameof(DevOps_Code_EntityModel_Strings_Unicode)), UnifiedVersion,
                "UnicodeStringReference",
                Dependencies(Package(nameof(DevOps_Code_EntityModel_Common_Interfaces_StaticEntity))),
                new List<EntityProperty> { EntityProperties.Create("Value", "string", "Contains the string value") },
                KeyType.Long, (int)CDorstKnownTypeIds.DevOps_Code_EntityModel_Strings_Unicode);

        private static Project Cloud_Azure_Storage_Connection_Functions_GetConnectionString(this Project project)
            => project.Class(Name(nameof(Cloud_Azure_Storage_Connection_Functions_GetConnectionString)), UnifiedVersion,
                "ConnectionStringGetter",
                "Function returns the AZURE_STORAGE_CONNECTION_STRING environment variable value", dependencies: null,
                environmentVariables: new Dictionary<string, string>(Array(new KeyValuePair<string, string>(
                    "AZURE_STORAGE_CONNECTION_STRING", "Connection string to your Azure Storage instance"))),
                @static: true,
                UsingDirectiveLists.Create(UsingDirectives.UsingStatic("System.Environment")),
                Comments.Summary("Function returns the AZURE_STORAGE_CONNECTION_STRING environment variable value"),
                fieldList: FieldLists.Create(Fields.PrivateConst("EmvironmentVariableName", TypeConstants.String, "Name of the environment variable containing the Azure Storage connection string", null, "\"AZURE_STORAGE_CONNECTION_STRING\"")),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("ConnectionString", TypeConstants.String, "GetEnvironmentVariable(EnvironmentVariableName)", documentationCommentList: Comments.Summary("Returns the AZURE_STORAGE_CONNECTION_STRING environment variable value"))));

        private static Project Cloud_Azure_Storage_Connection_Metapackages_ConfigurationManager(this Project project)
            => project.Metapackage(Name(nameof(Cloud_Azure_Storage_Connection_Metapackages_ConfigurationManager)), UnifiedVersion,
                "Metapackage for Azure ConfigurationManager dependencies",
                Dependencies(NuGet("Microsoft.WindowsAzure.ConfigurationManager", "3.2.3")));

        private static Project Cloud_Azure_Storage_Connection_Functions_GetCloudStorageAccount(this Project project)
            => project.Class(Name(nameof(Cloud_Azure_Storage_Connection_Functions_GetCloudStorageAccount)), UnifiedVersion,
                "ConfigurationManagerGetter",
                "Function returns an instance of Microsoft Azure ConfigurationManager using the given connection string",
                Dependencies(Package(nameof(Cloud_Azure_Storage_Connection_Metapackages_ConfigurationManager))),
                environmentVariables: null,
                @static: true,
                UsingDirectiveLists.Create(UsingDirectives.Using("Microsoft.Azure")),
                Comments.Summary("Function returns an instance of Microsoft Azure ConfigurationManager using the given connection string"),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("ConfigurationManager", "CloudStorageAccount", "CloudStorageAccount.Parse(connectionString)", ParameterLists.Create(new Parameter("connectionString", TypeConstants.String)), documentationCommentList: Comments.Summary("Returns an instance of Microsoft Azure ConfigurationManager using the given connection string"))));

        private static Project Cloud_Azure_Storage_Table_Functions_GetTableClient(this Project project)
            => project.Class(Name(nameof(Cloud_Azure_Storage_Table_Functions_GetTableClient)), UnifiedVersion,
                "TableClientGetter",
                "Function returns an instance of Microsoft Azure CloudTableClient using the given connection string",
                Dependencies(Package(nameof(Cloud_Azure_Storage_Connection_Functions_GetCloudStorageAccount))),
                environmentVariables: null,
                @static: true,
                UsingDirectiveLists.Create(
                    UsingDirectives.Using("Microsoft.Azure"),
                    UsingDirectives.UsingStatic($"{Package(nameof(Cloud_Azure_Storage_Connection_Functions_GetCloudStorageAccount))}.ConfigurationManagerGetter")),
                Comments.Summary("Function returns an instance of Microsoft Azure CloudTableClient using the given connection string"),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("TableClient", "CloudTableClient", "ConfigurationManager(connectionString).CreateCloudTableClient()", ParameterLists.Create(new Parameter("connectionString", TypeConstants.String)), documentationCommentList: Comments.Summary("Returns an instance of Microsoft Azure CloudTableClient using the given connection string"))));

        private static Project Cloud_Azure_Storage_Table_Functions_GetTableReference(this Project project)
            => project.Class(Name(nameof(Cloud_Azure_Storage_Table_Functions_GetTableReference)), UnifiedVersion,
                "TableReferenceGetter",
                "Function returns a reference of a Microsoft Azure CloudTable using the given connection string and table name",
                Dependencies(Package(nameof(Cloud_Azure_Storage_Table_Functions_GetTableClient))),
                environmentVariables: null,
                @static: true,
                UsingDirectiveLists.Create(
                    UsingDirectives.Using("Microsoft.Azure"),
                    UsingDirectives.UsingStatic($"{Package(nameof(Cloud_Azure_Storage_Table_Functions_GetTableClient))}.TableClientGetter")),
                Comments.Summary("Function returns a reference of a Microsoft Azure CloudTable using the given connection string and table name"),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("TableReference", "CloudTable", "TableClient(connectionString).GetTableReference(tableName)",
                        ParameterLists.Create(
                            new Parameter("connectionString", TypeConstants.String),
                            new Parameter("tableName", TypeConstants.String)), documentationCommentList: Comments.Summary("Returns a reference of a Microsoft Azure CloudTable using the given connection string and table name"))));

        private static Project Cloud_Azure_Storage_Table_Functions_GetOrCreateTableReference(this Project project)
            => project.Class(Name(nameof(Cloud_Azure_Storage_Table_Functions_GetOrCreateTableReference)), UnifiedVersion,
                "TableReferenceGetterOrCreator",
                "Function returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name",
                Dependencies(Package(nameof(Cloud_Azure_Storage_Table_Functions_GetTableReference))),
                environmentVariables: new Dictionary<string, string>(Array(new KeyValuePair<string, string>(
                    "AZURE_STORAGE_CONNECTION_STRING", "Connection string to your Azure Storage instance"))),
                @static: true,
                UsingDirectiveLists.Create(
                    UsingDirectives.Using("Microsoft.Azure"),
                    UsingDirectives.UsingStatic($"{Package(nameof(Cloud_Azure_Storage_Table_Functions_GetTableReference))}.TableReferenceGetter")),
                Comments.Summary("Function returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name"),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("GetOrCreateAzureTable", "CloudTable",
                        Blocks.Create(
                            "var table = TableReference(connectionString, tableName);",
                            "table.CreateIfNotExists();",
                            "return table;"),
                        ParameterLists.Create(
                            new Parameter("connectionString", TypeConstants.String),
                            new Parameter("tableName", TypeConstants.String)),
                        documentationCommentList: Comments.Summary("Returns a reference of a new or existing Microsoft Azure CloudTable using the given connection string and table name"))));

        private static Project Cloud_Azure_Storage_Table_Functions_GetAzureTable(this Project project)
            => project.Class(Name(nameof(Cloud_Azure_Storage_Table_Functions_GetAzureTable)), UnifiedVersion,
                "AzureTableGetter",
                "Function returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name",
                Dependencies(Package(nameof(Cloud_Azure_Storage_Table_Functions_GetOrCreateTableReference))),
                environmentVariables: new Dictionary<string, string>(Array(new KeyValuePair<string, string>(
                    "AZURE_STORAGE_CONNECTION_STRING", "Connection string to your Azure Storage instance"))),
                @static: true,
                UsingDirectiveLists.Create(
                    UsingDirectives.Using("Microsoft.Azure"),
                    UsingDirectives.UsingStatic($"{Package(nameof(Cloud_Azure_Storage_Connection_Functions_GetConnectionString))}.ConnectionStringGetter"),
                    UsingDirectives.UsingStatic($"{Package(nameof(Cloud_Azure_Storage_Table_Functions_GetOrCreateTableReference))}.TableReferenceGetterOrCreator")),
                Comments.Summary("Function returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name"),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("GetOrCreateAzureTable", "CloudTable", "GetOrCreateAzureTable(ConnectionString(), tableName)",
                        ParameterLists.Create(new Parameter("tableName", TypeConstants.String)),
                        documentationCommentList: Comments.Summary("Returns a reference of a new or existing Microsoft Azure CloudTable using the environment's connection string and given table name"))));

        private static Project DevOps_Build_AppVeyor_AzureTableStorageLedger(this Project project)
            => project.Class(Name(nameof(DevOps_Build_AppVeyor_AzureTableStorageLedger)), UnifiedVersion,
                "AppveyorBuildTable",
                "Azure Table Storage entity representing a successfully completed AppVeyor build",
                Dependencies(Package(nameof(Cloud_Azure_Storage_Connection_Metapackages_ConfigurationManager))),
                environmentVariables: null,
                @static: false,
                UsingDirectiveLists.Create(
                    UsingDirectives.Using("Microsoft.WindowsAzure.Storage.Table")),
                Comments.Summary("Azure Table Storage entity representing a successfully completed AppVeyor build"),
                baseList: BaseLists.Create("TableEntity"),
                constructorList: ConstructorLists.Create(
                    Constructors.Default("AppveyorBuildTable"),
                    new Constructor("AppveyorBuildTable",
                        Blocks.Create(
                            "PartitionKey = name;",
                            "RowKey = version;",
                            "Dependencies = dependencies;"),
                        ModifierLists.Public,
                        ParameterLists.Create(
                            new Parameter("name", TypeConstants.String),
                            new Parameter("version", TypeConstants.String),
                            new Parameter("dependencies", TypeConstants.String, new Expression("null"))))),
                propertyList: PropertyLists.Create(Properties.Public("Dependencies", TypeConstants.String, "Comma-delimited string of repository dependencies in {name}|{version} format")));

        private static Project DevOps_Build_AppVeyor_AzureTableStorageLedger_Builder(this Project project)
            => project.Class(Name(nameof(DevOps_Build_AppVeyor_AzureTableStorageLedger_Builder)), UnifiedVersion,
                "AppveyorBuildTableHelper",
                "Function returns an instance of AppveyorBuildTable",
                Dependencies(Package(nameof(DevOps_Build_AppVeyor_AzureTableStorageLedger))),
                environmentVariables: null,
                @static: true,
                usingDirectiveList: null,
                Comments.Summary("Function returns an instance of AppveyorBuildTable"),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("BuildTableEntry", "AppveyorBuildTable", "new AppveyorBuildTable(name, version, dependencies)",
                        ParameterLists.Create(
                            new Parameter("name", TypeConstants.String),
                            new Parameter("version", TypeConstants.String),
                            new Parameter("dependencies", TypeConstants.String, new Expression("null"))),
                        documentationCommentList: Comments.Summary("Returns an instance of AppveyorBuildTable"))));

        private static Project DevOps_Build_AppVeyor_Functions_GetAzureTable(this Project project)
            => project.Class(Name(nameof(DevOps_Build_AppVeyor_Functions_GetAzureTable)), UnifiedVersion,
                "BuildTableGetter",
                "Function returns an Azure CloudTable reference for a table named \"nuget\" in the storage account for the connection string stored in the environment",
                Dependencies(
                    Package(nameof(Cloud_Azure_Storage_Table_Functions_GetAzureTable)),
                    Package(nameof(DevOps_Build_AppVeyor_AzureTableStorageLedger))),
                environmentVariables: new Dictionary<string, string>(Array(new KeyValuePair<string, string>(
                    "AZURE_STORAGE_CONNECTION_STRING", "Connection string to your Azure Storage instance"))),
                @static: true,
                UsingDirectiveLists.Create(
                    UsingDirectives.Using("Microsoft.WindowsAzure.Storage.Table"),
                    UsingDirectives.UsingStatic($"{Package(nameof(Cloud_Azure_Storage_Table_Functions_GetAzureTable))}.AzureTableGetter")),
                Comments.Summary("Function returns an Azure CloudTable reference for a table named \"nuget\" in the storage account for the connection string stored in the environment"),
                fieldList: FieldLists.Create(Fields.PrivateConst("nuget", TypeConstants.String, initializer: "nameof(nuget)")),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("GetTable", "CloudTable", "GetOrCreateAzureTable(nuget)", documentationCommentList: Comments.Summary("Returns an Azure CloudTable reference for a table named \"nuget\" in the storage account for the connection string stored in the environment"))));

        private static Project DevOps_Build_AppVeyor_Functions_AddBuild(this Project project)
            => project.Class(Name(nameof(DevOps_Build_AppVeyor_Functions_AddBuild)), UnifiedVersion,
                "AppveyorBuildAdder",
                "Function adds the given repository build information to an Azure Storage Table ledger",
                Dependencies(
                    Package(nameof(Cloud_Azure_Storage_Table_Functions_GetAzureTable)),
                    Package(nameof(DevOps_Build_AppVeyor_AzureTableStorageLedger_Builder))),
                environmentVariables: new Dictionary<string, string>(Array(new KeyValuePair<string, string>(
                    "AZURE_STORAGE_CONNECTION_STRING", "Connection string to your Azure Storage instance"))),
                @static: true,
                UsingDirectiveLists.Create(
                    UsingDirectives.UsingStatic($"{Package(nameof(DevOps_Build_AppVeyor_Functions_GetAzureTable))}.BuildTableGetter"),
                    UsingDirectives.UsingStatic($"{Package(nameof(DevOps_Build_AppVeyor_AzureTableStorageLedger_Builder))}.AppveyorBuildTableHelper")),
                Comments.Summary("Azure Table Storage entity representing a successfully completed AppVeyor build"),
                methodList: MethodLists.Create(
                    Methods.PublicStaticAsync("AddBuildAsync", TypeConstants.Task,
                        Blocks.Create(
                            "var entry = BuildTableEntry(name, version, dependencies);",
                            "var operation = TableOperation.InsertOrReplace(entry);",
                            "var table = GetTable();",
                            "await table.ExecuteAsync(operation);"),
                        ParameterLists.Create(
                            new Parameter("name", TypeConstants.String),
                            new Parameter("version", TypeConstants.String),
                            new Parameter("dependencies", TypeConstants.String, new Expression("null"))),
                        documentationCommentList: Comments.Summary("Returns an Azure CloudTable reference for a table named \"nuget\" in the storage account for the connection string stored in the environment"))));

        private static Project DevOps_Build_AppVeyor_Functions_GetBuildDependencyString(this Project project)
            => project.Class(Name(nameof(DevOps_Build_AppVeyor_Functions_GetBuildDependencyString)), UnifiedVersion,
                "BuildDependencyStringGetter",
                "Function gets the given repository's dependency string from the Azure Storage Table AppVeyor build ledger",
                Dependencies(
                    Package(nameof(Cloud_Azure_Storage_Table_Functions_GetAzureTable)),
                    Package(nameof(DevOps_Build_AppVeyor_AzureTableStorageLedger))),
                environmentVariables: new Dictionary<string, string>(Array(new KeyValuePair<string, string>(
                    "AZURE_STORAGE_CONNECTION_STRING", "Connection string to your Azure Storage instance"))),
                @static: true,
                UsingDirectiveLists.Create(
                    UsingDirectives.UsingStatic($"{Package(nameof(DevOps_Build_AppVeyor_Functions_GetAzureTable))}.BuildTableGetter")),
                Comments.Summary("Function gets the given repository's dependency string from the Azure Storage Table AppVeyor build ledger"),
                methodList: MethodLists.Create(
                    Methods.PublicStaticAsync("GetDependencyStringAsync", "Task<string>",
                        Blocks.Create(
                            "var operation = TableOperation.Retrieve<AppveyorBuildTable>(name, version);",
                            "var table = GetTable();",
                            "var result = await table.ExecuteAsync(operation);",
                            "if (result?.Result == null) return null;",
                            "return ((AppveyorBuildTable)result.Result)).Dependencies;"),
                        ParameterLists.Create(
                            new Parameter("name", TypeConstants.String),
                            new Parameter("version", TypeConstants.String)),
                        documentationCommentList: Comments.Summary("Returns the given repository's dependency string from the Azure Storage Table AppVeyor build ledger"))));

        private static NuGetReference Package(string repo)
            => NuGet($"CDorst.{Name(repo)}", UnifiedVersion);
    }

    public enum CDorstKnownTypeIds
    {
        DevOps_Code_EntityModel_Strings_Ascii = 1,
        DevOps_Code_EntityModel_Strings_Unicode = 2
    }
}
