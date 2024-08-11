using FinBeatDemo.Converters;
using FinBeatDemo.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            options.MapType(typeof(Dictionary<int, string>), () => new OpenApiSchema
            {
                Type = "array",
                Example = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        { "1", new OpenApiString("value1") },
                    },
                    new OpenApiObject
                    {
                        { "2", new OpenApiString("value2") },
                    },
                    new OpenApiObject
                    {
                        { "3", new OpenApiString("value3") }
                    }
                }
            });
        });

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
