namespace CQRSCreateFolders;

public record SchemeFile(
    string Extension,
    string Start, 
    string End,
    TypeFile TypeFile,
    string NameEnd
);