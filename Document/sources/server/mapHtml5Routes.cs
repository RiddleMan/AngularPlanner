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
