using CsMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CsMarket.Data
{
    public class MarketRepository : IDescriptionRepository
    {
        private readonly MarketContext _context;

        public MarketRepository(MarketContext context)
        {
            _context = context;
        }

        public void AddDescription(Description description)
        {
            _context.Descriptions.Add(description);
            _context.SaveChanges();
        }

        public Description GetDescription(long classId)
        {
            return _context.Descriptions
                .AsNoTracking()
                .Where(x => x.ClassId == classId)
                .Single();
        }
    }
}
