using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrClean.Core.Application.Common.Interfaces.Providers
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
