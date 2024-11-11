using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace SharedKernel
{
    public class ProblemDetailsBuilder
    {
        private readonly HttpContext _context;
        private readonly ProblemDetails _problemDetails = new ProblemDetails();
        public ProblemDetailsBuilder(HttpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Set defaults properties values to status code, instance, title and traceId
        /// </summary>
        /// <returns></returns>
        public ProblemDetailsBuilder SetDefaults()
        {
            _problemDetails.Status = StatusCodes.Status400BadRequest;
            _problemDetails.Instance = _context.Request.GetEncodedPathAndQuery();
            _problemDetails.Title ??= ReasonPhrases.GetReasonPhrase(_problemDetails.Status.Value);
            _problemDetails.Extensions["traceId"] = _context.TraceIdentifier;

            return this;
        }

        /// <summary>
        /// Set http status code
        /// </summary>
        /// <returns></returns>
        public ProblemDetailsBuilder SetStatusCode(int statusCode)
        {
            _problemDetails.Status = statusCode;
            return this;
        }

        /// <summary>
        /// Set instance
        /// </summary>
        /// <returns></returns>
        public ProblemDetailsBuilder SetInstance(string instance)
        {
            _problemDetails.Instance = instance;
            return this;
        }

        /// <summary>
        /// Set title
        /// </summary>
        /// <returns></returns>
        public ProblemDetailsBuilder SetTitle(string title)
        {
            _problemDetails.Title = title;
            return this;
        }

        /// <summary>
        /// Set detail
        /// </summary>
        /// <returns></returns>
        public ProblemDetailsBuilder SetDetail(string detail)
        {
            _problemDetails.Detail = detail;
            return this;
        }

        /// <summary>
        /// Create custom field Errors base on notifications
        /// </summary>
        /// <returns></returns>
        public ProblemDetailsBuilder WithErrors(IEnumerable<Notification> notifications)
        {
            _problemDetails.Extensions.Add("Errors", CreateErrors(notifications));
            return this;
        }

        /// <summary>
        /// Create custom field Exception with exception message
        /// </summary>
        /// <returns></returns>
        public ProblemDetailsBuilder WithException(Exception exception)
        {
            _problemDetails.Extensions.Add("Exception", exception);
            return this;
        }

        /// <summary>
        /// Return the instance of object
        /// </summary>
        /// <returns></returns>
        public ProblemDetails Build()
        {
            return _problemDetails;
        }

        private IEnumerable<ErrorDetail> CreateErrors(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                yield return new ErrorDetail(notification.Message, notification.Key);
            }
        }
    }
}
