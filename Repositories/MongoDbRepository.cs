
using System.Linq.Expressions;
using MongoDB.Driver;
using MaoFeed.Entities;
using MaoFeed.DataBase;

namespace MaoFeed.Repositories;

public class MongoDbRepository<T> : IMongoDbRepository<T>
    where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbRepository(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<T>(GetCollectionName(typeof(T)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .First()).CollectionName;
    }

    public virtual IQueryable<T> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public virtual async Task<List<T>> FilterByAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken)
    {
        return await _collection.Find(filterExpression).ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<T, bool>> filterExpression,
        Expression<Func<T, TProjected>> projectionExpression, 
        CancellationToken cancellationToken)
    {
        return await _collection.Find(filterExpression).Project(projectionExpression).ToListAsync(cancellationToken);
    }

    public virtual async Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken)
    {
        return await _collection.Find(filterExpression).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<T> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _collection.Find(a => a.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task InsertOneAsync(T document, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(document, null, cancellationToken);
    }

    public virtual async Task InsertManyAsync(ICollection<T> documents, CancellationToken cancellationToken)
    {
        await _collection.InsertManyAsync(documents, null, cancellationToken);
    }

    public virtual async Task ReplaceOneAsync(T document, CancellationToken cancellationToken)
    {
        await _collection.FindOneAndReplaceAsync(a => a.Id == document.Id, document, null, cancellationToken);
    }

    public async Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken)
    {
        await _collection.FindOneAndDeleteAsync(filterExpression, null, cancellationToken);
    }

    public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
    {
        await _collection.FindOneAndDeleteAsync(a => a.Id == id, null, cancellationToken);
    }

    public async Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken)
    {
        await _collection.DeleteManyAsync(filterExpression, cancellationToken);
    }
}