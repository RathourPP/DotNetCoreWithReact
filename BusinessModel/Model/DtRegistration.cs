using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.BusinessModel.Model
{
    public class DtRegistration
    {
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email Is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact Is Required")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "City Is Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
