using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;
using Yumify.Service.DTO.Cart;

namespace Yumify.API.Controllers
{
    public class CartsController : BaseAPIController
    {
        private readonly ICartRespository _cart;
        private readonly IMapper _mapper;

        public CartsController(ICartRespository cart, IMapper mapper)
        {
            _cart = cart;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(string id)
        {
            var Response = new GeneralResponse(BadRequest().StatusCode);
            var cart = await _cart.GetCartAsync(id);
            if (cart is not null)
            {
                Response.StatusCode = Ok().StatusCode;
                Response.Message = Response.chooseMessage(Ok().StatusCode);
                Response.Data = cart;

                return Ok(Response);
            }
            Response.Message = Response.chooseMessage(BadRequest().StatusCode);
            Response.Data = "No Data returned";
            return BadRequest(Response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUpdateCartDto>> CreateUpdateCart(CreateUpdateCartDto cartDto)
        {
            var Response = new GeneralResponse(BadRequest().StatusCode);
            var mappedCart = _mapper.Map<Cart>(cartDto);
            var CreatedCart = await _cart.CreateUpdateCartAsync(mappedCart);
            if (CreatedCart is not null)
            {
                Response.StatusCode = Ok().StatusCode;
                Response.Message = Response.chooseMessage(Ok().StatusCode);
                Response.Data = CreatedCart;
                return Ok(Response);
            }
            Response.Message = Response.chooseMessage(BadRequest().StatusCode);
            Response.Data = "Already Exist";
            return BadRequest(Response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCart(string id)
        {
            var Response = new GeneralResponse(BadRequest().StatusCode);
            var deletedCart = await _cart.DeleteCartAsync(id);
            if (deletedCart)
            {
                Response.StatusCode = Ok().StatusCode;
                Response.Message = Response.chooseMessage(Ok().StatusCode);
                Response.Data = "Deleted Successfully";
                return Ok(Response);
            }
            Response.Message = Response.chooseMessage(NotFound().StatusCode);
            Response.Data = "Not Found this cart";
            return NotFound(Response);

        }
    }
}
