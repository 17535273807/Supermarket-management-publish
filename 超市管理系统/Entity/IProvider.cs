using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 超市管理系统.Entity
{
    public interface IProvider<T> where T : class
    {
        /// <summary>
        /// 获取表的全部数据
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();   

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);


        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(T entity);
        int Save();

    }
}
