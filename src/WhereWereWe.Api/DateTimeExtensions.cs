using System;

namespace WhereWereWe.Api
{
    internal static class DateTimeExtensions
    {
        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            var secondsSinceTheEpochStart = (dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                    .TotalSeconds;

            return (long)Math.Round(secondsSinceTheEpochStart);
        }
    }
}
