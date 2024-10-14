using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Service.DTO.User
{
    public class UpdatePasswordDto
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set;} = null!;
    }
}
