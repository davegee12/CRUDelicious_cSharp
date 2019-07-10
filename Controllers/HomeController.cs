using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;


namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private DishesContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(DishesContext context)
        {
            dbContext = context;
        }

        // Index Page
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes.ToList();
            IEnumerable<Dish> OrderByDescending = AllDishes.OrderByDescending(dish => dish.CreatedAt);
            ViewBag.AllDishes = OrderByDescending;
            return View("Index");
        }

        // Display Create New Dish Page
        [HttpGet("new")]
        public IActionResult New()
        {
            return View("New");
        }

        // Create POST route
        [HttpPost("create")]
        public IActionResult Create(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("New");
            }
        }

        // Display Show Page
        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            Dish ShowDish = dbContext.Dishes.FirstOrDefault(show => show.DishId == id);
            return View("Show", ShowDish);
        }

        // Display Update Page
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            Dish EditDish = dbContext.Dishes.FirstOrDefault(edit => edit.DishId == id);
            return View("Edit", EditDish);
        }

        // Update POST route
        [HttpPost("update/{id}")]
        public IActionResult Update(int id, Dish editDish)
        {
            if(ModelState.IsValid)
            {
                Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
                RetrievedDish.Name = editDish.Name;
                RetrievedDish.Chef = editDish.Chef;
                RetrievedDish.Calories = editDish.Calories;
                RetrievedDish.Tastiness = editDish.Tastiness;
                RetrievedDish.Description = editDish.Description;
                RetrievedDish.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return Redirect($"/{RetrievedDish.DishId}");
            }
            else
            {
                return View("Edit");
            }
        }

        // Delete
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
            dbContext.Dishes.Remove(RetrievedDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
