using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wad.iFollow.Web.Models
{
    public class FollowerData
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string avatar { get; set; }
        public bool isFollowed { get; set; }
    }

    public class FollowersModel
    {
        public List<FollowerData> wallElements { get; set; }

        public void BuildRecommendationsForUser(long id)
        {
            using (var conn = new ifollowdatabaseEntities4())
            {
                user currentUser = conn.users.First(u => u.id == id);
                wallElements = new List<FollowerData>();
                List<user> existingUsers = conn.users.ToList();
               
                foreach(user u in existingUsers)
                {
                    if (u.id != currentUser.id && 
                        conn.followers.Any(f => f.followedId == u.id && f.followerId == currentUser.id) == false)
                    {
                        FollowerData fd = new FollowerData();
                        fd.id = u.id;
                        fd.firstName = u.firstName;
                        fd.lastName = u.lastName;
                        fd.isFollowed = false;

                        if (u.images.Any(i => i.isAvatar == true))
                        {
                            image av = u.images.First(i => i.isAvatar == true);
                            fd.avatar = av.url;
                        }
                        else
                        {
                            fd.avatar = "Images/placeholderProfile.jpg";
                        }
                        wallElements.Add(fd);
                    }
                }
            }
        }

        public void BuildFollowersForUser(long id)
        {
            using (var conn = new ifollowdatabaseEntities4())
            {
                user currentUser = conn.users.First(u => u.id == id);
                wallElements = new List<FollowerData>();
                List<follower> fids = conn.followers.ToList();
                
                foreach(follower f in fids)
                {
                    if(conn.users.Any(uu => uu.id == f.followerId && f.followedId == currentUser.id))
                    {
                        user u = conn.users.First(uu => uu.id == f.followerId && f.followedId == currentUser.id);
                        bool isFollowed = conn.users.Any(uu => u.id == f.followedId && f.followerId == currentUser.id);

                        FollowerData fd = new FollowerData();
                        fd.id = u.id;
                        fd.firstName = u.firstName;
                        fd.lastName = u.lastName;
                        fd.isFollowed = isFollowed;

                        if (u.images.Any(i => i.isAvatar == true))
                        {
                            image av = u.images.First(i => i.isAvatar == true);
                            fd.avatar = av.url;
                        }
                        else
                        {
                            fd.avatar = "Images/placeholderProfile.jpg";
                        }
                        wallElements.Add(fd);
                    }
                }                
            }

            if (wallElements.Count() == 0)
            {
                throw (new Exception());
            }
        }

        public void BuildFollowedForUser(long id)
        {
            using (var conn = new ifollowdatabaseEntities4())
            {
                user currentUser = conn.users.First(u => u.id == id);
                wallElements = new List<FollowerData>();
                List<follower> fids = conn.followers.ToList();

                foreach (follower f in fids)
                {
                    if (conn.users.Any(uu => uu.id == f.followedId && f.followerId == currentUser.id))
                    {
                        user u = conn.users.First(uu => uu.id == f.followedId && f.followerId == currentUser.id);
                        FollowerData fd = new FollowerData();
                        fd.id = u.id;
                        fd.firstName = u.firstName;
                        fd.lastName = u.lastName;
                        fd.isFollowed = true;

                        if (u.images.Any(i => i.isAvatar == true))
                        {
                            image av = u.images.First(i => i.isAvatar == true);
                            fd.avatar = av.url;
                        }
                        else
                        {
                            fd.avatar = "Images/placeholderProfile.jpg";
                        }
                        wallElements.Add(fd);
                    }
                }
            }

            if(wallElements.Count() == 0)
            {
                throw (new Exception());
            }
        }
    }
}