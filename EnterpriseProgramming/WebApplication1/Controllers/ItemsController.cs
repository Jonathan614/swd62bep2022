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
        private ItemsService itemsService;
        private CategoriesService categoriesService;
        public ItemsController (ItemsService _itemsService, CategoriesService _categoriesService)
        {
            itemsService = _itemsService;
            categoriesService = _categoriesService;
                 
        }

        [HttpGet] //Get method is called to load the page with blank controls
        public IActionResult Create()
        {
            //do we need to process anything here the first time the user requests the create-an-item page?


            //make a call to get a list of categories
            var categories = categoriesService.GetCategories();

            //find a way how to pass those categories into the View
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = categories;

            //OR
            //ViewBag.Categories = categories;


            return View(myModel);
        }

        [HttpPost] //Post method is called after the end user fills in the data and presses Submit button
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




            return RedirectToAction("Create");
        }

        public IActionResult List()
        {
         var list = itemsService.ListItems();
            return View(list); //connection to the database is opened only here 
            //to render the text black on white on the page
        }

        public IActionResult Details(int id)
        {
            var myFoundItem = itemsService.GetItem(id);
            if (myFoundItem == null)
            {
                //redirecting the user to the List action/view
                //solution 1:
                ViewBag.Error = "Item was not found"; //ViewBag doesn't survive redirections
                var list = itemsService.ListItems();
                return View("List", list);

                //solution 2:
                //return RedirectToAction("List");

            }
            else
                return View(myFoundItem); //this will automatically redirect the user to a View called "Details"
        }

        public IActionResult Search(string keyword)  
        {
            var list = itemsService.Search(keyword);
            return View("List", list); //to locate a page/view bearing the same name of the action

        }
       
    }
}
