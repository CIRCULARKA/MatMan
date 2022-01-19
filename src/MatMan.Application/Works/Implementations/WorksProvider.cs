using System;
using System.Collections.Generic;
using MatMan.Domain.Models;
using MatMan.Data;

namespace MatMan.Application.Providers
{
    public class WorksProvider : UnitOfWorkBase, IWorksProvider
    {
        public WorksProvider(IRepository repo) : base(repo) { }

        public Work GetWorkByID(Guid workID) =>
            _repository.GetByPrimaryKey<Work>(workID);

        public IEnumerable<Work> AllWorks =>
            _repository.Get<Work>();
    }
}
