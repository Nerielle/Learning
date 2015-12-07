using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Sandbox.ViewModels;
using FilterAttribute = System.Web.Http.Filters.FilterAttribute;

namespace Sandbox.Filters
{
    public class GenerateResultListFilterAttribute : FilterAttribute, IResultFilter
    {
        private readonly Type _destinationType;
        private readonly Type _sourceType;

        public GenerateResultListFilterAttribute(Type sourceType, Type destinationType)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            var resultListGenericType = typeof(ResultList<>).MakeGenericType(_destinationType);
            var srcGenericType = typeof(List<>).MakeGenericType(_sourceType);
            var destGenericType = typeof(List<>).MakeGenericType(_destinationType);

            Mapper.CreateMap(_sourceType, _destinationType);
            var viewModel = Mapper.Map(model, srcGenericType, destGenericType);

            var queryOptions = filterContext.Controller.ViewData.ContainsKey("QueryOptions")
                ? filterContext.Controller.ViewData["QueryOptions"]
                : new QueryOptions();

            var resultList = Activator.CreateInstance(resultListGenericType, viewModel,
                queryOptions);

            filterContext.Controller.ViewData.Model = resultList;
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}
