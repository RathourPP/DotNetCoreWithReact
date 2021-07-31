using System.ComponentModel.DataAnnotations;

namespace DummyProjectApi.BusinessModel.Model
{
    public class DtLogin
    {
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
    }
}
