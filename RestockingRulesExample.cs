using RE = RulesEngine;

namespace MsRulesEngine;
public class RestockingRulesExample
{
    public static async Task Run()
    {


        string filePath = Path.Combine(Environment.CurrentDirectory, "RulesFiles", "RestockingRules.json");
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
                Quantity = 2
            },
            new() {
                Title = "Yakuza",
                Genre = "RPG",
                Id = 2,
                Platform = "Xbox",
                GamingStudio = "EA",
                Price = 49.99m,
                Quantity = 5
            },
            new() {
                Title = "Persona 5",
                Genre = "Sports",
                Id = 3,
                Platform = "Xbox",
                GamingStudio = "EA",
                Price = 49.99m,
                Quantity = 6
            }
        };

        RE.RulesEngine rulesEngine = new(workflows);

        var resultList = await rulesEngine.ExecuteAllRulesAsync("RestockingRules", gamesInventory);

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