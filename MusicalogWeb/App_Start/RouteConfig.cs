using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicalogWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "AlbumList", // Route name
                "Album/Index", // URL with no parameters
                new { controller = "Album", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "AlbumEdit", // Route name
                "Album/AlbumEdit/{albumID}", // URL with parameters
                new { controller = "Album", action = "AlbumEdit", albumID = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "AlbumDelete", // Route name
                "Album/AlbumDelete/{albumID}", // URL with parameters
                new { controller = "Album", action = "AlbumDelete", albumID = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "AlbumSort", // Route name
                "Album/AlbumSort/{selSortBy}", // URL with parameters
                new { controller = "Album", action = "AlbumSort", selSortBy = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "AlbumListPagination", // Route name
                "Album/AlbumListPagination/{direction}", // URL with parameters
                new { controller = "Album", action = "AlbumListPagination", direction = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Album", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
