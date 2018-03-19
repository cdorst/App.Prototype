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
            if (Metapackages == null) Metapackages = new List<Metapackage>();
        }
        public Account(string accountName, string authorEmail, string authorFullName, string appveyorAzureStorageSecret, List<Code> code = null, List<Metapackage> metapackages = null) : this()
        {
            AccountName = accountName;
            AppveyorAzureStorageSecret = appveyorAzureStorageSecret;
            AuthorEmail = authorEmail;
            AuthorFullName = authorFullName;
            if (code != null) Code = code;
            if (metapackages != null) Metapackages = metapackages;
        }

        public string AccountName { get; set; }
        public string AppveyorAzureStorageSecret { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorFullName { get; set; }
        public List<Code> Code { get; set; }
        public List<Metapackage> Metapackages { get; set; }

        public AccountDeclaration GetAccountDeclaration()
            => new AccountDeclaration(AccountName, AuthorFullName, AuthorEmail, AppveyorAzureStorageSecret, Code, Metapackages);

        public GitHubAccount GenerateAccount()
            => GetAccountDeclaration().GenerateAccount();

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
