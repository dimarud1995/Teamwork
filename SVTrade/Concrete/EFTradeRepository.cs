using System.Linq;
using Ninject;
using SVTrade.Entities;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        [Inject]
        private TradeDBEntities Db { get; set; }

        public IQueryable<User> Users
        {
            get { return Db.User; }

        }
    }
}