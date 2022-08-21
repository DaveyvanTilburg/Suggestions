namespace Suggestions
{
    public static class StringExtensions
    {
        public static bool IsLike(this string text, string other)
            => text.Contains(other, StringComparison.OrdinalIgnoreCase);
    }
}