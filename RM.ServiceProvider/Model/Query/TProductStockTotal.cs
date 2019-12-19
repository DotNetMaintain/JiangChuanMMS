namespace RM.ServiceProvider.Model
{
    public class TProductStockTotal
    {
        /// <summary>
        ///   货品代码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        ///   货品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///   库存数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///   成本单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///   库存金额
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        ///   销售数量
        /// </summary>
        public int SaleQty { get; set; }

        /// <summary>
        ///   销售金额
        /// </summary>
        public double SaleAmount { get; set; }

        /// <summary>
        ///   销售毛利
        /// </summary>
        public double SaleGrossProfit { get; set; }

        /// <summary>
        ///   采购数量
        /// </summary>
        public int PurchaseQty { get; set; }

        /// <summary>
        ///   采购金额
        /// </summary>
        public double PurchaseAmount { get; set; }
    }
}