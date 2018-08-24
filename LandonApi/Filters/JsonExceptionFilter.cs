using LandonApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Filters // filter is a chunk of code that runs before or after asp.net core processes a request
                            // filters that handle errors and exceptions are called exception filters. create a folder called filters
                            // that uses JsonExceptionFilter. it implements IException Filters. it has one method
                            // code that runs anytime there is an exception in the API.
                            // craete an instance of the api error class then send it back to the client
{
    public class JsonExceptionFilter : IExceptionFilter // use the exception filter interface built in asp to handle exceptions
    {
        private readonly IHostingEnvironment _env; // need local variable to hold the environment status that was asked for in the dependency injection
        public JsonExceptionFilter(IHostingEnvironment env) // use dependency injection to request what environment we are in (dev or production?)
        {
            _env = env; // inject the environment in so we can use it in the class

        }
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError(); // ApiError is in the models folder. this creates a new instance

            // we used dependency injection to see what environment we are in. if we are in dev we want more detailed info sent to client
            if (_env.IsDevelopment())
            { 
            // contains actual exceptions object to add some properties to it
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error has occured."; // send generic boring message if in production to customer client
                error.Detail = context.Exception.Message; // with boring message
            }
            // now send response back to client
            context.Result = new ObjectResult(error) /// manually create an object to serialize the error class we created then keep 500 status code
            {
                StatusCode = 500
            };

            // so now we have the exception boxed up, but we dont want to send entire trace if in production mode. to know if we are
            // in production or development, we should use ihostingenvironment helper service.we will use dependency (constructor) injection to
            // ask for this information

            // now we need to go to startup.cs and have mvc use this new filter we just wrote

        }
    }
}
