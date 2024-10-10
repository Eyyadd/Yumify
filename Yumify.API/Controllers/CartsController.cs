using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.IRepository;

namespace Yumify.API.Controllers
{
    public class CartsController : BaseAPIController
    {
        private readonly ICart _cart;

        public CartsController(ICart cart)
        {
            _cart = cart;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCart(string id)
        {
            var Response = new GeneralResponse(BadRequest().StatusCode);
            var cart = await _cart.GetCart(id);
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
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            var Response = new GeneralResponse(BadRequest().StatusCode);
            var CreatedCart = await _cart.CreateUpdateCart(cart);
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
            var deletedCart = await _cart.DeleteCart(id);
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
