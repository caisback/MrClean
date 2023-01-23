using System.Net;

namespace MrClean.Core.Application.Common.Errors
{
    public interface IError
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
