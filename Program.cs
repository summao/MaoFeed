using MaoFeed.DataBase;
using MaoFeed.Repositories;
using MaoFeed.Services;

var builder = WebApplication.CreateBuilder(args);
const string ALLOW_ONLY_WEB = "AllowOnlyWeb";

// Add services to the container.
var mongoDbSetting = new MongoDbSetting();
var sectionMongoDbSetting = builder.Configuration.GetSection("MongoDbSetting");
sectionMongoDbSetting.Bind(mongoDbSetting);
builder.AddMongoDb(mongoDbSetting);

builder.Services.AddScoped(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
builder.Services.AddScoped<IFeedService,FeedService>();

builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: ALLOW_ONLY_WEB,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
            });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
