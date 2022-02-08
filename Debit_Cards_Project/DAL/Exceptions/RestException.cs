using System.Net;

namespace Debit_Cards_Project.DAL.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; set; }

        public RestException(HttpStatusCode code, object? errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}
