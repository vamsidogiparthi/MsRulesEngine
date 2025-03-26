using RE = RulesEngine;

namespace MsRulesEngine;

public class SimpleDiscountRules
{
    public static async Task Run()
    {
        string filePath = Path.Combine(
            Environment.CurrentDirectory,
            "RulesFiles",
            "DiscountRules.json"
        );
        Console.WriteLine(filePath);
        string jsonString = File.ReadAllText(filePath);

        var workflows = JsonSerializer.Deserialize<Workflow[]>(jsonString);

        Game game = new()
        {
            Title = "Halo",
            Genre = "Action",
            Id = 1,
            Platform = "PlayStation",
            GamingStudio = "Sony",
            Price = 59.99m,
        };

        RE.RulesEngine rulesEngine = new(workflows);

        var resultList = await rulesEngine.ExecuteAllRulesAsync("GameDiscount", game);

        foreach (var result in resultList)
        {
            Console.WriteLine(
                $"Rule: {result.Rule.RuleName}, Result: {result.IsSuccess}, Message: {result.ExceptionMessage}"
            );
        }
    }
}
