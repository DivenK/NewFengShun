using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Services
{
    using System;
    using System.Collections.Generic;

    using itcast.crm16.IServices;
    using itcast.crm16.model;
    using itcast.crm16.IRepository;
    public partial class FSHistoryService : BaseBLL<FSHistory>, IFSHistoryService
    {
        IFSHistoryRepository dal;
        public FSHistoryService(IFSHistoryRepository dal)
        {
            this.dal = dal;
            base.basedal = dal;
        }




    }
}
