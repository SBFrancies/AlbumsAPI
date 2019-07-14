using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlbumsApi.Api.Filters
{
    public class GeneralExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            try
            {
                context.ExceptionHandled = true;
                context.Result =
                    new ObjectResult(context.Exception) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }

            catch
            {

            }
        }
    }
}
