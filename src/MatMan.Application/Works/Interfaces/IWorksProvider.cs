using System;
using System.Collections.Generic;
using MatMan.Domain.Models;

namespace MatMan.Application.Providers
{
    public interface IWorksProvider
    {
        Work GetWorkByID(Guid workID);

        IEnumerable<Work> AllWorks { get; }
    }
}
