using MaoFeed.Entities.Feed;
using MaoFeed.Repositories;

namespace MaoFeed.Services;

public interface IFeedService
{
    Task Add(Feed feed, CancellationToken cancellationToken);
}

public class FeedService : IFeedService
{
    private readonly IMongoDbRepository<Feed> _feedRepository;

    public FeedService(IMongoDbRepository<Feed> feedRepository)
    {
        _feedRepository = feedRepository;
    }

    public async Task Add(Feed feed, CancellationToken cancellationToken)
    {
        await _feedRepository.InsertOneAsync(feed, cancellationToken);
    }
}