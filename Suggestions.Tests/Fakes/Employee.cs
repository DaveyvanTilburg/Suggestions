using Suggestions.Attributes;

namespace Suggestions.Tests.Fakes
{
    public class Employee : ISuggestable
    {
        private readonly int _id;

        [Identifier]
        [SuggestableProperty]
        public string UserName { get; }
        [SuggestableProperty]
        public string EmailAddress { get; }

        public Employee(int id, string userName, string emailAddress)
        {
            _id = id;
            UserName = userName;
            EmailAddress = emailAddress;
        }

        public int Id()
            => _id;

        public string Url()
            => $"http://other.url/employee/{_id}";
    }
}