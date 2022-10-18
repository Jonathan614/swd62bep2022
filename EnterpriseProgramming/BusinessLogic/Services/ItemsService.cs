using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Repositories;
using System.Linq;
using BusinessLogic.ViewModels;

namespace BusinessLogic.Services
{
    public class ItemsService
    {
        //the centralization of creation of instances implies a more efficient management of objects
        //i.e. we have to use a Design Pattern
        //Design Pattern : Dependency Injection - a variation of this is Constructor injection

        private ItemsRepository itemsRepository;
        public ItemsService(ItemsRepository _itemRepository)
        {
            itemsRepository = _itemRepository;
        }


        public void AddNewItem(string name, double price, int categoryId, int stock=0, string imagePath=null)
        {
            /*
             * foreach(Item x in GetItems())
             *  if (x.Name == name) {throw new .....}
             * 
             */


            if (itemsRepository.GetItems().Any(x=>x.Name==name))
                throw new Exception("Item with the same already exists");

            itemsRepository.AddItem(new Domain.Models.Item()
            {
                CategoryId = categoryId,
                ImagePath = imagePath,
                Name = name,
                Price = price,
                Stock = stock
            });
          
        }
         

        public IQueryable<ItemViewModel> ListItems()
        {
            //convert from Item into ItemViewModel

            var list = from i in itemsRepository.GetItems()
                       select new ItemViewModel() //can be flattened using AutoMapper
                       {
                           Id = i.Id,
                           ImagePath = i.ImagePath,
                           Name = i.Name,
                           Price = i.Price,
                           Stock = i.Stock,
                           Category = i.Category.Title
                       };
            return list;
        }

        public IQueryable<ItemViewModel> Search(string name)
        {
            return ListItems().Where(x => x.Name.Contains(name));
        
        }

        public IQueryable<ItemViewModel> Search(string name, double minPrice, double maxPrice)
        {
            //1...GetItems //prepares the statement in memory
            //2...Where (1st filtering) //it amends the prepared statement in memory
            //3...Where (2nd filtering) //it further amends the prepared statement in memory

            return Search(name).
                Where(x => x.Price >= minPrice && x.Price <= maxPrice);
        }
       
    }
}
