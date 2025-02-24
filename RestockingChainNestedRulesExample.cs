using RE = RulesEngine;

namespace MsRulesEngine;
public class RestockingChainNestedRulesExample
{
    public static async Task Run()
    {

        string filePath = Path.Combine(Environment.CurrentDirectory, "RulesFiles", "RestockingChainNestedRules.json");
        Console.WriteLine(filePath);
        string jsonString = File.ReadAllText(filePath);

        var workflows = JsonSerializer.Deserialize<Workflow[]>(jsonString);


        var gamesInventory = new List<Game>
        {
            new() {
                Title = "Halo",
                Genre = "Action",
                Id = 1,
                Platform = "PlayStation",
                GamingStudio = "Sony",
                Price = 59.99m,
                Quantity = 3
            },
            new() {
                Title = "Yakuza",
                Genre = "Action",
                Id = 2,
                Platform = "PlayStation",
                GamingStudio = "EA",
                Price = 49.99m,
                Quantity = 10
            },
        };

        var ruleParameters = new RuleParameter[] {
        new ("input1", gamesInventory),
      };

        var settings = new ReSettings
        {
            NestedRuleExecutionMode = NestedRuleExecutionMode.Performance
        };

        RE.RulesEngine rulesEngine = new(workflows, settings);

        var resultList = await rulesEngine.ExecuteAllRulesAsync("RestockingChainNestedRules", ruleParameters);


        foreach (var result in resultList)
        {
            Console.WriteLine($"Rule: {result.Rule.RuleName}, Result: {result.IsSuccess}, Message: {result.ExceptionMessage}");
            if (result.ActionResult != null)
            {
                Console.WriteLine($"Action Result: {result.ActionResult.Output}");
            }
        }
    }

}