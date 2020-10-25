using Git.ViewModels;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        public void Create(string userId, CreateRepositoryViewModel model);

        public IEnumerable<RepositoryViewModel> All();
    }
}
