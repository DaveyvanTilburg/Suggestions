using FluentAssertions;
using Suggestions.Customers;
using Suggestions.Tests.Fakes;
using Xunit;

namespace Suggestions.Tests
{
    public class UpdateTests
    {
        [Fact]
        public void BeAbleToSearchNewItem()
        {
            string question = "newItem";

            var customerRepository = new FakeCustomerRepository();
            IEnumerable<ISuggestableRepository> linkRepositories = new List<ISuggestableRepository>
            {
                customerRepository
            };

            var suggestionRepository = new SuggestableRepository(linkRepositories, 10);
            suggestionRepository.Register();

            List<Suggestion> suggestions = suggestionRepository.Suggestions(question).ToList();
            suggestions.Count.Should().Be(0);

            customerRepository.Add(new Customer(7, "newItem"));

            List<Suggestion> newSuggestions = suggestionRepository.Suggestions(question).ToList();
            newSuggestions.Count.Should().Be(1);
        }

        [Fact]
        public void BeUnableToSearchRemovedItem()
        {
            string question = "Alicia";

            var customerRepository = new FakeCustomerRepository();
            IEnumerable<ISuggestableRepository> linkRepositories = new List<ISuggestableRepository>
            {
                customerRepository
            };

            var suggestionRepository = new SuggestableRepository(linkRepositories, 10);
            suggestionRepository.Register();

            List<Suggestion> suggestions = suggestionRepository.Suggestions(question).ToList();
            suggestions.Count.Should().Be(1);

            customerRepository.Delete(3);

            List<Suggestion> newSuggestions = suggestionRepository.Suggestions(question).ToList();
            newSuggestions.Count.Should().Be(0);
        }
    }
}