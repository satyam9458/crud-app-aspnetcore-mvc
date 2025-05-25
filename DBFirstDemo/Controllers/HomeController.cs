using System.Diagnostics;
using DBFirstDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DBFirstDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestDbContext context;
        public HomeController(ILogger<HomeController> logger,TestDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public IActionResult List()
        {
         List<Student> students = context.Students.ToList();
            return View(students);
        }
        public IActionResult Details(int? id)
        {
            if(id!=null)
            {
                Student st = context.Students.FirstOrDefault(item => item.Roll == id);
                if (st != null)
                {
                    return View(st);
                }
                else
                {
                    TempData["Message"] = "Record Not Found for Roll: "+id;
                    return RedirectToAction("List");
                }
            }
           TempData["Message"] = "Please pass the Roll to search information";
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Students.Add(stu);
                    context.SaveChanges();
                    TempData["success"] = "Data Successfully inserted";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "unable to insert data,please check...!";
                }
                return RedirectToAction("List");
            }
            else
            {

                return View(stu);
            }
        }
     
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Student stu = context.Students.FirstOrDefault(item => item.Roll == id);
                if (stu != null)
                {
                    return View(stu);
                }
                else
                {
                    TempData["Message"] = "Record Not Found for Roll: " + id;
                    return RedirectToAction("List");
                }
            }
            TempData["Message"] = "Please pass the Roll to search information";
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Edit(Student stu)
        {
            if (ModelState.IsValid)
            {
                
                    context.Students.Update(stu);
                    context.SaveChanges();
                    TempData["success"] = "Data Successfully updated...!";
                    return RedirectToAction("List");
            }
            return View(stu);
          
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Student stu = context.Students.FirstOrDefault(item => item.Roll == id);
                if (stu != null)
                {
                  return View(stu);
                }
                else
                {
                    TempData["Message"] = "Record Not Found for Roll: " + id;
                    return RedirectToAction("List");
                }
            }
            TempData["Message"] = "Please pass the Roll FOR DELETE DATA";
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(Student stu)
        {
            if (stu!=null)
            {
               
                    context.Students.Remove(stu);
                    context.SaveChanges();
                    TempData["success"] = "Data Successfully Deleted...!";
                    return RedirectToAction("List");
                
            }
            TempData["Message"] = "unable to delete data,please check...!";
            return RedirectToAction("List");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
