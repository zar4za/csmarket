using CsMarket.Steam.Inventory;

namespace CsMarket.Market
{
    public class DictionaryDescriptionStorage : IDescriptionStorage
    {
        private Dictionary<(long, long), Description> _descriptions;

        public DictionaryDescriptionStorage()
        {
            _descriptions = new Dictionary<(long, long), Description>();
        }

        public void AddDescription(Description description)
        {
            _descriptions.Add((description.InstanceId, description.ClassId), description);
        }

        public Description GetDescription(long instanceid, long classid)
        {
            return _descriptions[(instanceid, classid)];
        }
    }
}
