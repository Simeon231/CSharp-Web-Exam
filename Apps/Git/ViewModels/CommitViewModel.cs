﻿using System;

namespace Git.ViewModels
{
    public class CommitViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string RepositoryName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
