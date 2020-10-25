using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    class CommitsController : Controller
    {
        ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var commitRep = this.commitsService.GetRepository(id);

            return this.View(commitRep);
        }

        [HttpPost]
        public HttpResponse Create(string description, string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (description.Length < 5)
            {
                return this.Error("Description should have atleast 5 characters");
            }

            var isDone = this.commitsService.Create(this.GetUserId(), id, description);

            if(isDone == false)
            {
                return this.Error("Invalid repository");
            }

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var commits = this.commitsService.AllCommits(this.GetUserId());

            return this.View(commits);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.commitsService.Delete(id);

            return this.Redirect("/Commits/All");
        }
    }
}
