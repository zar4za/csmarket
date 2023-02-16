namespace CsMarket.Steam
{
    public class SteamId
    {
        private const string SteamIdBase = "STEAM_0"; // only individual accounts accepted 
        private const long SteamId64Base = 76561197960265728;

        private readonly int _idNumber;
        private readonly int _accountNumber;

        public string SteamIdText => $"{SteamIdBase}:{_idNumber}:{_accountNumber}";

        public int SteamId32 => _accountNumber * 2 + _idNumber;

        public long SteamId64 => SteamId64Base + SteamId32;

        public SteamId(long steamId64)
        {
            if (steamId64 <= SteamId64Base)
            {
                throw new ArgumentException($"SteamID64 must be greater than its base equal {SteamId64Base}.", nameof(steamId64));
            }

            var idBase = (int) (steamId64 - SteamId64Base);

            if (idBase % 2 != 0)
            {
                _idNumber = 1;
                idBase -= 1;
            }

            _accountNumber = idBase / 2;
        }

        public SteamId(int steamId32)
        {
            if (steamId32 < 1)
            {
                throw new ArgumentException("SteamID32 must be greater than 0.", nameof(steamId32));
            }

            if (steamId32 % 2 != 0)
            {
                _idNumber = 1;
                steamId32 -= 1;
            }

            _accountNumber = steamId32 / 2;
        }

        public SteamId(string steamIdText)
        {
            if (!steamIdText.StartsWith(SteamIdBase))
            {
                throw new ArgumentException("SteamID must start with \'STEAM_0\' meaning it is individual account.", nameof(steamIdText));
            }

            var parts = steamIdText.Split(':');

            if (parts.Length != 3)
            {
                throw new ArgumentException("SteamID text representation must be in format \'STEAM_X:Y:Z\'.", nameof(steamIdText));
            }

            var idNumber = int.Parse(parts[1]);
            var accountNumber = int.Parse(parts[2]);

            if (idNumber != 0 && idNumber != 1)
            {
                throw new ArgumentException("Value Y in \'STEAM_X:Y:Z\' must be 0 or 1.", nameof(steamIdText));
            }

            if (accountNumber < 0)
            {
                throw new ArgumentException("Value Z in \'STEAM_X:Y:Z\' must be positive.", nameof(steamIdText));
            }

            _idNumber = idNumber;
            _accountNumber = accountNumber;
        }

        public SteamId(int idNumber, int accountNumber)
        {
            _idNumber = idNumber;
            _accountNumber = accountNumber;
        }
    }
}
