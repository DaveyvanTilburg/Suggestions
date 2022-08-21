namespace Suggestions
{
    public class SuggestableRepository
    {
        private readonly List<ISuggestableRepository> _suggesableRepositories;
        private readonly int _maxSuggestions;
        private Dictionary<string, List<SuggestableDecorator>>? _suggestables;

        public SuggestableRepository(IEnumerable<ISuggestableRepository> linkRepositories, int maxSuggestions)
        {
            _suggesableRepositories = new List<ISuggestableRepository>(linkRepositories);
            _maxSuggestions = maxSuggestions;
        }

        public void Register()
        {
            foreach(ISuggestableRepository suggestableRepository in _suggesableRepositories)
            {
                suggestableRepository.OnUpdateCallback(OnUpdate);
                suggestableRepository.OnDeleteCallback(OnDelete);
            }
        }

        private void OnDelete(ISuggestableRepository suggestableRepository, int id)
        {
            if(_suggestables == null)
                return;

            string typeName = suggestableRepository.GetType().Name;
            SuggestableDecorator? match = _suggestables[typeName].FirstOrDefault(s => s.Id() == id);

            if(match == null)
                return;

            _suggestables[typeName].Remove(match);
        }

        private void OnUpdate(ISuggestableRepository suggestableRepository, ISuggestable updatedItem)
        {
            if(_suggestables == null)
                return;

            string typeName = suggestableRepository.GetType().Name;
            SuggestableDecorator? match = _suggestables[typeName].FirstOrDefault(s => s.Id() == updatedItem.Id());

            if(match != null)
                _suggestables[typeName].Remove(match);

            _suggestables[typeName].Add(new SuggestableDecorator(updatedItem));
        }

        public IEnumerable<Suggestion> Suggestions(string question)
        {
            _suggestables ??= Init();

            return _suggestables
                .SelectMany(s => s.Value)
                .SelectMany(s => s.Suggestions(question))
                .Take(_maxSuggestions);
        }

        private Dictionary<string, List<SuggestableDecorator>> Init()
        {
            var result = new Dictionary<string, List<SuggestableDecorator>>();

            foreach(ISuggestableRepository repository in _suggesableRepositories)
                result.Add(repository.GetType().Name, repository
                    .Suggestables()
                    .Select(s => new SuggestableDecorator(s))
                    .ToList()
                );

            return result;
        }
    }
}