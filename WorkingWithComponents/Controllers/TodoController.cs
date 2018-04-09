using System.Web.Mvc;
using WorkingWithComponents.Controllers.Common;
using WorkingWithComponents.ViewModels.Todo;

namespace WorkingWithComponents.Controllers
{
    public class TodoController : ComponentController
    {
        [ChildActionOnly]
        public ActionResult Component(int id)
        {
            return View(new TodoItemViewModel { Name = "Make a component", Status = "Completed" });
        }

        [ChildActionOnly]
        public ActionResult ComponentList()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ComponentPagedList()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ComponentAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ComponentAdd(TodoItemViewModel item)
        {
            item.Id = 1;
            return Redirect(item);
        }

        [ChildActionOnly]
        public ActionResult ComponentEdit()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ComponentDelete()
        {
            return View();
        }
    }
}