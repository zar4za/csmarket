using CsMarket.Data.Entity;

namespace CsMarket.Market
{
    public interface IDescriptionStorage
    {
        Description GetDescription(long instanceid, long classid);

        void AddDescription(Description description);
    }
}
