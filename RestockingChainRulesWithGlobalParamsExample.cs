using RulesEngine.Extensions;
using RE = RulesEngine;

namespace MsRulesEngine;

public class RestockingChainRulesWithGlobalParamsExample
{
    public static async Task Run()
    {
        string filePath = Path.Combine(
            Environment.CurrentDirectory,
            "RulesFiles",
            "RestockingChainRulesWithGlobalParams.json"
        );
        Console.WriteLine(filePath);
        string jsonString = File.ReadAllText(filePath);

        var workflows = JsonSerializer.Deserialize<Workflow[]>(jsonString);

        var gamesInventory = new List<Game>
        {
            new()
            {
                Title = "Halo",
                Genre = "Action",
                Id = 1,
                Platform = "PlayStation",
                GamingStudio = "Sony",
                Price = 59.99m,
                Quantity = 3,
            },
            new()
            {
                Title = "Yakuza",
                Genre = "Action",
                Id = 2,
                Platform = "PlayStation",
                GamingStudio = "EA",
                Price = 49.99m,
                Quantity = 10,
            },
        };

        var ruleParameters = new RuleParameter[] { new("input1", gamesInventory) };

        var settings = new ReSettings
        {
            NestedRuleExecutionMode = NestedRuleExecutionMode.Performance,
        };

        RE.RulesEngine rulesEngine = new(workflows, settings);

        var resultList = await rulesEngine.ExecuteAllRulesAsync(
            "RestockingChainRulesWithGlobalParams",
            ruleParameters
        );

        resultList.OnSuccess(
            (successevent) =>
            {
                var successRuleResult = resultList.Where(x =>
                    x.IsSuccess & x.Rule.RuleName == successevent
                );

                foreach (var result in successRuleResult)
                {
                    Console.WriteLine($"Parent Success Event: {result.ActionResult?.Output}");
                }
                foreach (var result in successRuleResult.SelectMany(x => x.ChildResults))
                {
                    Console.WriteLine(
                        $"Child Rule: {result.Rule.RuleName}, Result: {result.IsSuccess}, Message: {result.ExceptionMessage}"
                    );
                    if (result.ActionResult != null)
                    {
                        Console.WriteLine($"Child Action Result: {result.ActionResult.Output}");
                    }
                }
            }
        );
    }
}
