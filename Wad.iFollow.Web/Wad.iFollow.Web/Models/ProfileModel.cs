using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wad.iFollow.Web.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        {
            elements = new WallPostsModel();
        }

        public string userName { get; set; }
        public long userId { get; set; }
        public bool isCurrentUser { get; set; }
        public int postsCount { get; set; }
        public int followersCount { get; set; }
        public int followedCount { get; set; }

        [FileSize(1024 * 1024 * 3)]
        [FileTypes("jpg,jpeg,png,gif")]
        public HttpPostedFileBase File { get; set; }

        public string avatarPath { get; set; }

        public WallPostsModel elements { get; set; }
    }
}