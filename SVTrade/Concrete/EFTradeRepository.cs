using Ninject;
using SVTrade.Abstract;
using SVTrade.Models;

namespace SVTrade.Concrete
{
    public partial class EFTradeRepository : IRepository
    {
        [Inject]
        private TradeDBEntities _db = new TradeDBEntities();

    }
}