using Customer.Domain.SeedWork;
using DomainEntity = Customer.Domain.Entity;
using Customer.Domain.SeedWork.SearchableRepository;

namespace Customer.Domain.Repository;

public interface ICustomerRepository
    : IGenericRepository<DomainEntity.Customer>,
        ISearchableRepository<DomainEntity.Customer>
{
}