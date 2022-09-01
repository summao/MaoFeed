using System.Linq.Expressions;
using MaoFeed.Entities;

namespace MaoFeed.Repositories;

public interface IMongoDbRepository<T> where T : BaseEntity
{
    IQueryable<T> AsQueryable();

    Task<List<T>> FilterByAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken);

    Task<List<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TProjected>> projectionExpression,
        CancellationToken cancellationToken
    );

    Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken);

    Task<T> FindByIdAsync(string id, CancellationToken cancellationToken);

    Task InsertOneAsync(T document, CancellationToken cancellationToken);

    Task InsertManyAsync(ICollection<T> documents, CancellationToken cancellationToken);

    Task ReplaceOneAsync(T document, CancellationToken cancellationToken);

    Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken);

    Task DeleteByIdAsync(string id, CancellationToken cancellationToken);

    Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken);
}