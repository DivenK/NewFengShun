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
    }
}
