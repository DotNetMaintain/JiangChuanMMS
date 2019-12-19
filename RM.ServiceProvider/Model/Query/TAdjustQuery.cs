using System;

namespace RM.ServiceProvider.Model
{
    /// <summary>
    ///   调拨单查询实体
    /// </summary>
    public class TAdjustQuery
    {
        /// <summary>
        ///   ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///   调拨单号
        /// </summary>
        public string AdjustBillCode { get; set; }

        /// <summary>
        ///   调拨人
        /// </summary>
        public string AdjustMan { get; set; }

        /// <summary>
        ///   调拨日期
        /// </summary>
        public DateTime AdjustDate { get; set; }

        /// <summary>
        ///   源仓库
        /// </summary>
        public string SourceWareHouse { get; set; }

        /// <summary>
        ///   目标仓库
        /// </summary>
        public string TargetWareHouse { get; set; }

        /// <summary>
        ///   摘要
        /// </summary>
        public string Memo { get; set; }

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
        ///   调拨确认
        /// </summary>
        public bool AuditFlag { get; set; }
    }
}