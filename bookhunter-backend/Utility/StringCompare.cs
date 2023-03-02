namespace BookHunter_Backend.Utility
{
    public static class StringCompare
    {
        public static float CompareStrings(string s1, string s2)
        {
            var maxLength = Math.Max(s1.Length, s2.Length);
            var editDistance = ComputeLevenshteinDistance(s1, s2);
            var similarity = (float)(maxLength - editDistance) / maxLength;
            return similarity * 100;
        }

        private static int ComputeLevenshteinDistance(string s1, string s2)
        {
            var distance = new int[s1.Length + 1, s2.Length + 1];

            for (var i = 0; i <= s1.Length; i++)
            {
                distance[i, 0] = i;
            }

            for (var j = 0; j <= s2.Length; j++)
            {
                distance[0, j] = j;
            }

            for (var i = 1; i <= s1.Length; i++)
            {
                for (var j = 1; j <= s2.Length; j++)
                {
                    var cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }
            return distance[s1.Length, s2.Length];
        }
    }
}