using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Yumify.API.Helper;
using Yumify.Core.Entities;
using Yumify.Core.Entities.OrdersEntities;
using Yumify.Core.IServices;

namespace Yumify.API.Controllers
{
    public class PaymentController : BaseAPIController
    {
        private IStripePaymentService _PaymentServices;
        private readonly ILogger<PaymentController> _Logger;

        public PaymentController(IStripePaymentService paymentServices, ILogger<PaymentController> logger)
        {
            _PaymentServices = paymentServices;
            _Logger = logger;
        }

        [HttpGet("{CartId}")]
        public async Task<ActionResult<Cart>> CreateOrUpdatePaymentIntent(string CartId)
        {
            var response = new GeneralResponse(BadRequest().StatusCode);
            var Cart = await _PaymentServices.CreateOrUpdatePaymentIntent(CartId);
            if (Cart is not null)
            {
                response.StatusCode = Ok().StatusCode;
                response.Message = "Payment Intent Created Sucessfully";
                response.Data = Cart;
                return Ok(response);
            }
            response.Message = response.chooseMessage(BadRequest().StatusCode);
            return BadRequest(response);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_...";

            var stripeEvent = EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret);
            Order? LogOrder;

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                LogOrder = await _PaymentServices.UpdateOrderStatus(paymentIntent?.Id!, true);
                _Logger.LogInformation("Order is Sucessed {0}", LogOrder?.PaymentIntentId);
                _Logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);


            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                LogOrder = await _PaymentServices.UpdateOrderStatus(paymentIntent?.Id!, false);
                _Logger.LogInformation("Order is Sucessed {0}", LogOrder?.PaymentIntentId);
                _Logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);


            }


            return Ok();


        }
    }
}
