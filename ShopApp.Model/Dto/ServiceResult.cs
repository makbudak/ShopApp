using System.Net;

namespace ShopApp.Model.Dto
{
    public class ServiceResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
