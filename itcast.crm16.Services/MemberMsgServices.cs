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
    public partial class MemberMsgServices : BaseBLL<MemberMsg>, IMemberMsgServices
    {

        IMemberMsgRepository subBal;
        public MemberMsgServices(IMemberMsgRepository memberMsgBal)
        {
            this.subBal = memberMsgBal;
            base.basedal = memberMsgBal;
        }
    }
}
