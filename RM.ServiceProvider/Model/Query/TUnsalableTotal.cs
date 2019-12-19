namespace RM.ServiceProvider.Model
{
    public class TUnsalableTotal
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
        ///   上限数量
        /// </summary>
        public int MaxQty { get; set; }

        /// <summary>
        ///   调整数量
        /// </summary>
        public int AdjustQty { get; set; }
    }
}