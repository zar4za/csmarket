using CsMarket.Core;
using CsMarket.Data;

namespace CsMarket.Steam.Inventory
{
    public class CachedInventoryFactory : IInventoryFactory
    {
        private readonly IInventoryFactory _factory;

        public CachedInventoryFactory(IInventoryFactory activeFactory)
        {
            _factory = activeFactory;
        }

        public IEnumerable<Item> GetInventory(long steamId64)
        {

            throw new NotImplementedException();
        }
    }
}
