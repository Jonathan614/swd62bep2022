using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IWebHostEnvironment hostService;
        public ItemsController (ItemsService _itemsService, CategoriesService _categoriesService, IWebHostEnvironment _host)
        {
            itemsService = _itemsService;
            categoriesService = _categoriesService;
            hostService = _host;
                 
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
        public IActionResult Create(CreateItemViewModel data, IFormFile file)
        {
            //......
            try
            {
                //----------------Image upload------------------------------


                //1. to check whether image has been received successfully 
                if (file != null)
                {

                    //2. generate a unique filename for the image
                    string uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    //3. identify a place (in the wwwroot > Images) where to store the actual physical file
                    //hostService enables you to get the full path where to store the image on the web server
                    string absolutePath = hostService.WebRootPath + @"\Images\" + uniqueFilename;

                    //4. save the actual file
                    using (var stream = System.IO.File.Create(absolutePath)) //this will create an empty stream at location : absolutePath
                    {
                          file.CopyTo(stream); //this will copy the data from the uploaded file (parameter) into  empty stream at absolute Path
                    } //will close all streams

                    //5. form the ImagePath and store assign it in the model above

                    data.ImagePath = "/Images/" + uniqueFilename; //the first / indicates asp.net to start locating the image from the root folder

                }

                //-------------------------------------------------

                //1. implement the create method by
                //2. applying dependency injection and ask for ItemsService
                //3. itemsService.AddNewItem(......)
                itemsService.AddNewItem(data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);


                //ViewBag doesn't survive redirections
              //  TempData["Message"] = "Item added successfully";

                //ViewBag vs Tempdata
                //ViewBag doesnt survive redirections
                //Tempdata survives one redirection

                ViewBag.Message = "Item added successfully";
            }
            catch (Exception ex)
            {
                //ViewBag : a dynamic object, it allows you to declare properties on the fly
                //log the exception
                ViewBag.Error = "There was a problem adding a new item. make sure all the fields are correctly filled";
            }

            // return RedirectToAction("Create");
            var categories = categoriesService.GetCategories();
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = categories;

            return View("Create", myModel);
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

       

        public IActionResult Delete(int id)
        {
            itemsService.DeleteItem(id);
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
           var item = itemsService.GetItem(id);
            
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.CategoryId = item.CategoryId;
            myModel.ImagePath = item.ImagePath;
            myModel.Name = item.Name;
            myModel.Price = item.Price;
            myModel.Stock = item.Stock;
         

            if(item != null)
            {
                myModel.Categories = categoriesService.GetCategories();
                return View(myModel);
            }
            else
            {
                return RedirectToAction("List");
            }
            
        }

        [HttpPost] //this method will be triggered after a Submit button inside a form is clicked
        public IActionResult Edit(int id, CreateItemViewModel data, IFormFile file)
        {
           //to do the comments
            try
            {
                if (file != null)
                {
                    //we get the old path and delete the old image

                    string uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string absolutePath = hostService.WebRootPath + @"\Images\" + uniqueFilename;
                    using (var stream = System.IO.File.Create(absolutePath)) //this will create an empty stream at location : absolutePath
                    {
                        file.CopyTo(stream); 
                    } 
                    data.ImagePath = "/Images/" + uniqueFilename; //the first / indicates asp.net to start locating the image from the root folder
                }
                else
                {
                    //set the original image path if nothing is uploaded
                }
                 
                
                itemsService.UpdateItem(id,data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);

                ViewBag.Message = "Item updated successfully";
            }
            catch (Exception ex)
            {
                //ViewBag : a dynamic object, it allows you to declare properties on the fly
                //log the exception
                ViewBag.Error = "There was a problem updating item. make sure all the fields are correctly filled";
            }

            
            var categories = categoriesService.GetCategories();
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = categories;

            return View("Edit", myModel);
        }

    }
}
