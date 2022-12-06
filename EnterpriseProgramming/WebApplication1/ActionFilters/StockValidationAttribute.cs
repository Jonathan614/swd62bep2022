using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ActionFilters
{
    public class StockValidationAttribute: ActionFilterAttribute
    {
        //in the home assignment: you need to check the db whether the user who will edit the file has got the permission saved in db

        private ItemsService _itemService;
        


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                string currentlyLoggedUser = context.HttpContext.User.Identity.Name;
                int itemId = Convert.ToInt32(context.ActionArguments.ElementAt(0).Value);

                if(currentlyLoggedUser!="ryanattard@gmail.com") context.Result = new UnauthorizedResult();


                ItemsService myItemsService = (ItemsService)context.HttpContext.RequestServices.GetService(typeof(ItemsService));

                int stock = myItemsService.GetItem(itemId).Stock;
                if (stock > 0)
                    context.Result = new RedirectToActionResult("ErrorMessage", "Home", new { message = "Cannot delete this item because stock > 0" });



            }

        }
    }
}
