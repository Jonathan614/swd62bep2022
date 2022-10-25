using BusinessLogic.ViewModels;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
   public  class CategoriesService
    {
        private readonly CategoriesRepository cr;
        public CategoriesService(CategoriesRepository _cr)
        {
            cr = _cr;
        }

        //it is never recommended to return the class that shapes the db
        public IQueryable<CategoryViewModel> GetCategories()
        {

            var list = from c in cr.GetCategories() //todo: automapper
                       select new CategoryViewModel()
                       {
                           Id = c.Id,
                           Title = c.Title
                       }
                       ;
            return list;
        }
    }
}
