using MongoDB.Driver;

namespace MaoFeed.DataBase;

public static class MongoDbExtensions
{
    public static void AddMongoDb(this WebApplicationBuilder builder, MongoDbSetting mongoDbSetting)
    {
        builder.Services.AddSingleton<IMongoDatabase>(db => {
            return new MongoClient(mongoDbSetting.ConnectionString)
                .GetDatabase(mongoDbSetting.DatabaseName);
        });
    }
}