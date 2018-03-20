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
        private static readonly CommitOptions _commitOptions = new CommitOptions();

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
                repo.Index.Add(relativePath);
                var signature = GetSignature(authorEmail, authorName);
                try
                {
                    MakeCommit(repo, fileName, update, signature);
                    if (!anyChanges) anyChanges = true;
                }
                catch (EmptyCommitException)
                {
                    repo.Index.Remove(relativePath);
                }
            }
            return anyChanges;
        }

        private static Signature GetSignature(string authorEmail, string authorName)
            => new Signature(authorName, authorEmail, DateTimeOffset.Now);

        private static void MakeCommit(LibGit2Sharp.Repository repo, string fileName, bool update, Signature signature)
            => repo.Commit(CommitMessage(update, fileName), signature, signature, _commitOptions);
    }
}
