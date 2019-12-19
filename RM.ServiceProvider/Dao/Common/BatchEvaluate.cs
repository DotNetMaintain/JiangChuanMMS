using System;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace RM.ServiceProvider.Dao
{
    public class BatchEvaluate
    {
        private static readonly object thisLock = new object();

        /// <summary>
        ///   通过反射实现对象间的公共属性赋值
        /// </summary>
        /// <param name="source"> 源对象 </param>
        /// <param name="target"> 目标对象 </param>
        public static void Eval(object source, object target)
        {
            //获得源对象所有公共属性信息的数组
            PropertyInfo[] sourcePropertyList = source.GetType().GetProperties();
            //获得目标对象所有公共属性信息的数组
            PropertyInfo[] targetPropertyList = target.GetType().GetProperties();
            //循环目标对象的所有公共属性
            foreach (PropertyInfo targetProp in targetPropertyList)
            {
                //获得目标对象属性的自定义属性
                object[] attrArray = targetProp.GetCustomAttributes(false);
                if (attrArray.Length > 0) //如果目标对象属性有自定义属性
                {
                    //如果自定义属性标识是主键
                    if (((ColumnAttribute) attrArray[0]).IsPrimaryKey)
                    {
                        continue;
                    }
                }
                //根据目标对象属性名找到源对象对应的属性
                PropertyInfo sourceProp = sourcePropertyList.First(itm => itm.Name == targetProp.Name);
                if (sourceProp != null)
                {
                    //获得源对象属性值
                    object value = sourceProp.GetValue(source, null);
                    //将源对象的属性值赋给目标对象对应的属性
                    targetProp.SetValue(target, value, null);
                }
            }
        }

        /// <summary>
        ///   自动生成编号
        /// </summary>
        /// <param name="codeHead"> </param>
        /// <returns> </returns>
        public static string GeneralCode()
        {
            lock (thisLock)
            {
                return DateTime.Now.ToString("yyyyMMddhhmmssfff");
            }
        }
    }
}