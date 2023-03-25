namespace CsMarket.Infrastructure
{
    public static class StringUtils
    {
        public static Dictionary<string, string> ParseColonSeparated(string value)
        {
            return value.ReplaceLineEndings("\n")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(claim => claim.Split(':', 2))
                .ToDictionary(claim => claim[0], claim => claim[1]);
        }
    }
}
