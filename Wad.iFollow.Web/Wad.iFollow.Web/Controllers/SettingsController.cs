using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wad.iFollow.Web.Models;

namespace Wad.iFollow.Web.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        public ActionResult Save(SettingsModel sm)
        {
            user currentUser = Session["user"] as user;
            using(var conn = new ifollowdatabaseEntities4())
            {
                currentUser = conn.users.First(u => u.id == currentUser.id);
                if (currentUser != null)
                {
                    currentUser.firstName = sm.firstName;
                    currentUser.lastName = sm.lastName;
                    currentUser.city = sm.city;
                    currentUser.country = sm.country;
                    currentUser.birthdate = sm.birthDate;

                    try
                    {
                        conn.SaveChanges();
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
            }
            return RedirectToAction("Settings","Wall");
        }
    }
}
