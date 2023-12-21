namespace CQRSCreateFolders.Models;

public sealed class Command 
{
    public string[] Usings { get; set; } = [];
    public string NameSpace { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;    
    public string CommandValue { get; set; } = string.Empty;    
    public string Reponse { get; set; } = string.Empty;    
}