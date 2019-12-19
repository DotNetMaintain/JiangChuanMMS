using System;
using System.Collections.Generic;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Model
{
    [Serializable]
    public class TPurchaseIndent
    {
        private List<TPurchaseIndentDetail> _detail = new List<TPurchaseIndentDetail>();
        public OperateType OprType { get; set; }

        public MMS_PurchaseIndentContent Content { get; set; }

        public List<TPurchaseIndentDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }

    [Serializable]
    public class TPurchaseIndentDetail
    {
        public OperateType OprType { get; set; }
        public MMS_PurchaseIndentDetail DetDetail { get; set; }
    }
}