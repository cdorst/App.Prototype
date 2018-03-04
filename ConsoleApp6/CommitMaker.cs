using DevOps.Primitives.SourceGraph;
using LibGit2Sharp;
using System;
using System.Linq;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;
using static System.IO.Path;
using static ConsoleApp6.FileWriter;
using static ConsoleApp6.CommitMessageWriter;

namespace ConsoleApp6
{
    public static class CommitMaker
    {
        public static bool TryMakeCommit(string authorEmail, string authorName, string repoDirectory, bool anyChanges, LibGit2Sharp.Repository repo, RepositoryFile file)
        {
            var fileNameSettings = file.FileName;
            var fileName = fileNameSettings.Name.Value;
            var pathParts = fileNameSettings.PathParts?.GetAssociations().Select(a => a.GetRecord().Value);
            var relativeDirectory = !Any(pathParts) ? string.Empty : Combine(pathParts.ToArray());
            var relativePath = Combine(relativeDirectory, fileName);
            var update = WriteFile(repoDirectory, file.Content.Value, relativeDirectory, relativePath);
            if (repo.RetrieveStatus().IsDirty)
            {
                if (!anyChanges) anyChanges = true;
                repo.Index.Add(relativePath);
                var signature = new Signature(authorName, authorEmail, DateTimeOffset.Now);
                repo.Commit(
                    CommitMessage(update, fileName),
                    signature, signature, new CommitOptions());
            }
            return anyChanges;
        }
    }
}
