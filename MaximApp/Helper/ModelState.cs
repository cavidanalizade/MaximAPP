using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MaximApp.Helper
{
    public static class ModelState
    {
        public static void AddToModelState(this ValidationResult result , ModelStateDictionary modelstate)
        {
            foreach (var error in result.Errors)
            {
                modelstate.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
