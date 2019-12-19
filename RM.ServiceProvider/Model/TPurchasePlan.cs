using System;
using System.Collections.Generic;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Model
{
    [Serializable]
    public class TPurchasePlan
    {
        private List<TPurchasePlanDetail> _detail = new List<TPurchasePlanDetail>();
        public OperateType OprType { get; set; }

        public MMS_PurchasePlanContent Content { get; set; }

        public List<TPurchasePlanDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }

    [Serializable]
    public class TPurchasePlanDetail
    {
        public OperateType OprType { get; set; }
        public MMS_PurchasePlanDetail DetDetail { get; set; }
    }
}