using Microsoft.AspNetCore.Mvc;

namespace asplab10.Models
{
    public class ErrorMarker : ViewComponent
    {
        public IViewComponentResult? Invoke(Consultation consultation)
        {
            if (consultation == null)
                return View("Valid");
            if (consultation.Errors.Count == 0)
                return View("Valid");
            return View(consultation.Errors);
        }
    }
}

