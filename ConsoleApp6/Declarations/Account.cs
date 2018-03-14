using DevOps.Primitives.SourceGraph;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Declarations
{
    public class Account
    {
        public Account()
        {
            if (Code == null) Code = new List<Code>();
            if (Entities == null) Entities = new List<Entity>();
            if (Metapackages == null) Metapackages = new List<Metapackage>();
        }
        public Account(string accountName, string authorEmail, string authorFullName, string appveyorAzureStorageSecret, List<Code> code = null, List<Entity> entities = null, List<Metapackage> metapackages = null) : this()
        {
            AccountName = accountName;
            AppveyorAzureStorageSecret = appveyorAzureStorageSecret;
            AuthorEmail = authorEmail;
            AuthorFullName = authorFullName;
            if (code != null) Code = code;
            if (entities != null) Entities = entities;
            if (metapackages != null) Metapackages = metapackages;
        }

        public string AccountName { get; set; }
        public string AppveyorAzureStorageSecret { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorFullName { get; set; }
        public List<Code> Code { get; set; }
        public List<Entity> Entities { get; set; }
        public List<Metapackage> Metapackages { get; set; }

        public AccountDeclaration GetAccountDeclaration()
            => new AccountDeclaration(AccountName, AuthorFullName, AuthorEmail, AppveyorAzureStorageSecret, Code, Entities, Metapackages);

        public GitHubAccount GenerateAccount()
            => GetAccountDeclaration().GenerateAccount();

        public Account WithClass(Class @class)
            => WithCode(@class.GetCode());

        public Account WithClasses(IEnumerable<Class> classes)
            => WithCode(classes.Select(c => c.GetCode()));

        public Account WithCode(Code code)
        {
            Code.Add(code);
            return this;
        }

        public Account WithCode(IEnumerable<Code> code)
        {
            Code.AddRange(code);
            return this;
        }

        public Account WithEntity(Entity entity)
        {
            Entities.Add(entity);
            return this;
        }

        public Account WithEntities(IEnumerable<Entity> entities)
        {
            Entities.AddRange(entities);
            return this;
        }

        public Account WithInterface(Interface @interface)
            => WithCode(@interface.GetCode());

        public Account WithInterfaces(IEnumerable<Interface> interfaces)
            => WithCode(interfaces.Select(i => i.GetCode()));

        public Account WithMetapackage(Metapackage metapackage)
        {
            Metapackages.Add(metapackage);
            return this;
        }

        public Account WithMetapackages(IEnumerable<Metapackage> metapackages)
        {
            Metapackages.AddRange(metapackages);
            return this;
        }
    }
}
