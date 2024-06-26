using FluentResults;
using Shopping.Domain;

namespace Shopping.Application;

public interface IRepository<T> where T : AggregateRoot
{
    Task<Result<T>> Create(string ownerId);
    Task<Result<T>> GetById(string id);
    Task<Result> Update(T entity);
}