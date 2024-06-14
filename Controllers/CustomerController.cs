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
    public IActionResult GetCustomerList()
    {
        List<CustomerModel> Customerlst = _db.Customers.ToList();
        return View("CustomerList", Customerlst);
    }

    [ActionName("Edit")]
    public IActionResult EditCustomer(int id)
    {
        CustomerModel item = _db.Customers.FirstOrDefault(x => x.CustomerId == id)!;
        if (item is null)
        {
            return Redirect("List");
        }

        return View("EditCustomer", item);
    }

    [ActionName("Create")]
    public IActionResult CreateCustomer()
    {
        return View("Save");
    }

    [HttpPost]
    [ActionName("Save")]
    public IActionResult SaveCustomer(CustomerModel model)
    {
        _db.Customers.Add(model);
        _db.SaveChanges();
        return Redirect("/Customer/List");
    }

    [HttpPost]
    [ActionName("Update")]
    public IActionResult UpdateCustomer(int id, CustomerModel model)
    {
        var item = _db.Customers.FirstOrDefault(x => x.CustomerId == id);
        if (item is null)
        {
            return Redirect("Customer/List");
        }
        item.CustomerName = model.CustomerName;
        item.PhoneNo = model.PhoneNo;
        item.DateOfBirth = model.DateOfBirth;
        item.Gender = model.Gender;

        _db.SaveChanges();
        return Redirect("/Customer/List");
    }

    [ActionName("Delete")]
    public IActionResult DeleteCustomer(int id)
    {
        var item = _db.Customers.FirstOrDefault(x => x.CustomerId == id);
        if (item is null)
        {
            return Redirect("/Customer/List");
        }
        _db.Customers.Remove(item);
        _db.SaveChanges();
        return Redirect("/Customer/List"); 
    }
}
