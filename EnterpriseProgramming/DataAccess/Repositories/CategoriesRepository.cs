using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class CategoriesRepository
    {
        private ShoppingCartContext context;
        public CategoriesRepository(ShoppingCartContext _context) //you ask for an instance of the context class
        {
            context = _context;
        }

        public IQueryable<Category> GetCategories()
        {
            return context.Categories;
        }
    }
}
