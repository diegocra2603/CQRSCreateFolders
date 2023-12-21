namespace CQRSCreateFolders.Models;

public sealed class Validator 
{
    public string[] Usings { get; set; } = [];
    public string NameSpace { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}