using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    class RepositoriesService : IRepositoriesService
    {
        ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<RepositoryViewModel> All()
        {
            var repositories = db.Repositories
                .Where(x => x.IsPublic)
                .Select(x => new RepositoryViewModel()
            {
                CommitsCount = x.Commits.Count,
                CreatedOn = x.CreatedOn,
                Name = x.Name,
                Owner = x.Owner.Username,
                RepositoryType = x.IsPublic == true ? "public" : "private",
                Id = x.Id,
            })
            .ToList();

            return repositories;
        }

        public void Create(string userId, CreateRepositoryViewModel model)
        {
            var repository = new Repository()
            {
                IsPublic = model.RepositoryType.ToLower() == "public" ? true : false,
                CreatedOn = DateTime.Now,
                Name = model.Name,
                OwnerId = userId,
            };

            db.Repositories.Add(repository);
            db.SaveChanges();
        }
    }
}
