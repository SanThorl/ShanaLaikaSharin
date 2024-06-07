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
        return View("List", Customerlst);
    }

    [ActionName("Edit")]
    public IActionResult Edit(int id)
    {
        CustomerModel item = _db.Customers.FirstOrDefault(x => x.CustomerId == id)!;
        if (item is null)
        {
            return Redirect("List");
        }

        return View("Edit", item);
    }

    [ActionName("Create")]
    public IActionResult Create()
    {
        return View("Save");
    }

    [HttpPost]
    [ActionName("Save")]
    public IActionResult Save(CustomerModel model)
    {
        _db.Customers.Add(model);
        _db.SaveChanges();
        return Redirect("List");
    }

    [HttpPost]
    [ActionName("Update")]
    public IActionResult Update(int id, CustomerModel model)
    {
        var item = _db.Customers.FirstOrDefault(x => x.CustomerId == id);
        if (item is null)
        {
            return Redirect("List");
        }
        item.CustomerName = model.CustomerName;
        item.PhoneNo = model.PhoneNo;
        item.DateOfBirth = model.DateOfBirth;
        item.Gender = model.Gender;

        _db.SaveChanges();
        return Redirect("/Customer/List");
    }

    [HttpDelete]
    [ActionName("Delete")]
    public IActionResult Delete(int id)
    {
        var item = _db.Customers.FirstOrDefault(x => x.CustomerId == id);
        if (item is null)
        {
            return Redirect("List");
        }
        _db.Customers.Remove(item);
        _db.SaveChanges();
        return Redirect("List");
    }
}
