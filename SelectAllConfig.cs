namespace CQRSCreateFolders;

public class SelectAllConfig 
{
    private readonly string _nameFile;
    private readonly string _nameFilePlural;
    public SelectAllConfig(string nameFile, string nameFilePlural)
    {
        _nameFile = nameFile;
        _nameFilePlural = nameFilePlural;
    }

    public string IRequestResponse => $"ErrorOr<List<{_nameFile}DTO>>";

    public string ContentHandler => $"""
    var {_nameFilePlural.FirstCharLower()} = await _{_nameFile.FirstCharLower()}Repository.GetAllAsync();

            return _mapper.Map<List<{_nameFile}DTO>>({_nameFilePlural.FirstCharLower()}.ToList());
    """;
}