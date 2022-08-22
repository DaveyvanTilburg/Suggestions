namespace Suggestions.Tests.Fakes
{
    public class FakeRepository<T> : ISuggestableRepository where T : ISuggestable
    {
        private readonly string _key;
        private readonly Dictionary<int, List<T>> _items;

        private Action<UpdateCallback>? _callbackUpdate;
        private Action<DeleteCallback>? _callbackDelete;

        public FakeRepository(string key, Dictionary<int, List<T>> items)
        {
            _key = key;
            _items = new Dictionary<int, List<T>>(items);
        }

        public void AddOrUpdate(int userId, T item)
        {
            if(!_items.ContainsKey(userId))
                _items.Add(userId, new List<T>());

            T? found = _items[userId].FirstOrDefault(i => i.Id() == item.Id());
            if(found != null)
                _items[userId].Remove(found);
            
            _items[userId].Add(item);
            _callbackUpdate?.Invoke(new UpdateCallback(this, userId, item));
        }

        public void Delete(int userId, int itemId)
        {
            if(!_items.ContainsKey(userId))
                _items.Add(userId, new List<T>());

            _items[userId].Remove(_items[userId].First(c => c.Id() == itemId));
            _callbackDelete?.Invoke(new DeleteCallback(this, userId, itemId));
        }

        public void OnDeleteCallback(Action<DeleteCallback> callback)
            => _callbackDelete += callback;

        public void OnUpdateCallback(Action<UpdateCallback> callback)
            => _callbackUpdate += callback;

        public IEnumerable<ISuggestable> Suggestables(int userId)
            => (_items.ContainsKey(userId) ? _items[userId] : new List<T>()).Cast<ISuggestable>();

        public string Key()
            => _key;
    }
}