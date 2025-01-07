using System.Net;

namespace Employee.Management.API.Common
{
    public class GenericResponse
    {
        public int Code { get; set; } = HttpStatusCode.OK.GetHashCode();
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; } = new();
    }
}
