using MaoFeed.Entities.Feed;
using MaoFeed.RequestModels;
using MaoFeed.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaoFeed.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedsController : ControllerBase
{
    private readonly IFeedService _feedService;
    
    public FeedsController(IFeedService feedService)
    {
        _feedService = feedService;
    }

    [HttpPost]
    public IActionResult Create(CreateFeedRequest req, CancellationToken cancellationToken)
    {
        var feed = new Feed
        {
            Text = req.Text,
            PrivacyLevel = req.PrivacyLevel,
            Poster = new Member
            {
                ProfileName = "Mao",
                ProfileImagePath = "xxx",
            },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        _feedService.Add(feed, cancellationToken);

        return Created(feed.Id?? "null", null);
    }
}