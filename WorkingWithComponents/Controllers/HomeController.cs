using System.Web.Mvc;
using WorkingWithComponents.Controllers.Common;

namespace WorkingWithComponents.Controllers
{
    public class HomeController : ComponentController
    {
        public ActionResult Index() => AddComponent("Todo", new { id = 1 }, title: "Todo item").Build();

        public ActionResult AddTodo() => AddComponent("Todo", action: "ComponentAdd", title: "Add Todo item").WithRedirect("Index", "Home").Build();
    }
}