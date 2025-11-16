namespace CSharp.Core.Models;

public class OrderValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Reasons { get; set; } = [];
}