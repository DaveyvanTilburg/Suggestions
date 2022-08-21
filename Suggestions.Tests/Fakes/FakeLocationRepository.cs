using Suggestions.Locations;

namespace Suggestions.Tests.Fakes
{
    public class FakeLocationRepository : ISuggestableRepository
    {
        private Action<ISuggestableRepository, ISuggestable>? _callbackUpdate;
        private Action<ISuggestableRepository, int>? _callbackDelete;

        private static List<Location> _locations = new List<Location>
        {
            new Location(1, "Prison", "The Hague", "Netherlands"),
            new Location(2, "Sport hall", "Rotterdam", "Netherlands"),
            new Location(3, "Not important", "New york", "America")
        };

        public IEnumerable<ISuggestable> Suggestables()
            => _locations;

        public void Add(Location location)
        {
            _locations.Add(location);
            _callbackUpdate?.Invoke(this, location);
        }

        public void Delete(int id)
        {
            _locations.Remove(_locations.First(c => c.Id() == id));
            _callbackDelete?.Invoke(this, id);
        }

        public void OnUpdateCallback(Action<ISuggestableRepository, ISuggestable> callback)
            => _callbackUpdate += callback;

        public void OnDeleteCallback(Action<ISuggestableRepository, int> callback)
            => _callbackDelete += callback;
    }
}