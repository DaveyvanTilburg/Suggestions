using FluentAssertions;
using Suggestions.Tests.Fakes;
using Xunit;

namespace Suggestions.Tests
{
    public class SuggestionTests
    {
        [Fact]
        public void CustomerTest() 
        {
            Test("Elisa", new[] 
            { 
                new Suggestion("Elisa West", "Elisa West", "Customer", "http://some.url/customers/1"),
                new Suggestion("Elisa East", "Elisa East", "Customer", "http://some.url/customers/6")
            });
        }

        [Fact]
        public void EmployeeTest()
        {
            Test("GoOgLe", new[] 
            { 
                new Suggestion("some@google.com", "testusername", "Employee", "http://other.url/employee/1"),
                new Suggestion("other@google.com", "compiledstory", "Employee", "http://other.url/employee/2")
            });
        }

        [Fact]
        public void LocationTest()
        {
            Test("rot", new[] 
            { 
                new Suggestion("Rotterdam", "Sport hall", "Location", "http://random.url/location/2")
            });
        }

        private void Test(string question, Suggestion[] expectedSuggestions)
        {
            IEnumerable<ISuggestableRepository> linkRepositories = new List<ISuggestableRepository>
            {
                new FakeCustomerRepository(),
                new FakeEmployeeRepository(),
                new FakeLocationRepository()
            };

            var suggestionRepository = new SuggestableRepository(linkRepositories, 10);
            List<Suggestion> suggestions = suggestionRepository.Suggestions(question).ToList();
            suggestions.Should().BeEquivalentTo(expectedSuggestions);
        }
    }
}