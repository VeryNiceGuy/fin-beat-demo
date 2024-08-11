using FinBeatDemo.Converters;
using FinBeatDemo.Models;

namespace FinBeatDemo;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<FinBeatDemoContext>();
        builder.Services.AddControllers().AddJsonOptions(o => {
            o.JsonSerializerOptions.Converters.Add(new JsonDictionaryConverter());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
