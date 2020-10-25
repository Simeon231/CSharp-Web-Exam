using Git.ViewModels;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitsService
    {
        public CommitRepository GetRepository(string repositoryId);

        public bool Create(string userId, string repositoryId, string description);

        public void Delete(string commitId);

        public IEnumerable<CommitViewModel> AllCommits(string userId);
    }
}
