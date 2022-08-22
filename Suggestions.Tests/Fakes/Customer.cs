using Suggestions.Attributes;

namespace Suggestions.Tests.Fakes
{
    public class Customer : ISuggestable
    {
        private readonly int _id;

        [Identifier]
        [SuggestableProperty]
        public string FullName { get; }

        public Customer(int id, string fullName)
        {
            _id = id;
            FullName = fullName;
        }

        public int Id()
            => _id;

        public string Url()
            => $"http://some.url/customers/{_id}";
    }
}