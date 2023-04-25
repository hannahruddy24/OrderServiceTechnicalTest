namespace BusinessLogic.Models
{
    public class OrderDTO
    {
        public OrderDetails Order { get; set; }
        public List<OrderProduct> Products { get; set; }
        public OrderCustomer Customer { get; set; }

    }
}
