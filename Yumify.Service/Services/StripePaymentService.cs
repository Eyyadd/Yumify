using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Yumify.Core.Entities;
using Yumify.Core.Entities.OrdersEntities;
using Yumify.Core.IRepository;
using Yumify.Core.IServices;
using Yumify.Core.Specification.OrderSpec;
using Yumify.Repository.Repositories;
using Product = Yumify.Core.Entities.Product;
namespace Yumify.Service.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private IConfiguration _Configuration;
        private readonly ICartRespository _CartRespository;
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IGenericRepository<DeliveryMethod> _DeliveyMethodRepo;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IGenericRepository<Order> _OrderRepo;

        StripePaymentService(IConfiguration configuration,
            ICartRespository cartRespository,
            IUnitOfWork unitOfWork
            )
        {
            _UnitOfWork = unitOfWork;
            _Configuration = configuration;
            _CartRespository = cartRespository;
            _ProductRepo = _UnitOfWork.Myrepository<Product>();
            _DeliveyMethodRepo = _UnitOfWork.Myrepository<DeliveryMethod>();
            _OrderRepo = _UnitOfWork.Myrepository<Order>();


        }
        public async Task<Cart> CreateOrUpdatePaymentIntent(string CartId)
        {
            StripeConfiguration.ApiKey = _Configuration["Stripe:SecreteKey"];
            PaymentIntentService paymentService = new PaymentIntentService();
            var Cart = await _CartRespository.GetCartAsync(CartId);
            var deliveyMethodCost = 0m;
            if (string.IsNullOrEmpty(Cart.PaymentIntentId))
            {
                if (Cart.Items?.Count > 0)
                {
                    foreach (var item in Cart.Items)
                    {
                        var productById = await _ProductRepo.GetByIdAsync(item.Id);
                        if (item.Price != productById.Price)
                            item.Price = productById.Price;
                    }

                    if (Cart.deliveryMethodId.HasValue)
                    {
                        var DeliveryMethod = await _DeliveyMethodRepo.GetByIdAsync((int)Cart.deliveryMethodId!);
                        deliveyMethodCost = DeliveryMethod.Cost;
                    }
                    //Create 
                    var options = new PaymentIntentCreateOptions()
                    {
                        Amount = (long)Cart.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)deliveyMethodCost * 100,
                        Currency = "usd",
                        PaymentMethodTypes = new List<string>() { "card" }
                    };
                    var PaymentIntent = await paymentService.CreateAsync(options);
                    Cart.PaymentIntentId = PaymentIntent.Id;
                    Cart.ClientSecrete = PaymentIntent.ClientSecret;
                }
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Cart.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)deliveyMethodCost * 100
                };

                await paymentService.UpdateAsync(Cart.PaymentIntentId, options);
            }

            return Cart;
        }

        public async Task<Order?> UpdateOrderStatus(string PAymentIntentId, bool IsPaid)
        {
            var spec = new OrderWithPaymentIntentSpecification(PAymentIntentId);
            var order = await _OrderRepo.GetByIdWithSpecAsync(spec);
            if (order is not null)
            {
                if (IsPaid)
                    order.OrderStatus = OrderStatus.PaymentDone;
                else
                    order.OrderStatus = OrderStatus.PaymentFailed;
                _OrderRepo.Update(order);
                var result = await _UnitOfWork.CompleteAsync();
                if(result > 0)
                {
                    return order;
                }
            }
            return null;
        }
    }
}
