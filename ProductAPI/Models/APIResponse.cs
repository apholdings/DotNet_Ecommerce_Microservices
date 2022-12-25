using System.Net;

namespace ProductAPI.Models
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; } = true;
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
