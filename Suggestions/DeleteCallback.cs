namespace Suggestions
{
    public record struct DeleteCallback(
        ISuggestableRepository SuggestableRepository, 
        int UserId, 
        int deletedItemId
    );
}