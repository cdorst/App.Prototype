using DevOps.Primitives.SourceGraph;
using DevOps.Primitives.SourceGraph.Helpers.Common.Accounts;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated;
using System.Collections.Generic;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.GitHubAccountBuilder;

namespace ConsoleApp6.Declarations
{
    public class AccountDeclaration
    {
        public AccountDeclaration() { }
        public AccountDeclaration(string accountName, string appveyorAzureStorageSecret, string authorEmail, string authorFullName, string copyright, string packageCacheUri, string packageIconUri, string namespacePrefix, List<Code> code, List<Metapackage> metapackages)
        {
            AccountName = accountName;
            AppveyorAzureStorageSecret = appveyorAzureStorageSecret;
            AuthorEmail = authorEmail;
            AuthorFullName = authorFullName;
            Code = code;
            Copyright = copyright;
            Metapackages = metapackages;
            NamespacePrefix = namespacePrefix;
            PackageCacheUrl = packageCacheUri;
            PackageIconUrl = packageIconUri;
        }
        public AccountDeclaration(string namespacePrefix, string authorFullName, string authorEmail, string appveyorAzureStorageSecret, List<Code> code, List<Metapackage> metapackages)
            : this(
                  namespacePrefix.ToLower(),
                  appveyorAzureStorageSecret,
                  authorEmail,
                  authorFullName,
                  $"Copyright © {authorFullName}",
                  $"https://{namespacePrefix.ToLower()}-dev.azureedge.net/nuget",
                  $"https://{namespacePrefix.ToLower()}-dev.azureedge.net/nuget/package-icon-64.png",
                  namespacePrefix,
                  code,
                  metapackages)
        {
        }

        public string AccountName { get; set; }
        public string AppveyorAzureStorageSecret { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorFullName { get; set; }
        public string Copyright { get; set; }
        public string NamespacePrefix { get; set; }
        public string PackageCacheUrl { get; set; }
        public string PackageIconUrl { get; set; }

        public List<Code> Code { get; set; }
        public List<Metapackage> Metapackages { get; set; }

        public GitHubAccount GenerateAccount()
            => GitHub(
                new GitHubAccountSpecification(AccountName, AppveyorAzureStorageSecret, AuthorEmail, AuthorFullName, Copyright, PackageCacheUrl, PackageIconUrl, NamespacePrefix),
                Code,
                Metapackages);
    }
}
