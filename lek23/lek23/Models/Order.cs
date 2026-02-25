namespace lek23.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsVipCustomer { get; set; }
    }
}
