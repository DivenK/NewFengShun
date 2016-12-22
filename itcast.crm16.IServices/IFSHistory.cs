using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.IServices
{
    using System;
    using System.Collections.Generic;

    using itcast.crm16.model;
    using Site.Models;

    public partial interface IFSHistoryService: IBaseBLL<FSHistory>
    {

        IEnumerable<FSHistoryViewModel> GetItemModel(int index, out int totalPage, string name, int pageSize = 10);







    }
}
