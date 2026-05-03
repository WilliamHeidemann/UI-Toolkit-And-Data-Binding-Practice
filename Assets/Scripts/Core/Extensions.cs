namespace Core
{
    public static class Extensions
    {
        public static bool IsInRange(this int value, int minInclusive, int maxExclusive) => 
            value >= minInclusive && value <= maxExclusive;
    }
}