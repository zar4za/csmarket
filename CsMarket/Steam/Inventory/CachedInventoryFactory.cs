using CsMarket.Core;
using CsMarket.Data;
using Mapster;

namespace CsMarket.Steam.Inventory
{
    public class CachedInventoryFactory : IInventoryFactory
    {
        private const int AssetRefreshSeconds = 300;
        
        private readonly IInventoryFactory _factory;
        private readonly MarketContext _marketContext;
        private readonly MarketContext _identityContext;

        public CachedInventoryFactory(SteamInventoryFactory activeFactory, MarketContext mContext, MarketContext iContext)
        {
            _factory = activeFactory;
            _marketContext = mContext;
            _identityContext = iContext;
        }

        public IEnumerable<Item> GetInventory(long steamId64)
        {
            var steamId32 = new SteamIdFormatter(steamId64).ToSteamId32();
            var user = _identityContext.Users
                .Where(x => x.SteamId32 == steamId32)
                .Single();

            var cached = _marketContext.Assets.Where(x => x.Owner.SteamId32 == steamId32);

            if (cached.Any() && !cached.Any(x => DateTime.UtcNow - x.LastUpdate > TimeSpan.FromSeconds(600)))
                return cached.ProjectToType<Item>();

            _marketContext.Database.BeginTransaction();
            _marketContext.Assets.RemoveRange(cached);
            var inventory = _factory.GetInventory(steamId64).Select(x => new Data.Entities.Asset()
            {
                AssetId = x.AssetId,
                Owner = user,
                LastUpdate = DateTime.UtcNow,
                ClassName = new Data.Entities.AssetClass()
                {
                    ClassId = x.ClassId,
                    IconUrl = x.IconUrl,
                    MarketHashName = x.MarketHashName
                }
            });

            _marketContext.Assets.AddRange(inventory);
            _marketContext.Database.CommitTransaction();
            _marketContext.SaveChanges();

            return _marketContext.Assets.Where(x => x.Owner.SteamId32 == steamId32).ProjectToType<Item>();
        }
    }
}
