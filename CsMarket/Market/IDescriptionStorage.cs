using CsMarket.Steam.Inventory;

namespace CsMarket.Market
{
    public interface IDescriptionStorage
    {
        Description GetDescription(long instanceid, long classid);

        void AddDescription(Description description);
    }
}
