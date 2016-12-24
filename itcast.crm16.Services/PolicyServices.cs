using itcast.crm16.IRepository;
using itcast.crm16.IServices;
using itcast.crm16.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Services
{
    public class PolicyServices : BaseBLL<Policy>, IPolicyServices
    {
       private  IPolicyRepository dal;

        public PolicyServices(IPolicyRepository policyDal)
        {
            base.basedal = policyDal;
            this.dal = policyDal;
        }

        /// <summary>
        /// 获取前端显示的政策法规数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public List<Policy> GetPolicyByPage(int pageIndex,int pageSize,out int count,bool isDelete)
        {
            return this.dal.QueryByPage(pageIndex,pageSize,out count,c=>c.IsDelete==false&&c.IsComment,c=>c.id);

        }


        /// <summary>
        /// 获取指定id政策法规的详细数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Policy GetPolicyById(int id)
        {
            List<Policy> pList = this.dal.QueryWhere(c =>c.id==id&& c.IsDelete == false && c.IsComment == true);
            if(pList!=null)
            {
                return pList[0];
            }
            return null;
        }

    }
}
