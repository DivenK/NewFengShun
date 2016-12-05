using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Common
{
    using System.Web;

    /// <summary>
    /// 负责管理缓存
    /// </summary>
    public class CacheMgr
    {
        /// <summary>
        /// 取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetData<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        /// <summary>
        /// 存入数据只有IIS重启以后才销毁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void SetData(string key, object obj)
        {
            HttpRuntime.Cache[key] = obj;
        }

        /// <summary>
        /// 手工移除指定key的缓存数据
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveData(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}
