using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AngularPlanner.Extensions
{
    public static class RouteCollectionExtension
    {
        public static void MapHtml5Routes(this RouteCollection route, List<string> routes, object defaults)
        {
            routes.ForEach(i => route.MapRoute(i, i, defaults));
        }
    }
}
