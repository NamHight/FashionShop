using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace FashionShop.Filters;

public class ModelValidationFilter : ActionFilterAttribute
{
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var actionName = context.ActionDescriptor.RouteValues["action"];
            context.Result = new ViewResult
            {
                ViewName = actionName,
                ViewData = new ViewDataDictionary((context.HttpContext.RequestServices.GetService(typeof(IModelMetadataProvider)) as IModelMetadataProvider)!,
                    context.ModelState)
                { 
                    Model = context.ActionArguments.Count > 0 ? context.ActionArguments.Values.FirstOrDefault() : null
                }
            };
        }
    }
    
}