using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.Files.Validators;

public class ValidatorFile
{
    public string GetContent(Validator validator){

        return $$"""
        {{GetUsigns(validator.Usings)}}

        {{validator.NameSpace}}

        public class {{validator.Name}}Validator : AbstractValidator<{{validator.Name}}>
        {
            {{validator.Content}}
        }
        """;
    }

    private string GetUsigns(string[] values)
    {
        var value = "";

        foreach (var item in values)
        {
            value += $"\n{item};";
        }

        return value;
    }
}