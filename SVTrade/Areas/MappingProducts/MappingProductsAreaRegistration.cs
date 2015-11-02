using System.Web.Mvc;

namespace SVTrade.Areas.MappingProducts
{
    public class MappingProductsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MappingProducts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MappingProducts_default",
                "MappingProducts/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}