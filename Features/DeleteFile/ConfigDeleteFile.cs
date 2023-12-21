using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.DeleteFile;

public class ConfigDeleteFile
{
    private readonly string _nameFile;
    private readonly string _fileNamespace;
    public ConfigDeleteFile(string nameFile, string fileNamespace)
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
            $"using ErrorOr;\n",
            $"using MediatR;"
        ];

        handler.NameSpace = $"{_fileNamespace}.Delete";

        handler.Name = $"Delete{_nameFile}Command";

        handler.Fields = [
            $"private readonly IAsyncRepository<{_nameFile}> _{_nameFile.FirstCharLower()}Repository;\n",
            "private readonly IUnitOfWork _unitOfWork;"
        ];

        handler.FieldsToConstructor = [
            $"\nIAsyncRepository<{_nameFile}> {_nameFile.FirstCharLower()}Repository,",
            "\nIUnitOfWork unitOfWork"
        ];

        handler.FieldsToAssign = [
            "_{{_nameFile.FirstCharLower()}}Repository = {{_nameFile.FirstCharLower()}}Repository;",
            "_unitOfWork = unitOfWork;"
        ];

        handler.IsAsync = false;

        handler.Response = $"ErroOr<Unit>";

        handler.Content = $$"""
            if (await _{{_nameFile.FirstCharLower()}}Repository.FirstOrDefault(new {{_nameFile}}Id(request.Id)) is not {{_nameFile}} {{_nameFile.FirstCharLower()}})
            {
                return Error.NotFound();
            }

            _{{_nameFile.FirstCharLower()}}Repository.Delete({{_nameFile.FirstCharLower()}});

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
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

        command.NameSpace = $"{_fileNamespace}.Delete";
        command.Name = $"Delete{_nameFile}Command";
        command.CommandValue = "\nGuid Id";
        command.Reponse = $"ErroOr<Unit>";

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
        validator.Name = $"Delete{_nameFile}Command";
        validator.Content = """
            RuleFor(x => x.Id).IsNotNull.IsNotEmpty().WithName("Código")
        """;

        return validator;
    }
}