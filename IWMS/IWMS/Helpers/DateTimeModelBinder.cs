using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Helpers
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)

        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            // Try to fetch the value of the argument by name
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var dateStr = valueProviderResult.FirstValue;

            // Here you define your custom parsing logic
            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime date))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "DateTime should be in format 'yyyy-mm-dd' or it is invalid");
                return Task.CompletedTask;
            }
            bindingContext.Result = ModelBindingResult.Success(date);
            return Task.CompletedTask;

        }
    }
}
