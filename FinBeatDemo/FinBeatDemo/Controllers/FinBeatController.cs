using FinBeatDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinBeatDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class FirstAssignmentController
{
    private FinBeatDemoContext _dbContext;
    public FirstAssignmentController(FinBeatDemoContext dbContext) => _dbContext = dbContext;

    [HttpGet]
    public List<Item> Get(int? id, int? code, string? value)
    {
        var items = _dbContext.Items.AsQueryable();

        if (id.HasValue)
        {
            items = items.Where(i => i.Id == id);
        }

        if (code.HasValue)
        {
            items = items.Where(i => i.Code == code);
        }

        if (!string.IsNullOrEmpty(value))
        {
            items = items.Where(i => i.Value == value);
        }

        return [.. items];
    }

    [HttpPost]
    public async Task<IResult> Post(Dictionary<int, string> items)
    {
        var i = 0;
        foreach(var item in items.OrderBy(data => data.Key))
        {
            _dbContext.Items.ExecuteDelete();

            await _dbContext.AddAsync(new Item
            {
                Id = i++,
                Code = item.Key,
                Value = item.Value
            });
        }

        await _dbContext.SaveChangesAsync();

        return Results.Ok();
    }
}
