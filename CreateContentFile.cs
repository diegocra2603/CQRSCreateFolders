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

        var unitOfWork = new UnitOfWorkConfig(_end);
        var _fileNamespaceSlit = _fileNamespace.Split(".");

        return $$"""
        using {{_fileNamespace}}.DTOs;
        using Domain.Contracts.Persistence;
        using Domain.Entities.{{_fileNamespaceSlit[2]}};{{unitOfWork.UsingUnitOfWork}}
        using ErrorOr;
        using MediatR;

        namespace {{_fileNamespace}}.{{_start}};

        public sealed class {{_start}}{{_nameFile}}{{_end}}Handler : IRequestHandler<{{_start}}{{_nameFile}}{{_end}}, ErrorOr<{{_nameFile}}DTO>>
        {
            private readonly IAsyncRepository<{{_nameFile}}> _{{_nameFile}}Repository;{{unitOfWork.FieldUnitOfWork}}

            public {{_start}}{{_nameFile}}{{_end}}Handler(IAsyncRepository<{{_nameFile}}> {{_nameFile}}Repository {{unitOfWork.ParamUnitOfWork}} )
            {
                _{{_nameFile}}Repository = {{_nameFile}}Repository;{{unitOfWork.AssignUntiOfWork}}
            }

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