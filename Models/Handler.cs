namespace CQRSCreateFolders.Models;

public sealed class Handler 
{
    public string[] Usings { get; set; } = [];
    public string NameSpace { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string[] Fields { get; set; } = [];
    public string[] FieldsToConstructor { get; set; } = [];
    public string[] FieldsToAssign { get; set; } = [];
    public bool IsAsync { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
}