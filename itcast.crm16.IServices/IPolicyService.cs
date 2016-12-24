using itcast.crm16.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.IServices
{
    public interface IPolicyServices : IBaseBLL<Policy>
    {
        /// <summary>
        /// 获取前端显示的政策法规数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        List<Policy> GetPolicyByPage(int pageIndex, int pageSize, out int count, bool isDelete);

        // <summary>
        /// 获取指定id政策法规的详细数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Policy GetPolicyById(int id);
    }
}
