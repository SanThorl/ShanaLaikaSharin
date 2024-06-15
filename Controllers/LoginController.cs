using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Webmvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;

        public LoginController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel reqModel)
        {
            var item = await _db.Users.FirstOrDefaultAsync(x => x.UserName == reqModel.UserName
            && x.Password == reqModel.Password);
            if (item is null)
                return View();
            string sessionId = Guid.NewGuid().ToString();
            DateTime sessionExpire = DateTime.Now.AddSeconds(50);

            CookieOptions cookie = new CookieOptions();
            cookie.Expires = sessionExpire;
            Response.Cookies.Append("UserId", item.UserId, cookie);
            Response.Cookies.Append("SessionId", sessionId, cookie);

            await _db.Login.AddAsync(new LoginModel
            {
                UserId = item.UserId,
                SessionId = sessionId,
                SessionExpired = sessionExpire
            });
            await _db.SaveChangesAsync();
            return Redirect("/Home");
        }
    }
}
