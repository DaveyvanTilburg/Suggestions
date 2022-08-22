using FluentAssertions;
using Suggestions.Attributes;
using Xunit;

namespace Suggestions.Tests
{
    public class PressureTests
    {
        [Theory]
        [InlineData(1000)]
        [InlineData(500)]
        [InlineData(10)]
        [InlineData(3)]
        [InlineData(1)]
        public void OneMillionRecords(int maxSuggestions)
        {
            var items = Enumerable.Range(0, 1000000).Select(i => new FakeItem($"name{i}", $"search{i}"));

            IEnumerable<ISuggestableRepository> linkRepositories = new List<ISuggestableRepository>
            {
                new FakeRepository(items)
            };

            var suggestionRepository = new SuggestableRepository(linkRepositories, maxSuggestions);
            List<Suggestion> suggestions = suggestionRepository.Suggestions(1, "search666").ToList();
            suggestions.Count.Should().Be(maxSuggestions);
        }

        private class FakeItem : ISuggestable
        {
            private readonly int _id;
            
            [Identifier]
            [SuggestableProperty]
            public string Name { get; }

            [SuggestableProperty]
            public string SearchString { get; }

            public FakeItem(string name, string searchString)
            {
                Name = name;
                SearchString = searchString;
            }

            public int Id()
                => _id;

            public string Url()
                => $"Short/{_id}";
        }

        private class FakeRepository : ISuggestableRepository
        {
            private List<FakeItem> _items;

            public FakeRepository(IEnumerable<FakeItem> items)
            {
                _items = new List<FakeItem>(items);
            }

            public string Key()
                => nameof(FakeItem);

            public void OnDeleteCallback(Action<DeleteCallback> callback)
            {
                throw new NotImplementedException();
            }

            public void OnUpdateCallback(Action<UpdateCallback> callback)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ISuggestable> Suggestables(int userId)
                => _items;
        }
    }
}