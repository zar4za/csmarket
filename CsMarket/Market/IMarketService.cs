using CsMarket.Core;

namespace CsMarket.Market
{
    public interface IMarketService
    {
        void PutOnSale(Item item, decimal price);
    }
}
