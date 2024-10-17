using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yumify.API.Helper;
using Yumify.Core.Entities.IdentityEntities;
using Yumify.Core.Entities.OrdersEntities;
using Yumify.Core.IServices;
using Yumify.Service.DTO.Order;

namespace Yumify.API.Controllers
{
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderServices _orderServices;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMapper _Mapper;

        public OrdersController(IOrderServices orderServices, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _orderServices = orderServices;
            _UserManager = userManager;
            _Mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrder createOrderDto)
        {
            var Response = new GeneralResponse(BadRequest().StatusCode);
            var cutomerMail = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var mappedAddress = _Mapper.Map<OrderAddress>(createOrderDto.orderAddress);

            var CreatedOrder = await _orderServices.CreateOrderAsync(cutomerMail, createOrderDto.cartId, createOrderDto.deliveryMethodId, mappedAddress);
            if (CreatedOrder is not null)
            {
                Response.StatusCode=Ok().StatusCode;
                Response.Message = "Order Created Sucessfully";
                Response.Data = CreatedOrder;

                return Ok(Response);
            }
            Response.Message=Response.chooseMessage(BadRequest().StatusCode);
            return BadRequest(Response);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var Response = new GeneralResponse(BadRequest().StatusCode);
            var deliveryMethods = await _orderServices.GetDeliveryMethodAsync();
            if (deliveryMethods is not null)
            {
                Response.StatusCode = Ok().StatusCode;
                Response.Message = "Delivery returned Sucessfully";
                Response.Data = deliveryMethods;
                return Ok(Response);
            }
            Response.Message = Response.chooseMessage(BadRequest().StatusCode);
            return BadRequest(Response);
        }
    }
}
