namespace Employee.Management.Domain.Entities.ApplicationUser
{
    public class ApplicationUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDateTimeUTC { get; set; } = DateTime.UtcNow;
    }
}

