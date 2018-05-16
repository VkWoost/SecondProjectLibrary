﻿using Library.BLL.MappingProiles;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Library.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.AddProfile<MagazineProfile>();
                cfg.AddProfile<BrochureProfile>();
                cfg.AddProfile<BookProfile>();
                cfg.AddProfile<AuthorProfile>();
                cfg.AddProfile<PublicationHouseProfile>();
                });
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
