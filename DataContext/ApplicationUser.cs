using Microsoft.AspNetCore.Identity;
using System;

namespace DummyProjectApi.DataContext
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
