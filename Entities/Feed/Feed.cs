using MaoFeed.DataBase;

namespace MaoFeed.Entities.Feed;

[BsonCollection("feeds")]
public class Feed : BaseEntity
{
    public string PrivacyLevel { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string? ImagePath { get; set; }
    public Member Poster { get; set; } = default!;
}