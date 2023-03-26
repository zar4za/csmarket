using CsMarket.Data.Entities;

namespace CsMarket.Data
{
    public interface IAssetRepository
    {
        IQueryable<Asset> GetInventory(int steamId32);
    }
}
