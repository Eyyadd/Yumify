using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;
using Yumify.Core.Entities.IdentityEntities;
using Yumify.Core.IServices;
using Yumify.Service.DTO.User;

namespace Yumify.API.Controllers
{

    public class AccountController : BaseAPIController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthSerivce _authServices;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthSerivce auth)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authServices = auth;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserDto loginUserDto)
        {
            var Response = new GeneralResponse(NotFound().StatusCode,"Not valid login");

            var VaildUser = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (VaildUser is not null)
            {
                var VaildPassword = await _signInManager.CheckPasswordSignInAsync(VaildUser, loginUserDto.Password,false);
                if (VaildPassword.Succeeded)
                {
                    Response.StatusCode = Ok().StatusCode;
                    Response.Message = "User Returned Successfully";
                    Response.Data = new UserDto()
                    {
                        DisplayName = VaildUser.DisplayName,
                        Email = VaildUser.Email!,
                        Token = await _authServices.CreateToken(VaildUser)
                    };
                    return Ok(Response);
                }
            }

            return Ok(Response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerDto)
        {
            var Response = new GeneralResponse(NotFound().StatusCode, "Not valid login");
            var checkUserExist = await _userManager.FindByEmailAsync(registerDto.Email);

            if(checkUserExist is null)
            {
                var user = new ApplicationUser()
                {
                    DisplayName = registerDto.FirstName + " " + registerDto.LastName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email.Split("@")[0]+Guid.NewGuid(),
                };
                var RegisteredUser = await _userManager.CreateAsync(user,registerDto.Password);

                if(RegisteredUser is not null)
                {
                    Response.StatusCode = Ok().StatusCode;
                    Response.Message = "User Created Successfully";
                    Response.Data = new UserDto()
                    {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = await _authServices.CreateToken(user)
                    };

                    return Ok(Response);
                }
            }

            return Ok(Response);
        }
    }
}
