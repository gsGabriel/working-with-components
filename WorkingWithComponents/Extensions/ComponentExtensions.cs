using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkingWithComponents.ViewModels.Common;

namespace WorkingWithComponents.Extensions
{
    public static class ComponentExtensions
    {
        private static IList<ComponentViewModel> components = new List<ComponentViewModel>();
        
        public static HtmlHelper AddComponent(this HtmlHelper htmlHelper, string controller, object routeValues = null, string action = "Component", string title = "")
        {
            components.Add(new ComponentViewModel { Controller = controller, RouteValues = routeValues, Action = action, Title = title });
            return htmlHelper;
        }
        
        public static HtmlHelper WithRedirect(this HtmlHelper htmlHelper, string actionRedirect, string controllerRedirect)
        {
            var lastComponent = components.Last();

            htmlHelper.ViewContext.Controller.TempData[$"{lastComponent.Controller}-{lastComponent.Action}"] = new RedirectViewModel { Action = actionRedirect, Controller = controllerRedirect };
            return htmlHelper;
        }
        
        public static MvcHtmlString Build(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(RenderViewToString(htmlHelper, "_PartialWrapper", new WrapperViewModel { Components = components }));
        }

        private static string RenderViewToString(HtmlHelper htmlHelper, string viewName, object model)
        {
            htmlHelper.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(htmlHelper.ViewContext.Controller.ControllerContext, viewName);
                var viewContext = new ViewContext(htmlHelper.ViewContext.Controller.ControllerContext, viewResult.View, htmlHelper.ViewData, htmlHelper.ViewContext.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(htmlHelper.ViewContext.Controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}