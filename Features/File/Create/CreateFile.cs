using CQRSCreateFolders.Features.CreateFile;
using CQRSCreateFolders.Features.DeleteFile;
using CQRSCreateFolders.Features.DTOFile;
using CQRSCreateFolders.Features.SelectAllFile;
using CQRSCreateFolders.Features.SelectByIdFile;
using CQRSCreateFolders.Features.UpdateFile;
using CQRSCreateFolders.Models.Contracts;

namespace CQRSCreateFolders.Features.Files.Create;

public class CreateFile
{
    private readonly string _nameFile;
    private readonly string _fileNamespace;

    public CreateFile(string nameFile,string fileNamespace)
    {
        _nameFile = nameFile;
        _fileNamespace = fileNamespace;

        var configCreateFile = new ConfigCreateFile(_nameFile, _fileNamespace);
        var configDeleteFile = new ConfigDeleteFile(_nameFile, _fileNamespace);
        var configDTOFile = new ConfigDTOFile(_nameFile, _fileNamespace);
        var configSelectAllFile = new ConfigSelectAllFile(_nameFile, _fileNamespace);
        var configSelectByIdFile = new ConfigSelectByIdFile(_nameFile, _fileNamespace);
        var configUpdateFile = new ConfigUpdateFile(_nameFile, _fileNamespace);
    }

    public void CreateFilesCreate(){

    }

}