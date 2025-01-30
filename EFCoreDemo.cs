
using Microsoft.EntityFrameworkCore;
using MsRulesEngine.DBContext;
using RE = RulesEngine;
namespace MsRulesEngine;
public class EFCoreDemo
{

    public static async Task Run()
    {
        ArcadeDbContext db = new();
        db.Database.EnsureCreated();
        
        var dbArcade = db.Arcades.FirstOrDefault();
        var games = db.Games.AsNoTracking().ToArray();
        var workflows = JsonSerializer.Deserialize<Workflow[]>(dbArcade!.Workflows);
        RE.RulesEngine rulesEngine = new(workflows);

        var resultList = await rulesEngine.ExecuteAllRulesAsync("GameDiscount", games);

        foreach (var result in resultList)
        {
            Console.WriteLine($"Rule: {result.Rule.RuleName}, Result: {result.IsSuccess}, Message: {result.ExceptionMessage}");
        }
    }

}
