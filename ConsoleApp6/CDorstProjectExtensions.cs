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
        private const string UnifiedVersion = "1.0.18";

        public static Project AddRepositories(this Project project)
            => project
                .DevOps_Code_EntityModel_Common_Metapackages_EntityFrameworkCore()
                .DevOps_Code_EntityModel_Common_Interfaces_Entity()
                .DevOps_Code_EntityModel_Common_Interfaces_StaticEntity()
                .DevOps_Code_EntityModel_Strings_Ascii()
                .DevOps_Code_EntityModel_Strings_Unicode()
                .Cloud_Azure_Storage_Connection_Functions_GetConnectionString()
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
                fieldList: FieldLists.Create(Fields.PrivateConst("EmvironmentVariableName", TypeConstants.String, "Name of the environment variable containing the Azure Storage connection string", null, "\"AZURE_STORAGE_CONNECTION_STRING\";")),
                methodList: MethodLists.Create(
                    Methods.PublicStatic("ConnectionString", TypeConstants.String, "GetEnvironmentVariable(EnvironmentVariableName);")));

        private static NuGetReference Package(string repo)
            => NuGet($"CDorst.{Name(repo)}", UnifiedVersion);
    }

    public enum CDorstKnownTypeIds
    {
        DevOps_Code_EntityModel_Strings_Ascii = 1,
        DevOps_Code_EntityModel_Strings_Unicode = 2
    }
}
