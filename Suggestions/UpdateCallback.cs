namespace Suggestions
{
    public record struct UpdateCallback(
        ISuggestableRepository SuggestableRepository, 
        int UserId, 
        ISuggestable UpdatedItem
    );
}