namespace Suggestions.Locations
{
    public class Location : ISuggestable
    {
        private readonly int _id;

        [Identifier]
        [SuggestableProperty]
        public string Name { get; }

        [SuggestableProperty]
        public string City { get; }

        [SuggestableProperty]
        public string Country { get; }

        public Location(int id, string name, string city, string country)
        {
            _id = id;
            Name = name;
            City = city;
            Country = country;
        }

        public int Id()
            => _id;

        public string Url()
            => $"http://random.url/location/{_id}";
    }
}