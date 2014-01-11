using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wad.iFollow.Web.Models;

namespace Wad.iFollow.Web.Controllers
{
    public class BaseController : Controller
    {

        protected  ifollowdatabaseEntities4 entities; 
        public BaseController()
        {
            entities = new ifollowdatabaseEntities4();
        }

    }
}
