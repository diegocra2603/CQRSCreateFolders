using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.SelectByIdFile;

public class ConfigSelectByIdFile
{
    private readonly string _nameFile;
    private readonly string _fileNamespace;
    public ConfigSelectByIdFile(string nameFile, string fileNamespace)
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

        handler.Name = $"SelectBy{_nameFile}Query";

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

        handler.Content = 
        $$"""
            if (await _{{_nameFile.FirstCharLower()}}Repository.FirstOrDefaultAsync(x => x.Id == new {{_nameFile}}Id(request.Id)) is not {{_nameFile}} {{_nameFile.FirstCharLower()}})
            {
                return Error.NotFound();
            }

            return _mapper.Map<{{_nameFile}}DTO>({{_nameFile.FirstCharLower()}});
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

        command.NameSpace = $"{_fileNamespace}.SelectById";
        command.Name = $"SelectById{_nameFile}Query";
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

        validator.NameSpace = $"{_fileNamespace}.SelectById";
        validator.Name = $"SelectById{_nameFile}Query";
        validator.Content = """
            RuleFor(x => x.Id).IsNotNull.IsNotEmpty().WithName("Código")
        """;

        return validator;
    }
}