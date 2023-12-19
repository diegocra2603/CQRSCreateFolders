namespace CQRSCreateFolders;

public record SchemeDirectory(
    string Directory,
    List<SchemeFile> Files
);