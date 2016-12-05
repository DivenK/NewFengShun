using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Repository
{
    using System.Data.Entity;
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() : base("name=FS_SiteEntities") { }
    }
}
