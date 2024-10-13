using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Service.DTO.User
{
    public class RegisterUserDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; }=null!;
        public string ConfirmPassword {  get; set; }=null!;

        public string FirstName { get; set; }=null!;
        public string LastName { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
