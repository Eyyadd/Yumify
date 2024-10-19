using Yumify.Core.Entities.OrdersEntities;

namespace Yumify.Core.Specification
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(string CustomerEmail) : base(O => O.CustomerMail == CustomerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            SortingDESC = (O => O.OrderDate);
        }
        public OrderSpecification(string CustomerEmail,int orderid) : base(O => O.CustomerMail == CustomerEmail&&O.Id==orderid)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
        
    }
}
