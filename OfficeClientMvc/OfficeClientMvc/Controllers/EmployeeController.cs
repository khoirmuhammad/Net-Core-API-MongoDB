using Microsoft.AspNetCore.Mvc;

namespace OfficeClientMvc.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
