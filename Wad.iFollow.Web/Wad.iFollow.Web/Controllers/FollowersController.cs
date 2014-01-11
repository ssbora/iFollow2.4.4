using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wad.iFollow.Web.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Wad.iFollow.Web.Controllers
{
    public class FollowersController : Controller
    {
        [HttpPost]
        public ActionResult Follow(string submit)
        {
            user currentUser = Session["user"] as user;
            long uid = (long)Convert.ToDouble(submit);

            using(var entities = new ifollowdatabaseEntities4())
            {
                currentUser = entities.users.First(u => u.id == currentUser.id);
                follower fol = new follower();
                fol.followedId = uid;
                fol.followerId = currentUser.id;
                entities.followers.Add(fol);

                try
                {
                    entities.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }

            return RedirectToAction("Followers", "Wall");
        }
    }
}
