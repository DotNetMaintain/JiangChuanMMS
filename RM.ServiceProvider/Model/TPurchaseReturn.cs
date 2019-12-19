using System;
using System.Collections.Generic;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Model
{
    [Serializable]
    public class TPurchaseReturn
    {
        private List<TPurchaseReturnDetail> _detail = new List<TPurchaseReturnDetail>();
        public OperateType OprType { get; set; }

        public MMS_PurchaseReturnContent Content { get; set; }

        public List<TPurchaseReturnDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }

    [Serializable]
    public class TPurchaseReturnDetail
    {
        public OperateType OprType { get; set; }
        public MMS_PurchaseReturnDetail DetDetail { get; set; }
    }
}