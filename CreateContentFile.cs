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
        var selectAllConfig = new SelectAllConfig(_nameFile, _fileNamespaceSlit[2]);
        var selectbyIdConfig = new SelectByIdConfig(_nameFile);
        var response = _start.Contains("SelectAll") ? selectAllConfig.IRequestResponse : $"ErrorOr<{_nameFile}DTO>";
        
        string content = $"throw new NotImplementedException();";
        string strAsync = " ";

        if(_start.Contains("SelectAll"))
        {
            content = selectAllConfig.ContentHandler;
            strAsync = " async ";
        } 
        else if (_start.Contains("SelectById")) 
        {
            content = selectbyIdConfig.ContentHandler;
            strAsync = " async ";
        }
    
        return $$"""
        using {{_fileNamespace}}.DTOs;
        using Domain.Contracts.Persistence;
        using Domain.Entities.{{_fileNamespaceSlit[2]}};{{unitOfWork.UsingUnitOfWork}}
        using AutoMapper;
        using ErrorOr;
        using MediatR;

        namespace {{_fileNamespace}}.{{_start}};

        public sealed class {{_start}}{{_nameFile}}{{_end}}Handler : IRequestHandler<{{_start}}{{_nameFile}}{{_end}}, {{response}}>
        {
            private readonly IAsyncRepository<{{_nameFile}}> _{{_nameFile.FirstCharLower()}}Repository;
            private readonly IMapper _mapper;{{unitOfWork.FieldUnitOfWork}}

            public {{_start}}{{_nameFile}}{{_end}}Handler(
                IAsyncRepository<{{_nameFile}}> {{_nameFile.FirstCharLower()}}Repository,
                IMapper mapper{{unitOfWork.ParamUnitOfWork}})
            {
                _{{_nameFile.FirstCharLower()}}Repository = {{_nameFile.FirstCharLower()}}Repository;
                _mapper = mapper;{{unitOfWork.AssignUntiOfWork}}
            }

            public{{strAsync}}Task<{{response}}> Handle({{_start}}{{_nameFile}}{{_end}} request, CancellationToken cancellationToken)
            {
                {{content}}
            }
        }
        """;
    }

    public string CreateCommand(){
        var _fileNamespaceSlit = _fileNamespace.Split(".");
        var selectAllConfig = new SelectAllConfig(_nameFile, _fileNamespaceSlit[2]);
        var response = _start.Contains("SelectAll") ? selectAllConfig.IRequestResponse : $"ErrorOr<{_nameFile}DTO>";
        var contentCommand = _start.Contains("SelectById") || _start.Contains("Delete") ? "\n        Guid Id" : "";

        return $$"""
        using {{_fileNamespace}}.DTOs;
        using ErrorOr;
        using MediatR;

        namespace {{_fileNamespace}}.{{_start}};

        public partial record {{_start}}{{_nameFile}}{{_end}}({{contentCommand}}) : IRequest<{{response}}>;
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

        public sealed class {{_nameFile}}DTO
        {
             
        }  
        """;
    }
}