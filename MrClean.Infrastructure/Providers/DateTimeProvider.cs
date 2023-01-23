using MrClean.Core.Application.Common.Interfaces.Providers;

namespace MrClean.Infrastructure.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
