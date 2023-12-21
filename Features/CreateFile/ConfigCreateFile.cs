using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.CreateFile;

public class ConfigCreateFile
{
    private readonly string _nameFile;
    private readonly string _fileNamespace;
    public ConfigCreateFile(string nameFile, string fileNamespace)
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

        handler.NameSpace = $"{_fileNamespace}.Create";

        handler.Name = $"Create{_nameFile}Command";

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

        handler.Response = $"ErroOr<{_nameFile}DTO>";

        return handler;
    }

    public Command GetCommand(){

        var command = new Command();

        command.Usings = [
            $"using {_fileNamespace}.DTOs;\n",
            $"using ErrorOr;\n",
            $"using MediatR;"
        ];

        command.NameSpace = $"{_fileNamespace}.Create";
        command.Name = $"Create{_nameFile}Command";
        command.CommandValue = "";
        command.Reponse = $"ErroOr<{_nameFile}DTO>";

        return command;        
    }

    public Validator GetValidator(){

        var validator = new Validator();

        validator.Usings = [
            $"using {_fileNamespace}.DTOs;\n",
            $"using ErrorOr;\n",
            $"using MediatR;"
        ];

        validator.NameSpace = $"{_fileNamespace}.Create";
        validator.Name = $"Create{_nameFile}Command";
        validator.Content = "";

        return validator;
    }
}