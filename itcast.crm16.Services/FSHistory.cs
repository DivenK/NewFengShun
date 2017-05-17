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
    using Site.Models;
    using System.Linq.Expressions;

    public partial class FSHistoryService : BaseBLL<FSHistory>, IFSHistoryService
    {
        IFSHistoryRepository dal;
        public FSHistoryService(IFSHistoryRepository dal)
        {
            this.dal = dal;
            base.basedal = dal;
        }

        public IEnumerable<FSHistoryViewModel> GetItemModel(int index, out int totalPage, string name, int pageSize = 10,int type=0,bool IsShow=false)
        {
            List<FSHistory> itemList = null;
            Expression<Func<FSHistory, bool>> where;
            if (string.IsNullOrWhiteSpace(name))
            {
                where =(c=>c.Type==type);
                if (IsShow)
                {
                    where = (c =>c.Display == 0&&c.Type==type);
                }
             
            }
            else
            {
                where = (c => c.Title.Contains(name) && c.Type == type);
                if (IsShow)
                {
                    where = (c => c.Title.Contains(name) && c.Display == 0 &&c.Type==type);
                }
              
            }
            itemList = dal.QueryByPage(index, pageSize, out totalPage, where, c => c.CreaterTime).ToList<FSHistory>();
            int newid = 1;
            if (index > 1)
            {
                newid = (index - 1) * 10 + newid;
            }
            return itemList.Select(d => new FSHistoryViewModel
            {
                Nid = newid++,
                DisplayStr = d.Display == 0 ? true : false,
                Likes = d.Likes,
                id = d.id,
                Contents = d.Contents.Length >10&&IsShow==false? d.Contents.Substring(0, 8) : d.Contents,
                Creater = d.Creater,
                CreaterTime = d.CreaterTime,
                Look = d.Look,
                Title = d.Title,
                type =d.Type,
                Image=d.Image,
                UpdateTime = d.UpdateTime,
                UpdateTimeStr = d.UpdateTime.ToShortDateString(),
            }).ToList();
        }


    }
}
