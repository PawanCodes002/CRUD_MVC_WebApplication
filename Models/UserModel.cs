using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC_WebApplication.Models
{
    public class UserModel
    {
        [Display(Name = "EmailId")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "UserType")]
        public string UserType { get; set; }
    }
}