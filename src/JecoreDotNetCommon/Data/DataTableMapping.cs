using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Data
{
    public class DataTableMapping<T>
    {
        /// <summary>
        /// 自动生成list模型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> FillModelList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T model = (T)Activator.CreateInstance(typeof(T));
                //T model = new T();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        propertyInfo.SetValue(model, dr[i], null);
                }

                modelList.Add(model);
            }
            return modelList;
        }
        /// <summary>
        /// 自动生成单个模型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public T FillModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return default(T);
            }
            T model = (T)Activator.CreateInstance(typeof(T));
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        propertyInfo.SetValue(model, dr[i], null);
                }
            }
            return model;
        }
    }
}
