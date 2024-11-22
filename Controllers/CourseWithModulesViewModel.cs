using Newtonsoft.Json;

namespace AngularCRUD2.Controllers
{
 public class CourseWithModulesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public decimal Fees { get; set; }

        // List of modules associated with the course
        public System.Collections.Generic.List<ModuleViewModel> Modules { get; set; }
    }

    public class ModuleViewModel
    {
        public string Title { get; set; }
        public string Contents { get; set; }

        public int Duration { get; set; }
    }
}