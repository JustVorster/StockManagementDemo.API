using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

public class SwaggerAllowAnonymousConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            if (controller.ControllerType.FullName!.Contains("Swashbuckle"))
            {
                foreach (var action in controller.Actions)
                {
                    action.Filters.Add(new AllowAnonymousFilter());
                }
            }
        }
    }
}
