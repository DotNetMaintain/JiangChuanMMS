using System;
using System.Collections.Generic;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Model
{
    [Serializable]
    public class TSale
    {
        private List<TSaleDetail> _detail = new List<TSaleDetail>();
        public OperateType OprType { get; set; }

        public MMS_SaleContent Content { get; set; }

        public List<TSaleDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }

    [Serializable]
    public class TSaleDetail
    {
        public OperateType OprType { get; set; }
        public MMS_SaleDetail DetDetail { get; set; }
    }
}