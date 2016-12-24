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
    public partial interface ISiteSetService : IBaseBLL<SiteSet>
    {

        IEnumerable<model.ModelViews.SiteSetViewModel> GetItems(int index, int pageSize, int Type,bool IsShow=false);







    }
}
