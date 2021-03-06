namespace AngularPlanner
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHtml5Routes(new List<string>()
            {
                "login",
                "login/{url}",
                "register",
                "expenses/add",
                "expenses",
                "expenses/{id}",
                "expenses/tag/{tag}",
                "expenses/tag/{tag}/{id}",
                "expenses/date/{date}",
                "expenses/date/{date}/{id}",
                "expenses/edit",
                "account",
                "statistics",
                "limits",
                "summaries",
                "simulations",
                "users"
            }, new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
