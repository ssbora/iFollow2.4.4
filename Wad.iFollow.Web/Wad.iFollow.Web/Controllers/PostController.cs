using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Wad.iFollow.Web.Models;

namespace Wad.iFollow.Web.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        [HttpPost]
        public ActionResult Upload(UploadFileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                image newImage = null;
                user currentUser = Session["user"] as user;

                if (fileModel != null && fileModel.File != null)
                {
                    string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") + ".png";
                    var path = Path.Combine(Server.MapPath("~/Images/UserPhotos"), timestamp);
                    fileModel.File.SaveAs(path);
                    fileModel.Path = timestamp;

                    using (var entities = new ifollowdatabaseEntities4())
                    {
                        newImage = new image();
                        newImage.isAvatar = false;
                        newImage.isDeleted = false;
                        newImage.url = timestamp;

                        int count = entities.images.Count();
                        newImage.id = count + 1;
                        newImage.ownerId = currentUser.id;

                        entities.images.Add(newImage);
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
                }

                using (var entities = new ifollowdatabaseEntities4())
                {
                    post newPost = new post();

                    newPost.dateCreated = DateTime.UtcNow;
                    int count = entities.posts.Count();
                    newPost.id = count + 1;

                    if (newImage != null)
                    {
                        newPost.imageId = newImage.id;
                    }
                    newPost.ownerId = currentUser.id;
                    newPost.message = fileModel.Message;
                    newPost.rating = 0;
                    newPost.isDeleted = false;

                    entities.posts.Add(newPost);
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
                return RedirectToAction("MainPage", "Wall");
            }

            return RedirectToAction("MainPage", "Wall");
        }
    }
}
