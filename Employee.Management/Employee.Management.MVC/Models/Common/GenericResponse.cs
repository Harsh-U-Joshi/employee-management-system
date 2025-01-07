using System.Net;

namespace Employee.Management.MVC.Models.Common
{
    public class GenericResponse<T> where T : class
    {
        public int Code { get; set; } = HttpStatusCode.OK.GetHashCode();
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }
}
