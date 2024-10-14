using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yumify.API.Helper;
using Yumify.Core.Entities.IdentityEntities;
using Yumify.Core.IServices;
using Yumify.Service.DTO.User;
using Yumify.Service.Helper.Extensions;

namespace Yumify.API.Controllers
{

    public class AccountController : BaseAPIController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthSerivce _authServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthSerivce auth, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authServices = auth;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserDto loginUserDto)
        {
            var Response = new GeneralResponse(NotFound().StatusCode, "Not valid login");

            var VaildUser = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (VaildUser is not null)
            {
                var VaildPassword = await _signInManager.CheckPasswordSignInAsync(VaildUser, loginUserDto.Password, false);
                if (VaildPassword.Succeeded)
                {
                    var MyToken = await _authServices.CreateToken(VaildUser);
                    Response.StatusCode = Ok().StatusCode;
                    Response.Message = "User Returned Successfully";
                    Response.Data = new UserDto()
                    {
                        DisplayName = VaildUser.DisplayName,
                        Email = VaildUser.Email!,
                        Token = MyToken
                    };
                    return Ok(Response);
                }
            }

            return BadRequest(Response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerDto)
        {
            var Response = new GeneralResponse(NotFound().StatusCode, "Not valid login");
            var checkUserExist = await _userManager.FindByEmailAsync(registerDto.Email);

            if (checkUserExist is null)
            {
                var mappedAddress = _mapper.Map<Address>(registerDto.Address);
                mappedAddress.FirstName = registerDto.FirstName;
                mappedAddress.LastName = registerDto.LastName;
                var user = new ApplicationUser()
                {
                    DisplayName = registerDto.FirstName + " " + registerDto.LastName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email.Split("@")[0] + Guid.NewGuid(),
                    Address = mappedAddress
                };
                var RegisteredUser = await _userManager.CreateAsync(user, registerDto.Password);


                if (RegisteredUser.Succeeded)
                {
                    var MyToken = await _authServices.CreateToken(user);
                    Response.StatusCode = Ok().StatusCode;
                    Response.Message = "User Created Successfully";
                    Response.Data = new UserDto()
                    {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = MyToken
                    };

                    return Ok(Response);
                }
            }

            return BadRequest(Response);
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> CurrentUserAsync()
        {

            var Response = new GeneralResponse(NotFound().StatusCode, "not found this user");
            var CheckMail = User.FindFirstValue(ClaimTypes.Email);

            if (CheckMail is not null)
            {
                var CurrentUser = await _userManager.GetUserAddressByEmail(CheckMail);
                if (CurrentUser is not null)
                {
                    var mappedUser = _mapper.Map<UserDto>(CurrentUser);
                    Response.StatusCode = Ok().StatusCode;
                    Response.Message = Response.chooseMessage(200);
                    Response.Data = mappedUser;
                    return Ok(Response);
                }

            }
            return BadRequest(Response);
        }

        [HttpGet("GetUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> CurrentUserAddressAsync()
        {
            var Response = new GeneralResponse(NotFound().StatusCode, "not found this user");
            var mail = User.FindFirstValue(ClaimTypes.Email);
            if (mail is not null)
            {
                var CurrentUser = await _userManager.GetUserAddressByEmail(mail);
                if (CurrentUser is not null)
                {
                    var mappedAddress = _mapper.Map<AddressDto>(CurrentUser.Address);
                    Response.StatusCode = Ok().StatusCode;
                    Response.Message = Response.chooseMessage(200);
                    Response.Data = mappedAddress;

                    return Ok(Response);
                }
            }

            return BadRequest(Response);
        }


        [HttpPut("UpdateAddress")]
        [Authorize]
        public async Task<ActionResult<UpdateUserAddress>> UpdateAddress(UpdateUserAddress newAddress)
        {
            var mappedAddres = _mapper.Map<Address>(newAddress);
            var mail = User.FindFirstValue(ClaimTypes.Email);
            var Response = new GeneralResponse(NotFound().StatusCode, "not found this user");

            if (mail is not null)
            {
                var CurrentUser = await _userManager.GetUserAddressByEmail(mail);
                if (CurrentUser is not null)
                {
                    mappedAddres.Id = CurrentUser.Address.Id;
                    mappedAddres.FirstName=CurrentUser.DisplayName.Split(' ')[0];
                    mappedAddres.LastName=CurrentUser.DisplayName.Split(' ')[1];
                    mappedAddres.ApplicationUserId = CurrentUser.Id;
                    CurrentUser.Address = mappedAddres;
                    var result = await _userManager.UpdateAsync(CurrentUser);
                    if (result.Succeeded)
                    {
                        Response.StatusCode = Ok().StatusCode;
                        Response.Message = "Address Updated Successfully";
                        Response.Data = newAddress;

                        return Ok(Response);
                    }
                }
            }

            return BadRequest(Response);
        }
    }
}
