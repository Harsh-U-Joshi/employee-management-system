namespace Employee.Management.Application.Exceptions
{
    public class GenericException : Exception
    {
        public int StatusCode { get; set; }

        public GenericException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
