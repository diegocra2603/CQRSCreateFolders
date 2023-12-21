using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.SelectAllFile;

public class ConfigSelectAllFile
{
    private readonly string _nameFile;
    private readonly string _fileNamespace;
    public ConfigSelectAllFile(string nameFile, string fileNamespace)
    {
        _nameFile = nameFile;
        _fileNamespace = fileNamespace;
    }

    public Handler GetHandler(){

        var handler = new Handler();
        var unitOfWork = new UnitOfWorkConfig();
        var fileNamespaceSlit = _fileNamespace.Split(".");

        handler.Usings = [
            $"using {_fileNamespace}.DTOs;\n",
            $"using Domain.Contracts.Persistence;\n",
            $"using Domain.Entities.{fileNamespaceSlit[2]};{unitOfWork.UsingUnitOfWork};\n",
            $"using AutoMapper;\n",
            $"using ErrorOr;\n",
            $"using MediatR;"
        ];

        handler.NameSpace = $"{_fileNamespace}.SelectAll";

        handler.Name = $"SelectAll{_nameFile}Query";

        handler.Fields = [
            $"private readonly IAsyncRepository<{_nameFile}> _{_nameFile.FirstCharLower()}Repository;\n",
            "private readonly IMapper _mapper;\n",
            "private readonly IUnitOfWork _unitOfWork;"
        ];

        handler.FieldsToConstructor = [
            $"\nIAsyncRepository<{_nameFile}> {_nameFile.FirstCharLower()}Repository,",
            "\nIMapper mapper,",
            "\nIUnitOfWork unitOfWork"
        ];

        handler.FieldsToAssign = [
            "_{{_nameFile.FirstCharLower()}}Repository = {{_nameFile.FirstCharLower()}}Repository;",
            "_mapper = mapper;",
            "_unitOfWork = unitOfWork;"
        ];

        handler.IsAsync = false;

        handler.Response = $"ErroOr<List<{_nameFile}DTO>>";

        var splitFileNamespace = _fileNamespace.Split(".");
        var nameFilePlural = splitFileNamespace[2];

        handler.Content = 
        $$"""
            var {{nameFilePlural.FirstCharLower()}} = await _{{_nameFile.FirstCharLower()}}Repository.GetAllAsync();

            return _mapper.Map<List<{{_nameFile}}DTO>>({{nameFilePlural.FirstCharLower()}}.ToList());
        """;

        return handler;
    }

    public Command GetCommand(){

        var command = new Command();

        command.Usings = [
            $"using {_fileNamespace}.DTOs;\n",
            $"using ErrorOr;\n",
            $"using MediatR;"
        ];

        command.NameSpace = $"{_fileNamespace}.SelectAll";
        command.Name = $"SelectAll{_nameFile}Query";
        command.CommandValue = "";
        command.Reponse = $"ErroOr<List<{_nameFile}DTO>>";

        return command;        
    }
}