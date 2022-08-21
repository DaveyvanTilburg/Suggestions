namespace Suggestions
{
    public interface ISuggestableRepository
    {
        IEnumerable<ISuggestable> Suggestables();
        void OnUpdateCallback(Action<ISuggestableRepository, ISuggestable> callback);
        void OnDeleteCallback(Action<ISuggestableRepository, int> callback);
    }
}