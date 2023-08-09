using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext db;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    // ========================Root===============================
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> AllDishes = db.Dishes.OrderByDescending(a => a.CreatedAt).ToList();
        ViewBag.everyDish = AllDishes;
        return View(AllDishes);
    }

    // ====================New Method=============================

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View();
    }


    // ====================Create Method===========================

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {

            return View("NewDish");
        }
        db.Dishes.Add(newDish);
        db.SaveChanges();
        return RedirectToAction("Index");
    }



    // ====================GetOne by Id Method================================


    [HttpGet("dishes/{id}/view")]
    public IActionResult ViewDish(int id)
    {
        Dish OneDish = db.Dishes.FirstOrDefault(a => a.DishId == id);
        return View(OneDish);

    }



    // ====================Edit method=====================
    [HttpGet("dishes/{id}/edit")]
    public IActionResult EditDish(int id)
    {
        Dish OneDish = db.Dishes.FirstOrDefault(a => a.DishId == id);
        if (OneDish == null)
        {
            return RedirectToAction("Index");
        }
        return View("EditDish", OneDish);
    }


    // ========================Update Method==================================
    [HttpPost("dishes/{id}/update")]
    public IActionResult Update(Dish newDish, int id)
    {
        if(!ModelState.IsValid)
        {
            return EditDish(id);
        }
        Dish? OldDish = db.Dishes.FirstOrDefault(i => i.DishId == id);

        if (OldDish==null)
        {
            return RedirectToAction("Index");
        }   
            OldDish.Name = newDish.Name;
            OldDish.Chef = newDish.Chef;
            OldDish.Tastiness = newDish.Tastiness;
            OldDish.Calories = newDish.Calories;
            OldDish.Description = newDish.Description;
            db.Dishes.Update(OldDish);
            OldDish.UpdatedAt = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("ViewDish", new {id = id});
    }

// =======================Delete Method===============================


[HttpPost("posts/{id}/delete")]
    public IActionResult Delete(int id)
    {
        Dish? OnePost = db.Dishes.FirstOrDefault(a => a.DishId == id);
        db.Dishes.Remove(OnePost);
        db.SaveChanges();
        return RedirectToAction("Index");
        
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
