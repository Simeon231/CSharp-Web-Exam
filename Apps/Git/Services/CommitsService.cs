using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CommitViewModel> AllCommits(string userId)
        {
            var commits = db.Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel()
            {
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                RepositoryName = x.Repository.Name,
                Id = x.Id,
            })
            .ToList();

            return commits;
        }

        public bool Create(string userId, string repositoryId, string description)
        {
            var repId = db.Repositories.FirstOrDefault(x => x.Id == repositoryId);

            if(repId == null)
            {
                return false;
            }

            var commit = new Commit()
            {
                CreatedOn = DateTime.Now,
                CreatorId = userId,
                Description = description,
                RepositoryId = repositoryId,
            };

            db.Commits.Add(commit);
            db.SaveChanges();

            return true;
        }

        public void Delete(string commitId)
        {
            var commit = db.Commits.FirstOrDefault(x => x.Id == commitId);

            if(commit == null)
            {
                return;
            }

            db.Commits.Remove(commit);
            db.SaveChanges();
        }

        public CommitRepository GetRepository(string repositoryId)
        {
            var repository = db.Repositories.FirstOrDefault(x => x.Id == repositoryId);
            var commitRep = new CommitRepository()
            {
                Id = repository.Id,
                Name = repository.Name,
            };

            return commitRep;
        }
    }
}
