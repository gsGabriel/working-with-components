namespace WorkingWithComponents.ViewModels.Common
{
    public class ComponentViewModel
    {
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public object RouteValues { get; set; }
    }
}