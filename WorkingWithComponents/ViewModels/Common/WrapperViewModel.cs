using System.Collections.Generic;

namespace WorkingWithComponents.ViewModels.Common
{
    public class WrapperViewModel
    {
        public IEnumerable<ComponentViewModel> Components { get; set; }
    }
}