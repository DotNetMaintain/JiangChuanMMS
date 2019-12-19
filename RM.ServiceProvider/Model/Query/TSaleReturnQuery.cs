using System;

namespace RM.ServiceProvider.Model
{
    public class TSaleReturnQuery
    {
        /// <summary>
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///   退货单号
        /// </summary>
        public string SaleBillCode { get; set; }

        /// <summary>
        ///   经办员
        /// </summary>
        public string SaleMan { get; set; }

        /// <summary>
        ///   退货日期
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        ///   退货单位
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        ///   收货仓库
        /// </summary>
        public string WareHouse { get; set; }

        /// <summary>
        ///   货品代码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        ///   货品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///   数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///   单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///   金额
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        ///   退货确认
        /// </summary>
        public bool AuditFlag { get; set; }
    }
}