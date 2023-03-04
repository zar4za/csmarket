using CsMarket.Core;

namespace CsMarket.Steam.Inventory
{
    public interface IInventoryFactory
    {
        IEnumerable<Item> GetInventory(long steamId64);
    }
}
