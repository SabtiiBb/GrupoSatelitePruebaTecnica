using System.Web;
using System.Web.Mvc;

namespace GrupoSatelitePruebaTecnica
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
