namespace MsRulesEngine.Models;
public class Game {
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public long Id { get; set; }
    public string Platform { get; set; } = string.Empty;    
    public string GamingStudio { get; set; } = string.Empty;
    public decimal Price { get; set; }
}