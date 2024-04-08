using asplab10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace asplab10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Consultation signup)
        {
            if (signup.Subject.Equals("Basics") && signup.Date.DayOfWeek == DayOfWeek.Monday)
                ModelState.AddModelError("", "Консультації з дисципліни «Основи» не проводяться по понеділках!");
            if (signup.Date <= DateTime.Now)
                ModelState.AddModelError("Date", "Ви не можете зареєструватись на сьогоднішні консультації!");
            if (signup.Date.DayOfWeek == DayOfWeek.Sunday || signup.Date.DayOfWeek == DayOfWeek.Saturday)
                ModelState.AddModelError("Date", "Оберіть будній день для консультації!");
            if (ModelState.IsValid)
                return View("Success", signup);
            else
            {
                foreach (var item in ModelState)
                {
                    if (item.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                        foreach (var error in item.Value.Errors)
                            signup.Errors.Add($"{error.ErrorMessage}");
                }
                return View(signup);
            }

        }

        [HttpGet]
        public IActionResult Confirm(Consultation consultation)
        {
            return View(consultation);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
