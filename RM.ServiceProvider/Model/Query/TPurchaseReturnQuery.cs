using System;

namespace RM.ServiceProvider.Model
{
    public class TPurchaseReturnQuery
    {
        /// <summary>
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///   退货单号
        /// </summary>
        public string PurchaseBillCode { get; set; }

        /// <summary>
        ///   经办人
        /// </summary>
        public string PurchaseMan { get; set; }

        /// <summary>
        ///   经办日期
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        ///   收货单位
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        ///   发货仓库
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
        ///   是否确认
        /// </summary>
        public bool AuditFlag { get; set; }

        /// <summary>
        ///   是否确认
        /// </summary>
        public bool PayMode { get; set; }
    }
}