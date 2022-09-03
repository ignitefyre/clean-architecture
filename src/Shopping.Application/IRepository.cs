using FluentResults;
using Shopping.Domain;

namespace Shopping.Application;

public interface IRepository<T> where T : AggregateRoot
{
    Result<T> Create();
    Result<T> GetById(string id);
    Result Update(T entity);
}