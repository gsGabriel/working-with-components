using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WorkingWithComponents.ViewModels.Common;

namespace WorkingWithComponents.Controllers.Common
{
    public class ComponentController : Controller
    {
        private readonly IList<ComponentViewModel> components;

        public ComponentController()
        {
            components = new List<ComponentViewModel>();
        }

        [NonAction]
        public ComponentController AddComponent(string controller, object routeValues = null, string action = "Component", string title = "")
        {
            components.Add(new ComponentViewModel { Controller = controller, RouteValues = routeValues, Action = action, Title = title });
            return this;
        }

        [NonAction]
        public ComponentController WithRedirect(string actionRedirect, string controllerRedirect)
        {
            var lastComponent = components.Last();

            this.TempData[$"{lastComponent.Controller}-{lastComponent.Action}"] = new RedirectViewModel { Action = actionRedirect, Controller = controllerRedirect };
            
            return this;
        }

        [NonAction]
        public ActionResult Build()
        {
            return View("_PartialWrapper", new WrapperViewModel { Components = components });
        }

        [NonAction]
        public ActionResult Redirect(object model)
        {
            var key = $"{this.RouteData.GetRequiredString("controller")}-{this.RouteData.GetRequiredString("action")}";

            if (this.TempData.ContainsKey(key))
            {
                TempData.TryGetValue(key, out dynamic redirect);

                return RedirectToAction(redirect.Action, redirect.Controller, model);
            }

            return RedirectToAction(this.RouteData.GetRequiredString("action"), this.RouteData.GetRequiredString("controller"), model);
        }
    }
}