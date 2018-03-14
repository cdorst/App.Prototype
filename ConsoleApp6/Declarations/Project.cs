using DevOps.Primitives.SourceGraph;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated;
using System.Collections.Generic;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;
using static ConsoleApp6.Declarations.ProjectIndexRepositoryAdder;

namespace ConsoleApp6.Declarations
{
    public class Project
    {
        public Project()
        {
            if (Classes == null) Classes = new List<Class>();
            if (Entities == null) Entities = new List<Entity>();
            if (Interfaces == null) Interfaces = new List<Interface>();
            if (Metapackages == null) Metapackages = new List<Metapackage>();
        }
        public Project(string accountName, string authorEmail, string authorFullName, string appveyorAzureStorageSecret, List<Class> classes = null, List<Entity> entities = null, List<Interface> interfaces = null, List<Metapackage> metapackages = null) : this()
        {
            AccountName = accountName;
            AppveyorAzureStorageSecret = appveyorAzureStorageSecret;
            AuthorEmail = authorEmail;
            AuthorFullName = authorFullName;
            if (classes != null) Classes = classes;
            if (entities != null) Entities = entities;
            if (interfaces != null) Interfaces = interfaces;
            if (metapackages != null) Metapackages = metapackages;
        }

        public string AccountName { get; set; }
        public string AppveyorAzureStorageSecret { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorFullName { get; set; }
        public List<Class> Classes { get; set; }
        public List<Entity> Entities { get; set; }
        public List<Interface> Interfaces { get; set; }
        public List<Metapackage> Metapackages { get; set; }

        public GitHubAccount GetGitHubAccountDeclaration()
            => AddProjectIndexRepository(GetAccount());

        public Project WithClass(Class @class)
        {
            Classes.Add(@class);
            return this;
        }

        public Project WithClasses(IEnumerable<Class> classes)
        {
            Classes.AddRange(classes);
            return this;
        }

        public Project WithEntity(Entity entity)
        {
            Entities.Add(entity);
            return this;
        }

        public Project WithEntities(IEnumerable<Entity> entities)
        {
            Entities.AddRange(entities);
            return this;
        }

        public Project WithInterface(Interface @interface)
        {
            Interfaces.Add(@interface);
            return this;
        }

        public Project WithInterfaces(IEnumerable<Interface> interfaces)
        {
            Interfaces.AddRange(interfaces);
            return this;
        }

        public Project WithMetapackage(Metapackage metapackage)
        {
            Metapackages.Add(metapackage);
            return this;
        }

        public Project WithMetapackages(IEnumerable<Metapackage> metapackages)
        {
            Metapackages.AddRange(metapackages);
            return this;
        }

        private Account GetAccount()
        {
            var account = new Account(AccountName, AuthorEmail, AuthorFullName, AppveyorAzureStorageSecret);
            if (Any(Classes)) account = account.WithClasses(Classes);
            if (Any(Entities)) account = account.WithEntities(Entities);
            if (Any(Interfaces)) account = account.WithInterfaces(Interfaces);
            if (Any(Metapackages)) account = account.WithMetapackages(Metapackages);
            return account;
        }
    }
}
