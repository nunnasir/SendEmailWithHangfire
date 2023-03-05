namespace SimpleEmailApp.Models
{
    public class OrderConfirmationViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
    }

    public class OrderItemViewModel
    {
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
    }

}
