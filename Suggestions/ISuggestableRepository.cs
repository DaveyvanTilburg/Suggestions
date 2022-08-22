namespace Suggestions
{
    public interface ISuggestableRepository
    {
        string Key();
        IEnumerable<ISuggestable> Suggestables(int userId);
        void OnUpdateCallback(Action<UpdateCallback> callback);
        void OnDeleteCallback(Action<DeleteCallback> callback);
    }
}