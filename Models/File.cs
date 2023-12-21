namespace CQRSCreateFolders.Models;

public sealed class File 
{
    public TypeFile TypeFile { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}