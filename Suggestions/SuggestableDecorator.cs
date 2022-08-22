using System.Reflection;
using Suggestions.Attributes;

namespace Suggestions
{
    public class SuggestableDecorator
    {
        private readonly ISuggestable _wrapped;

        public SuggestableDecorator(ISuggestable wrapped)
        {
            _wrapped = wrapped;
        }

        public int Id()
            => _wrapped.Id();

        public IEnumerable<Suggestion> Suggestions(string question)
        {
            return _wrapped
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttributes(typeof(SuggestablePropertyAttribute), false).Any())
                .Where(p => ((string)(p.GetValue(_wrapped) ?? string.Empty)).IsLike(question))
                .Select(p => 
                    new Suggestion(
                        (string)(p.GetValue(_wrapped) ?? string.Empty),
                        (string)(_wrapped.GetType().GetProperties().First(p => p.GetCustomAttributes(typeof(IdentifierAttribute), false).Any()).GetValue(_wrapped) ?? string.Empty),
                        _wrapped.GetType().Name,
                        _wrapped.Url())
                    );
        }
    }
}