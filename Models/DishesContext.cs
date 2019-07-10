using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious
{
    public class DishesContext : DbContext
    {
        public DishesContext(DbContextOptions options) : base(options) {}

        public DbSet<Dish> Dishes {get;set;}
    }
}