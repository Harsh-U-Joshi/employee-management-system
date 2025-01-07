namespace Employee.Management.Domain.Entities.Common
{
    public class ErrorLog
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
        public string Context { get; set; }
    }
}
