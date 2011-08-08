using System.Web.Mvc;

namespace TestWeb2.Areas.MPArea
{
    public class MPAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MPArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MPArea_default",
                "MPArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
