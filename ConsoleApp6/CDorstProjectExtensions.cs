using ConsoleApp6.Declarations;
using DevOps.Primitives.CSharp;
using DevOps.Primitives.CSharp.Helpers.Common;
using DevOps.Primitives.CSharp.Helpers.EntityFramework;
using DevOps.Primitives.NuGet;
using System.Collections.Generic;
using static ConsoleApp6.CDorstProjectNames;
using static ConsoleApp6.Declarations.Helpers.NuGetDependencies;

namespace ConsoleApp6
{
    public static class CDorstProjectExtensions
    {
        private const string UnifiedVersion = "1.0.9";

        public static Project AddRepositories(this Project project)
            => project
                .DevOps_Code_EntityModel_Common_Metapackages_EntityFrameworkCore()
                .DevOps_Code_EntityModel_Common_Interfaces_Entity()
                .DevOps_Code_EntityModel_Common_Interfaces_StaticEntity()
                //.DevOps_Code_EntityModel_Strings_Ascii()
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

        private static NuGetReference Package(string repo)
            => NuGet($"CDorst.{Name(repo)}", UnifiedVersion);
    }

    public enum CDorstKnownTypeIds
    {
        DevOps_Code_EntityModel_Strings_Ascii = 1
    }
}
