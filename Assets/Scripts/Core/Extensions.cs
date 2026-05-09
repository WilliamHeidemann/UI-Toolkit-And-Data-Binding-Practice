using UnityEngine.UIElements;

namespace Core
{
    public static class Extensions
    {
        /// <returns>Returns true of the value is in the range [minInclusive,maxExclusive[</returns>
        public static bool IsInRange(this int value, int minInclusive, int maxExclusive) =>
            value >= minInclusive && value <= maxExclusive;

        /// <returns>Returns true of the value is in the range [0,maxExclusive[</returns>
        public static bool IsInRange(this int value, int maxExclusive) => value.IsInRange(0, maxExclusive);

        public static void Toggle(this VisualElement element)
        {
            element.style.display = element.style.display == DisplayStyle.None 
                ? DisplayStyle.Flex 
                : DisplayStyle.None;
        }

        public static bool IsDisplayFlex(this VisualElement element)
        {
            return element.style.display == DisplayStyle.Flex;
        }
    }
}