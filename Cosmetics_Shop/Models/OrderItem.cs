namespace Cosmetics_Shop.Models
{
    public class OrderItem
    {
        #region Properties
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        #endregion

        // Constructor
        public OrderItem()
        {

        }

        // Parameterized constructor
        public OrderItem(int id, int orderId, int productId, int quantity)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}