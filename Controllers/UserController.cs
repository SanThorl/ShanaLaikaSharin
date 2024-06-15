using Microsoft.AspNetCore.Mvc;

namespace Webmvc.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult Index(UserModel model)
        {
            model.UserId = Ulid.NewUlid().ToString();
            _context.Users.Add(model);
            _context.SaveChanges();
            return Redirect("/Login");
        }
    }
}
