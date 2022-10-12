using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ItemsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {   
            //do we need to process anything here the first time the user requests the create-an-item page?
            //no
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateItemViewModel data)
        {
            //......
            try
            {
                //1. implement the create method by
                //2. applying dependency injection and ask for ItemsService
                //3. itemsService.AddNewItem(......)



                ViewBag.Message = "Item added successfully";
            }
            catch (Exception ex)
            {
                //ViewBag : a dynamic object, it allows you to declare properties on the fly
                //log the exception
                ViewBag.Error = "There was a problem adding a new item. make sure all the fields are correctly filled";
            }

            return View();
        }
    }
}
