using RE = RulesEngine;

namespace MsRulesEngine;
public class RestockingChainRulesExample
{
    public static async Task Run()
    {

        string filePath = Path.Combine(Environment.CurrentDirectory, "RulesFiles", "RestockingChainRules.json");
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
                Quantity = 5
            },
            new() {
                Title = "Yakuza",
                Genre = "Action",
                Id = 2,
                Platform = "PlayStation",
                GamingStudio = "EA",
                Price = 49.99m,
                Quantity = 2
            },
        };

        var ruleParameters = new RuleParameter[] {
        new ("input1", gamesInventory),
      };


        RE.RulesEngine rulesEngine = new(workflows);

        var result = await rulesEngine.ExecuteActionWorkflowAsync("RestockingChainRules", "ActionPlaystationGameRestock1", ruleParameters);


        Console.WriteLine($"Rule: {result.Output}");
    }

}