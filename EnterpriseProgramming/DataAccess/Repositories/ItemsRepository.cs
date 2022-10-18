using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    //constructor injection
    //once you ask for an instance in the constructor as a parameter,
    //.net core will realize that it needs to create an instance of the asked class
    //and it will create it on the first call
    public class ItemsRepository
    {
        private ShoppingCartContext context;
        public ItemsRepository(ShoppingCartContext _context) //you ask for an instance of the context class
        {
            context = _context;
        }
           
        public void AddItem(Item i)
        {
           
            context.Items.Add(i);
            context.SaveChanges();
        }

        public void DeleteItem(Item i)
        {
            context.Items.Remove(i);
            context.SaveChanges();
        }

        public Item GetItem(int id)
        {
       

            return null; }

        public IQueryable<Item> GetItems() //IQueryable vs List: to explain
        {
            return context.Items;
        }

        public void Update(Item originalItem, Item newItem)
        { }
    }
}
