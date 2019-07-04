using System.Web.Mvc;

namespace ParkSquare.BuildScreen.WebOld
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}