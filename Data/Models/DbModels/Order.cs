namespace Data.Models.DbModels
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public string? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductIds { get; set; }
    }
}
