using MatMan.Data;

namespace MatMan.Application
{

    public class UnitOfWorkBase
    {
        protected readonly IRepository _repository;

        protected UnitOfWorkBase(IRepository repo) =>
            _repository = repo;
    }
}
