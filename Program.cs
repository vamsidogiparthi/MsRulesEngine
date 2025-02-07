// See https://aka.ms/new-console-template for more information

using RE = RulesEngine;
using MsRulesEngine;

string filePath = Path.Combine(Environment.CurrentDirectory, "RulesFiles", "DiscountRules.json");
Console.WriteLine(filePath);
string jsonString = File.ReadAllText(filePath);
var jsonDocument = JsonDocument.Parse(jsonString);

var workflows = JsonSerializer.Deserialize<Workflow[]>(jsonString);

Game game = new()
{
    Title = "Halo",
    Genre = "Action",
    Id = 1,
    Platform = "PlayStation",
    GamingStudio = "Sony",
    Price = 59.99m
};

Game game1 = new()
{
    Title = "Prince of Persia",
    Genre = "RPG",
    Id = 1,
    Platform = "PlayStation",
    GamingStudio = "Sony",
    Price = 60.99m
};



RE.RulesEngine rulesEngine = new(workflows);

var resultList = await rulesEngine.ExecuteAllRulesAsync("GameDiscount", game, game1);

foreach (var result in resultList)
{
    Console.WriteLine($"Rule: {result.Rule.RuleName}, Result: {result.IsSuccess}, Message: {result.ExceptionMessage}");
    if (result.ActionResult is not null)
    {
        Console.WriteLine($"Discounted Price: {result.ActionResult.Output}");
    }
}

await GitHubTimeDifference.Run();
await ChainedRuleExample.Run();