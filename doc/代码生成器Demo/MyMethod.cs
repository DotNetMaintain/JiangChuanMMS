using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sql
{
    public class MyMethod
    {
        private bool MyAdd;
        private bool MyAddReturnId;

        public bool MyAddReturnId1
        {
            get { return MyAddReturnId; }
            set { MyAddReturnId = value; }
        }
        private bool MyDelete;

        public bool MyDelete1
        {
            get { return MyDelete; }
            set { MyDelete = value; }
        }
        private bool MyChange;

        public bool MyChange1
        {
            get { return MyChange; }
            set { MyChange = value; }
        }
        private bool MySelectAll;

        public bool MySelectAll1
        {
            get { return MySelectAll; }
            set { MySelectAll = value; }
        }
        private bool MySelectById;

        public bool MySelectById1
        {
            get { return MySelectById; }
            set { MySelectById = value; }
        }
        private bool MySelectByWhere;

        public bool MySelectByWhere1
        {
            get { return MySelectByWhere; }
            set { MySelectByWhere = value; }
        }

        public bool MyAdd1
        {
            get { return MyAdd; }
            set { MyAdd = value; }
        }

        
    }
}
