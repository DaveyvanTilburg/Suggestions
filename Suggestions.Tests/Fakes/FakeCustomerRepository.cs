using Suggestions.Customers;

namespace Suggestions.Tests.Fakes
{
    public class FakeCustomerRepository : ISuggestableRepository
    {
        private Action<ISuggestableRepository, ISuggestable>? _callbackUpdate;
        private Action<ISuggestableRepository, int>? _callbackDelete;

        private readonly static List<Customer> _customers = new List<Customer>
        {
            new Customer(1, "Elisa West"),
            new Customer(2, "Dolores Vargas"),
            new Customer(3, "Alicia Gibson"),
            new Customer(4, "Antoinette Rivera"),
            new Customer(5, "Amy Vega"),
            new Customer(6, "Elisa East"),
        };

        public IEnumerable<ISuggestable> Suggestables()
            => _customers;

        public void Add(Customer customer)
        {
            _customers.Add(customer);
            _callbackUpdate?.Invoke(this, customer);
        }

        public void Delete(int id)
        {
            _customers.Remove(_customers.First(c => c.Id() == id));
            _callbackDelete?.Invoke(this, id);
        }

        public void OnUpdateCallback(Action<ISuggestableRepository, ISuggestable> callback)
            => _callbackUpdate += callback;

        public void OnDeleteCallback(Action<ISuggestableRepository, int> callback)
            => _callbackDelete += callback;
    }
}