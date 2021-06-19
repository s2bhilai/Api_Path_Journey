using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public class AuthorEntityBinder : IModelBinder
    {
        private IMapper _mapper;
        private ICourseLibraryRepository _repository;

        public AuthorEntityBinder(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = courseLibraryRepository;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;

            //Try to fetch value of argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            //Check if the argument value is null or empty
            if(string.IsNullOrWhiteSpace(value))
            {
                return Task.CompletedTask;
            }

            //if(!int.TryParse(value, out var id))
            //{
            //    //Non integer args
            //    bindingContext.ModelState.TryAddModelError(modelName, "Author Id must be integer");
            //    return Task.CompletedTask;
            //}

            var authorModel = _repository.GetAuthor(Guid.Parse(value));

            var authordto_mb = _mapper.Map<AuthorDto_ModelBinding>(authorModel);

            bindingContext.Result = ModelBindingResult.Success(authordto_mb);

            return Task.CompletedTask;

        }
    }
}
