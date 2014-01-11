using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Wad.iFollow.Web.Helpers
{
    public static class LayoutHelper
    {
            public static string GetLayout(RouteData data, string defaultLayout)
            {
                if (data.Values["action"] == "MainPage")
                    return "~/views/shared/_MainLayout.cshtml";

                return defaultLayout;
            }
    }
}