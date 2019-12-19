using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sql
{
    public class ProcedureInfo
    {
        private string row;

        public string Row
        {
            get { return row; }
            set { row = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int length;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private bool isNull;

        public bool IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }

        private bool isIdentity;

        public bool IsIdentity
        {
            get { return isIdentity; }
            set { isIdentity = value; }
        }
    }
}
