// See https://aka.ms/new-console-template for more information

using RE = RulesEngine;

namespace MsRulesEngine;

public class GitHubTimeDifference
{

    public static async Task Run()
    {
        var workflows = new Workflow[]
        {
            new() {
                WorkflowName = "AlarmWorkflow",
                Rules =
                [
                    new Rule
                    {
                        RuleName = "CheckWithToString Convert",
                        RuleExpressionType = RuleExpressionType.LambdaExpression,
                        Expression = "(d2 - d1).TotalMinutes > 15",
                        ErrorMessage = "Time is not greater than 15 minutes",                        
                    }
                ]
            }
        };

      var d1 = DateTime.Now;
      var d2 = DateTime.Now.AddMinutes(20);

    var ruleParameters = new RuleParameter[] {
        new ("d1", d1),
        new ("d2", d2)
      };

        RE.RulesEngine rulesEngine = new(workflows);

        var resultList = await rulesEngine.ExecuteAllRulesAsync("AlarmWorkflow", ruleParameters);

        foreach (var result in resultList)
        {
            Console.WriteLine($"Rule: {result.Rule.RuleName}, Result: {result.IsSuccess}, Message: {result.ExceptionMessage}");           
        }
    }
}