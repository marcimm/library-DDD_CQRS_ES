using Microsoft.AspNetCore.Mvc;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter.CustomExeception;
using System;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        // send RFC 7807 compliant payload 
        public IActionResult Error() => Problem();


        [HttpGet]
        [Route("customError")]
        // Custom exception error message
        public IActionResult ErrorCustom()
        {
            return BadRequest(new
            {
                success = false,
                errors = new string[] { "Erro", "Um Erro inesperado aconteceu!" }
            });
        }


        [HttpGet]
        [Route("throw-exception")]
        public string ErrorTest(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new HttpResponseException();
            }
            else
            {
                return "Error Controller";
            }
        }

    }
}
