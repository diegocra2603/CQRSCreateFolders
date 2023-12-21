using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.DTOFile;

public class ConfigDTOFile
{
    private readonly string _nameFile;
    private readonly string _fileNamespace;
    public ConfigDTOFile(string nameFile, string fileNamespace)
    {
        _nameFile = nameFile;
        _fileNamespace = fileNamespace;
    }

    public DTO GetDTO()
    {
        var Dto = new DTO();

        Dto.NameSpace = $"{_fileNamespace}.DTOs";
        Dto.Name = $"{_nameFile}DTO";
        Dto.Content = $"\n Guid Id";

        return Dto;
    }
}