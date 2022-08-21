using Suggestions.Employees;

namespace Suggestions.Tests.Fakes
{
    internal class FakeEmployeeRepository : ISuggestableRepository
    {
        private Action<ISuggestableRepository, ISuggestable>? _callbackUpdate;
        private Action<ISuggestableRepository, int>? _callbackDelete;

        private static List<Employee> _employees = new List<Employee>
        {
            new Employee(1, "testusername", "some@google.com"),
            new Employee(2, "compiledstory", "other@google.com")
        };

        public IEnumerable<ISuggestable> Suggestables()
            => _employees;

        public void Add(Employee employee)
        {
            _employees.Add(employee);
            _callbackUpdate?.Invoke(this, employee);
        }

        public void Delete(int id)
        {
            _employees.Remove(_employees.First(c => c.Id() == id));
            _callbackDelete?.Invoke(this, id);
        }

        public void OnUpdateCallback(Action<ISuggestableRepository, ISuggestable> callback)
            => _callbackUpdate += callback;

        public void OnDeleteCallback(Action<ISuggestableRepository, int> callback)
            => _callbackDelete += callback;
    }
}