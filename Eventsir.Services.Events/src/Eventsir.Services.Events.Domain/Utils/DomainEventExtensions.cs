using Eventsir.Services.Events.Domain.Events;
using System.Text;

namespace Eventsir.Services.Events.Domain.Utils
{
    public static class DomainEventExtensions
    {
        public static string ToDashCase(this IDomainEvent domainEvent)
        {
            string input = domainEvent.GetType().Name;
            var sb = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
                if (i != 0 && char.IsUpper(input[i]))
                    sb.Append($"-{input[i]}");
                else
                    sb.Append(input[i]);

            return sb.ToString().ToLower();
        }
    }
}
