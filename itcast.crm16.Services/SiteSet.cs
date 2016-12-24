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
    using model.ModelViews;

    public partial class SiteSetService : BaseBLL<SiteSet>, ISiteSetService
    {
        ISiteSetRepository dal;
        public SiteSetService(ISiteSetRepository dal)
        {
            this.dal = dal;
            base.basedal = dal;
        }

        public IEnumerable<SiteSetViewModel> GetItems(int index, int pageSize, int Type,bool IsShow=false)
        {
            int count = 0;
            var indexID = 1;
            if (index > 1)
            {
                indexID = (index - 1 * 10) + indexID;
            }
           var list=  dal.QueryByPage(index, pageSize, out count, c => true, c => c.id);
            if (IsShow)
            {
                list = dal.QueryByPage(index, pageSize, out count, c => true&&c.Look==0, c => c.id);
            }
            return list.Select(p => new SiteSetViewModel()
            {
                NId = indexID++,
                TimeStr = p.CreatTime.ToString(),
                type = p.type,
                Contents = p.Contents,
                Image = p.Image,
                LookStr = p.Look == 0 ? true : false,
                Title=p.Title,
                id=p.id,
                Creator=p.Creator,
                Remark=p.Remark,
                Look=p.Look
            }).ToList();
        }
    }
}
