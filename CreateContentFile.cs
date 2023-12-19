using System.ComponentModel.Design;

namespace CQRSCreateFolders;

public class CreateContentFile
{
    private readonly TypeFile _typeFile;
    private readonly string _nameFile;
    private readonly string _fileNamespace;
    private readonly string _start;
    private readonly string _end;
    private Dictionary<TypeFile, Func<string>> types = new Dictionary<TypeFile, Func<string>>();
    public CreateContentFile(TypeFile typeFile, string nameFile, string fileNamespace, string start, string end)
    {
        _typeFile = typeFile;
        _nameFile = nameFile;
        _fileNamespace = fileNamespace;
        _start = start;
        _end = end;

        types.Add(TypeFile.Handler, () => CreateHandler());
        types.Add(TypeFile.Command, () =>  CreateCommand());
        types.Add(TypeFile.DTO, () =>  CreateDTO());
        types.Add(TypeFile.Validation, () =>  CreateValidation());
    }

    public string Create()
    {
        return types.FirstOrDefault( x => x.Key == _typeFile).Value.Invoke();
    }

    public string CreateHandler(){
        return $$"""
        using {{_fileNamespace}}.DTOs;
        using ErrorOr;
        using MediatR;

        namespace {{_fileNamespace}}.{{_start}};

        public sealed class {{_start}}{{_nameFile}}{{_end}}Handler : IRequestHandler<{{_start}}{{_nameFile}}{{_end}}, ErrorOr<{{_nameFile}}DTO>>
        {
            public Task<ErrorOr<{{_nameFile}}DTO>> Handle({{_start}}{{_nameFile}}{{_end}} request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
        """;
    }

    public string CreateCommand(){
        return $$"""
        using {{_fileNamespace}}.DTOs;
        using ErrorOr;
        using MediatR;

        namespace {{_fileNamespace}}.{{_start}};

        public partial record {{_start}}{{_nameFile}}{{_end}}() : IRequest<ErrorOr<{{_nameFile}}DTO>>;
        """;
    }

    public string CreateValidation(){
        return $$"""
        using FluentValidation;

        namespace {{_fileNamespace}}.{{_start}};

        public class {{_start}}{{_nameFile}}{{_end}}Validator : AbstractValidator<{{_start}}{{_nameFile}}{{_end}}>
        {

        }
        """;
    }

    public string CreateDTO(){
        return $$"""
        namespace {{_fileNamespace}}.DTOs;

        public record {{_nameFile}}DTO();  
        """;
    }
}