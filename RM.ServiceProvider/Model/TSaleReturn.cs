using System;
using System.Collections.Generic;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Model
{
    [Serializable]
    public class TSaleReturn
    {
        private List<TSaleReturnDetail> _detail = new List<TSaleReturnDetail>();
        public OperateType OprType { get; set; }

        public MMS_SaleReturnContent Content { get; set; }

        public List<TSaleReturnDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }

    [Serializable]
    public class TSaleReturnDetail
    {
        public OperateType OprType { get; set; }
        public MMS_SaleReturnDetail DetDetail { get; set; }
    }
}