using CsMarket.Data.Entity;

namespace CsMarket.Data
{
    public interface IDescriptionRepository
    {
        void AddDescription(Description description);

        Description GetDescription(long classId);
    }
}
