// See https://aka.ms/new-console-template for more information

using RE = RulesEngine;

namespace MsRulesEngine;

public class ChainedRuleExample
{

    public static async Task Run()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "RulesFiles", "ChainedRuleExample.json");
        Console.WriteLine(filePath);
        string jsonString = File.ReadAllText(filePath);
        var jsonDocument = JsonDocument.Parse(jsonString);

        var workflows = JsonSerializer.Deserialize<Workflow[]>(jsonString);

        var d1 = DateTime.Now;
        var d2 = DateTime.Now.AddMinutes(20);

        var ruleParameters = new RuleParameter[] {
        new ("input1", 50000),
        new ("input2", 50000)
      };

        RE.RulesEngine rulesEngine = new(workflows);

        var result = await rulesEngine.ExecuteActionWorkflowAsync("EvaluateRuleActionTest", "TotalAmount5W", ruleParameters);


            Console.WriteLine($"Rule: {result.Output}");
        }
}