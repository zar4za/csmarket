using CsMarket.Core;
using CsMarket.Data;

namespace CsMarket.Steam.Inventory
{
    public class CachedInventoryFactory : IInventoryFactory
    {
        private readonly IInventoryFactory _factory;
        private readonly IAssetRepository _repository;

        public CachedInventoryFactory(IInventoryFactory activeFactory, IAssetRepository repository)
        {
            _factory = activeFactory;
            _repository = repository;
        }

        public IEnumerable<Item> GetInventory(long steamId64)
        {

            throw new NotImplementedException();
        }
    }
}
