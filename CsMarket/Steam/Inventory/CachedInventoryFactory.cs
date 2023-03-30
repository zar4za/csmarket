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
                return cached.OrderByDescending(x => x.AssetId).ProjectToType<Item>();

            _marketContext.Database.BeginTransaction();

            var result = _factory.GetInventory(steamId64);

            var inventory = result.Select(x => new Data.Entities.Asset()
            {
                AssetId = x.AssetId,
                Owner = user,
                LastUpdate = DateTime.UtcNow,
                WasTraded = false,
                Class = new Data.Entities.AssetClass()
                {
                    ClassId = x.ClassId,
                    IconUrl = x.IconUrl,
                    MarketHashName = x.MarketHashName,
                    Rarity = x.Rarity
                }
            });

            foreach (var asset in cached.Where(x => !inventory.Contains(x)))
            {
                asset.WasTraded = true;
            }

            _marketContext.Assets.UpdateRange(inventory.Where(x => cached.Contains(x)));
            _marketContext.Assets.AddRange(inventory.Where(x => !cached.Contains(x)));
            _marketContext.Database.CommitTransaction();
            _marketContext.SaveChanges();

            return _marketContext.Assets
                .Where(x => x.Owner.SteamId32 == steamId32)
                .OrderByDescending(x => x.AssetId)
                .ProjectToType<Item>();
        }
    }
}
