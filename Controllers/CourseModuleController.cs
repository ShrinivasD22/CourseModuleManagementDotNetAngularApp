using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularCRUD2.Models;
using System.Data.Entity;

namespace AngularCRUD2.Controllers
{
    public class CourseModuleController : Controller
    {
        private readonly coursesmoduleEntities db = new coursesmoduleEntities();

        //my 
        public ActionResult Index()
        {
            return View();
        }


        // GET: CourseModule/Create
        public ActionResult Create()
        {
            return View(); // This returns the Create view
        }

        public ActionResult GetCourses()
        {
            return View(); // Returns a view that uses Angular/JavaScript to fetch and display courses
        }

        public ActionResult EditCourse()
        {
            return View(); // Returns a view that uses Angular/JavaScript to fetch and display courses
        }
        public ActionResult DeleteCourse()
        {
            return View(); // Returns a view that uses Angular/JavaScript to fetch and display courses
        }

        public ActionResult GetCourseById()
        {
            return View(); // Returns a view that uses Angular/JavaScript to fetch and display courses
        }

        // GET: api/course/{id}
        [HttpGet]
        public JsonResult GetCourseByIdd(int id)
        {
            var course = db.Courses.Include(c => c.Modules).FirstOrDefault(c => c.ID == id);

            if (course == null)
            {
                return Json(new { success = false, message = "Course not found" }, JsonRequestBehavior.AllowGet);
            }

            var result = new
            {
                course.ID,
                course.Title,
                course.Duration,
                course.Fees,
                Modules = course.Modules.Select(m => new
                {
                    m.Title,
                    m.Contents,
                    m.Duration
                }).ToList()
            };

            return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
        }


        // GET: api/courses
        [HttpGet]
        public JsonResult GetCoursess()
        {
            var courses = db.Courses.Include(c => c.Modules).ToList();
            var result = courses.Select(course => new
            {
                course.ID,
                course.Title,
                course.Duration,
                course.Fees,
                Modules = course.Modules.Select(m => new { m.Title, m.Contents, m.Duration }).ToList()
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // POST: Create course
        [HttpPost]
        public JsonResult CreateCourse(CourseWithModulesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Title = model.Title,
                    Duration = model.Duration,
                    Fees = model.Fees,
                    Modules = model.Modules.Select(m => new Module
                    {
                        Title = m.Title,
                        Contents = m.Contents,
                        Duration = m.Duration
                    }).ToList()
                };

                db.Courses.Add(course);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid data" });
        }

        // PUT: Edit course
        [HttpPost]
        [Route("CourseModule/EditCourses/{id}")]
        public JsonResult EditCourses(int id, CourseWithModulesViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Fetch the course with the specified ID, including its related Modules
                var course = db.Courses.Include(c => c.Modules).FirstOrDefault(c => c.ID == id);
                if (course == null)
                {
                    return Json(new { success = false, message = "Course not found" });
                }

                // Update the course properties
                course.Title = model.Title;
                course.Duration = model.Duration;
                course.Fees = model.Fees;

                // Remove existing modules and add the updated ones
                db.Modules.RemoveRange(course.Modules);
                course.Modules = model.Modules.Select(m => new Module
                {
                    Title = m.Title,
                    Contents = m.Contents,
                    Duration = m.Duration,
                    CourseID = course.ID
                }).ToList();

                // Save changes to the database
                db.SaveChanges();

                return Json(new { success = true, message = "Course updated successfully." });
            }

            return Json(new { success = false, message = "Invalid data." });
        }


        // DELETE: Delete course
        // POST: Delete course
        [HttpPost]
        public JsonResult DeleteCourses(int id)
        {
            var course = db.Courses.Include(c => c.Modules).FirstOrDefault(c => c.ID == id);
            if (course != null)
            {
                db.Modules.RemoveRange(course.Modules);
                db.Courses.Remove(course);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Course not found" });
        }

    }
}
