namespace Suggestions.Tests.Fakes
{
    public static class FakeRepositoryFactory
    {
        public static FakeRepository<Customer> CustomerRepository()
            => new FakeRepository<Customer>(
                nameof(Customer),
                new Dictionary<int, List<Customer>>
                {
                    [1] = new List<Customer>
                    {
                        new Customer(1, "Elisa West"),
                        new Customer(2, "Dolores Vargas"),
                        new Customer(3, "Alicia Gibson"),
                        new Customer(4, "Antoinette Rivera"),
                        new Customer(5, "Amy Vega"),
                        new Customer(6, "Elisa East"),
                    }
                }
            );

        public static FakeRepository<Employee> EmployeeRepository()
            => new FakeRepository<Employee>(
                nameof(Employee),
                new Dictionary<int, List<Employee>>
                {
                    [1] = new List<Employee>
                    {
                        new Employee(1, "testusername", "some@google.com"),
                        new Employee(2, "compiledstory", "other@google.com")
                    }
                }
            );

        public static FakeRepository<Location> LocationRepository()
            => new FakeRepository<Location>(
                nameof(Location),
                new Dictionary<int, List<Location>>
                {
                    [1] = new List<Location>
                    {
                        new Location(1, "Prison", "The Hague", "Netherlands"),
                        new Location(2, "Sport hall", "Rotterdam", "Netherlands"),
                        new Location(3, "Not important", "New york", "America")
                    }
                }
            );
    }
}