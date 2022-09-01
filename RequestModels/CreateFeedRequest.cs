namespace MaoFeed.RequestModels;

public class CreateFeedRequest
{
    public string PrivacyLevel { get; set; } = default!;
    public string Text { get; set; } = default!;
}