namespace CsMarket.Steam
{
    public class SteamIdProviderOptions
    {
        public string ReturnTo { get; set; } = string.Empty;

        public string Realm { get; set; } = string.Empty;

        public string BuildRequestQuery()
        {
            var query =
                "?openid.ns=" + Uri.EscapeDataString("http://specs.openid.net/auth/2.0") +
                "&openid.mode=" + Uri.EscapeDataString("checkid_setup") +
                "&openid.claimed_id=" + Uri.EscapeDataString("http://specs.openid.net/auth/2.0/identifier_select") +
                "&openid.identity=" + Uri.EscapeDataString("http://specs.openid.net/auth/2.0/identifier_select") +
                "&openid.return_to=" + Uri.EscapeDataString(ReturnTo) +
                "&openid.realm=" + Uri.EscapeDataString(Realm);

            return query;
        }
    }
}
