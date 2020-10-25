using Git.Services;
using Git.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    class RepositoriesController : Controller
    {
        IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var repositories = repositoriesService.All();

            return this.View(repositories);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateRepositoryViewModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (model.Name.Length < 3 || model.Name.Length > 10)
            {
                return this.Error("Repository name should be between 3 and 10.");
            }

            this.repositoriesService.Create(this.GetUserId(), model);

            return this.Redirect("/Repositories/All");
        }
    }
}
