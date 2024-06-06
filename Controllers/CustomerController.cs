using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Webmvc.Models;

namespace Webmvc.Controllers;

public class CustomerController : Controller
{
    private readonly AppDbContext _db;

    public CustomerController(AppDbContext db)
    {
        _db = db;
    }

    [ActionName("List")]
    public IActionResult Get()
    {
        List<CustomerModel> Customerlst = _db.Customers.ToList();
        return View("Index", Customerlst);
    }
}
