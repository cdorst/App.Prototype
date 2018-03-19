using DevOps.Primitives.SourceGraph;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated;
using System;
using System.Collections.Generic;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;
using static ConsoleApp6.Declarations.ProjectIndexRepositoryAdder;

namespace ConsoleApp6.Declarations
{
    public class Project
    {
        public Project()
        {
            if (Code == null) Code = new List<Code>();
            if (Metapackages == null) Metapackages = new List<Metapackage>();
        }
        public Project(string accountName, string authorEmail, string authorFullName, string appveyorAzureStorageSecret, List<Code> code = null, List<Metapackage> metapackages = null) : this()
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

        public GitHubAccount GetGitHubAccountDeclaration()
            => AddProjectIndexRepository(GetAccount());

        public Project WithCode(Code code)
        {
            Code.Add(code);
            return this;
        }

        public Project WithCode(IEnumerable<Code> code)
        {
            Code.AddRange(code);
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
            if (Any(Code)) account = account.WithCode(Code);
            if (Any(Metapackages)) account = account.WithMetapackages(Metapackages);
            return account;
        }
    }
}
