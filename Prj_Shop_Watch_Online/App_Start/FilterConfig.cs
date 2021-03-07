using System.Web;
using System.Web.Mvc;

namespace Prj_Shop_Watch_Online
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
