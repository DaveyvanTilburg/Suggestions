namespace Suggestions
{
    public class SuggestableRepository
    {
        private readonly List<ISuggestableRepository> _suggesableRepositories;
        private readonly int _maxSuggestions;
        private Dictionary<int, Dictionary<string, List<SuggestableDecorator>>> _suggestables;

        public SuggestableRepository(IEnumerable<ISuggestableRepository> linkRepositories, int maxSuggestions)
        {
            _suggesableRepositories = new List<ISuggestableRepository>(linkRepositories);
            _maxSuggestions = maxSuggestions;
            _suggestables = new Dictionary<int, Dictionary<string, List<SuggestableDecorator>>>();
        }

        public void Register()
        {
            foreach(ISuggestableRepository suggestableRepository in _suggesableRepositories)
            {
                suggestableRepository.OnUpdateCallback(OnUpdate);
                suggestableRepository.OnDeleteCallback(OnDelete);
            }
        }

        private void OnDelete(DeleteCallback callback)
        {
            if(!_suggestables.ContainsKey(callback.UserId))
                return;

            string key = callback.SuggestableRepository.Key();
            SuggestableDecorator? match = _suggestables[callback.UserId][key].FirstOrDefault(s => s.Id() == callback.deletedItemId);

            if(match == null)
                return;

            _suggestables[callback.UserId][key].Remove(match);
        }

        private void OnUpdate(UpdateCallback callback)
        {
            if(!_suggestables.ContainsKey(callback.UserId))
                return;

            string key = callback.SuggestableRepository.Key();
            SuggestableDecorator? match = _suggestables[callback.UserId][key].FirstOrDefault(s => s.Id() == callback.UpdatedItem.Id());

            if(match != null)
                _suggestables[callback.UserId][key].Remove(match);

            _suggestables[callback.UserId][key].Add(new SuggestableDecorator(callback.UpdatedItem));
        }

        public IEnumerable<Suggestion> Suggestions(int userId, string question)
        {
            if(!_suggestables.ContainsKey(userId))
                _suggestables[userId] = Init(userId);

            return _suggestables[userId]
                .SelectMany(s => s.Value)
                .SelectMany(s => s.Suggestions(question))
                .Take(_maxSuggestions);
        }

        private Dictionary<string, List<SuggestableDecorator>> Init(int userId)
        {
            var result = new Dictionary<string, List<SuggestableDecorator>>();

            foreach(ISuggestableRepository repository in _suggesableRepositories)
                result.Add(repository.Key(), repository
                    .Suggestables(userId)
                    .Select(s => new SuggestableDecorator(s))
                    .ToList()
                );

            return result;
        }
    }
}