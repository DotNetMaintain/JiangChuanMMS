using System;

namespace RM.ServiceProvider.Model
{
    public class TPurchasePlanQuery
    {
        public int ID { get; set; }

        public string PurchaseBillCode { get; set; }

        public string PurchaseMan { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Provider { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double Amount { get; set; }

        public bool AuditFlag { get; set; }

        public string DeptName { get; set; }

        public string PayMode { get; set; }
    }
}