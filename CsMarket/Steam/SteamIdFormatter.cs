using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CsMarket.Steam
{
    public class SteamIdFormatter
    {
        private const string SteamIdBase = "STEAM_0"; // only individual accounts accepted 
        private const long SteamId64Base = 76561197960265728;

        private readonly Regex _accountIdRegex = new(@"^https?://steamcommunity\.com/openid/id/(7[0-9]{15,25})$", RegexOptions.Compiled);

        private readonly int _endingBit;
        private readonly int _account;

        public SteamIdFormatter(string url)
        {
            var steamId64 = long.Parse(_accountIdRegex.Match(url).Groups[1].Value);
            steamId64 -= SteamId64Base;

            if (steamId64 < 0) 
                throw new ArgumentException($"SteamID64 must be greater than its base equal {SteamId64Base}.", nameof(steamId64));

            _endingBit = (int)steamId64 % 2;
            _account = (int)steamId64 / 2;
        }

        public SteamIdFormatter(long steamId64)
        {
            steamId64 -= SteamId64Base;

            if (steamId64 < 0) 
                throw new ArgumentException($"SteamID64 must be greater than its base equal {SteamId64Base}.", nameof(steamId64));

            _endingBit = (int)steamId64 % 2;
            _account = (int)steamId64 / 2;
        }

        public SteamIdFormatter(int steamId32)
        {
            if (steamId32 < 0) 
                throw new ArgumentException("SteamID32 must be greater than 0.", nameof(steamId32));

            _endingBit = steamId32 % 2;
            _account = steamId32 / 2;
        }

        public int ToSteamId32() => _account * 2 + _endingBit;

        public long ToSteamId64() => ToSteamId32() + SteamId64Base;

        public string ToSteamId32Full() => $"[U:1:{ToSteamId32()}]";

        public string ToSteamId3() => $"{SteamIdBase}:{_endingBit}:{_account}";

    }
}
