namespace CQRSCreateFolders.Models;

public class Directory {
    public string Name { get; set; } = string.Empty;
    public List<File> Files { get; set; } = new List<File>();
}