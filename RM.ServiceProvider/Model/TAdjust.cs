using System;
using System.Collections.Generic;
using RM.ServiceProvider.Enum;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Model
{
    [Serializable]
    public class TAdjust
    {
        private List<TAdjustDetail> _detail = new List<TAdjustDetail>();
        public OperateType OprType { get; set; }

        public MMS_AdjustContent Content { get; set; }

        public List<TAdjustDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }

    [Serializable]
    public class TAdjustDetail
    {
        public OperateType OprType { get; set; }
        public MMS_AdjustDetail DetDetail { get; set; }
    }
}