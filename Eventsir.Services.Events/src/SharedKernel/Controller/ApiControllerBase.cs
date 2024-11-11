using Microsoft.AspNetCore.Mvc;
using SharedKernel.Controller;
using System.Net;

namespace SharedKernel
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public IActionResult SuccessResponse(object response, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Response.StatusCode = (int)statusCode;
            Response.ContentType = MediaTypes.APPLICATON_JSON;

            return new JsonResult(response);
        }
    }
}
