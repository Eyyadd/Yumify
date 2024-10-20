using Microsoft.AspNetCore.Identity;
using Yumify.Core.Entities;
using Yumify.Core.Entities.IdentityEntities;
using Yumify.Core.Entities.OrdersEntities;
using Yumify.Core.IRepository;
using Yumify.Core.IServices;
using Yumify.Core.Specification;
using Yumify.Core.Specification.OrderSpec;

namespace Yumify.Service.Services
{
    public class OrderSerivces : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRespository _cart;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStripePaymentService _PaymentService;
        private IGenericRepository<Product> _prodRepos;
        private IGenericRepository<Order> _OrderRepos;
        private IGenericRepository<DeliveryMethod> _deliveryMethodRepos;

        public OrderSerivces(IUnitOfWork unitOfWork, ICartRespository cart, UserManager<ApplicationUser> userManager,IStripePaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _cart = cart;
            _userManager = userManager;
            _PaymentService = paymentService;
            _prodRepos = _unitOfWork.Myrepository<Product>();
            _OrderRepos = _unitOfWork.Myrepository<Order>();
            _deliveryMethodRepos = _unitOfWork.Myrepository<DeliveryMethod>();
        }

        public async Task<Order> CreateOrderAsync(string CutomerMail, string CartId, int deliveryMethodId, OrderAddress orderAddress)
        {
            // 1.Get Basket From Baskets Repo
            var Cart = await _cart.GetCartAsync(CartId);


            // 2. Get Selected Items at Basket From Products Repo
            var orderitems = new List<OrderItems>();
            if (Cart?.Items.Count > 0)
            {
                foreach (var prod in Cart.Items)
                {
                    var product = await _prodRepos.GetByIdAsync(prod.Id);

                    var ProdcutItem = new ProductItemOrder()
                    {
                        PictreUrl = prod.PictureUrl,
                        ProductName = prod.Name,
                        ProductId = prod.Id
                    };
                    var orderItem = new OrderItems(ProdcutItem, prod.Quantity, product.Price);
                    orderitems.Add(orderItem);
                }
            }

            // 3. Calculate SubTotal
            var subtotal = orderitems.Sum(item => item.Quantity * item.Price);

            // 4. Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _deliveryMethodRepos.GetByIdAsync(deliveryMethodId);

            //Validate the pymentintent is unique for the order 
            var spec = new OrderWithPaymentIntentSpecification(Cart?.PaymentIntentId);
            var existingOrder = await _OrderRepos.GetByIdWithSpecAsync(spec);
            if (existingOrder != null)
            {
                _OrderRepos.Delete(existingOrder);
               await _PaymentService.CreateOrUpdatePaymentIntent(CartId);
            }

            // 5. Create Order
            var order = new Order
            {
                CustomerMail = CutomerMail,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = orderAddress,
                Items = orderitems,
                SubtotalPrice = subtotal,
                PaymentIntentId=Cart?.PaymentIntentId!
            };

            await _OrderRepos.AddAsync(order);

            // 6. Save To Database [TODO]
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>?> GetDeliveryMethodAsync()
        {
            var AllDeliveryMethods = await _deliveryMethodRepos.GetAllAsync();
            return AllDeliveryMethods as IReadOnlyList<DeliveryMethod>;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int OrderId, string CutomerMail)
        {
            var spec = new OrderSpecification(CutomerMail, OrderId);
            var Orders = await _OrderRepos.GetByIdWithSpecAsync(spec);

            return Orders;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string CutomerMail)
        {
            var spec = new OrderSpecification(CutomerMail);
            var Orders =await _OrderRepos.GetAllWithSpecAsync(spec);

            return Orders as IReadOnlyList<Order>;
        }
    }
}
