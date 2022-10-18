using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{

    //Design Patterns - Creational, Behavioural, Structural
    //Dependency Injection - it is about centralizing and therefore better management of the (creation of) instances
    //1 variation is Constructor Injection

    public class ItemsController : Controller
    {
        private ItemsService itemsService { get; set; }
        public ItemsController (ItemsService _itemsService)
        {
            itemsService = _itemsService;
        }

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
                itemsService.AddNewItem(data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);
                 

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

        public IActionResult List()
        {
         var list = itemsService.ListItems();
            return View(list); //connection to the database is opened only here 
            //to render the text black on white on the page
        }
       
    }
}
