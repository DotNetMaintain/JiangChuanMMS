using System;
using System.Collections.Generic;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Model
{
    [Serializable]
    public class TPurchase
    {
        private List<TPurchaseDetail> _detail = new List<TPurchaseDetail>();
        public OperateType OprType { get; set; }

        public MMS_PurchaseContent Content { get; set; }

        public List<TPurchaseDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }

    [Serializable]
    public class TPurchaseDetail
    {
        public OperateType OprType { get; set; }
        public MMS_PurchaseDetail DetDetail { get; set; }
    }
}