using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Service.DTO.User
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

        public string Token { get; set; }=null!;
    }
}
