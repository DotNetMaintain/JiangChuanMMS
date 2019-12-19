using System.Collections.Generic;
using RM.ServiceProvider.Model;
using RM.ServiceProvider.Dao;

namespace RM.ServiceProvider.Interface
{
    public interface IDeliveryDetail
    {
        List<MMS_Delivery_Detail> GetAllInfo();

        int InsertInfo(MMS_Delivery_Detail info);

        bool UpdateInfo(MMS_Delivery_Detail info);


        bool DeleteInfo(int id);

        MMS_Delivery_Detail GetInfo(int id);



      
    }
}