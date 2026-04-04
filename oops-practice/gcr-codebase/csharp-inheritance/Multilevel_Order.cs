using System;

namespace csharp_inheritance
{
    class Order
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(string id, DateTime date)
        {
            OrderId = id;
            OrderDate = date;
        }

        public virtual string GetOrderStatus()
        {
            return "Order placed";
        }
    }

    class ShippedOrder : Order
    {
        public string TrackingNumber { get; set; }

        public ShippedOrder(string id, DateTime date, string tracking) : base(id, date)
        {
            TrackingNumber = tracking;
        }

        public override string GetOrderStatus()
        {
            return $"Shipped (Tracking: {TrackingNumber})";
        }
    }

    class DeliveredOrder : ShippedOrder
    {
        public DateTime DeliveryDate { get; set; }

        public DeliveredOrder(string id, DateTime date, string tracking, DateTime delivery) : base(id, date, tracking)
        {
            DeliveryDate = delivery;
        }

        public override string GetOrderStatus()
        {
            return $"Delivered on {DeliveryDate:d}";
        }
    }

    class Program
    {
        static void Main()
        {
            Order o1 = new Order("O1", DateTime.Now);
            Order o2 = new ShippedOrder("O2", DateTime.Now.AddDays(-2), "TRK123");
            Order o3 = new DeliveredOrder("O3", DateTime.Now.AddDays(-5), "TRK999", DateTime.Now.AddDays(-1));

            Console.WriteLine(o1.GetOrderStatus());
            Console.WriteLine(o2.GetOrderStatus());
            Console.WriteLine(o3.GetOrderStatus());
        }
    }
}