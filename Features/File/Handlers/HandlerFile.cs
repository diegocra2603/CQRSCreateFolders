using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.Files.Handlers;

public class HandlerFile
{
    public string GetContent(Handler handler){

        var async = handler.IsAsync ? " async " : " ";

        return $$"""
        {{handler.Usings.ArrayToString()}}

        {{handler.NameSpace}};

        public sealed class {{handler.Name}}Handler : IRequestHandler<{{handler.Name}}, {{handler.Response}}>
        {
            {{handler.Fields.ArrayToString()}}

            public {{handler.Name}}Handler({{handler.FieldsToConstructor.ArrayToString()}})
            {
                {{handler.FieldsToAssign.ArrayToString()}} 
            }

            public{{async}}Task<{{handler.Response}}> Handle({{handler.Name}} request, CancellationToken cancellationToken)
            {
                {{handler.Content}}
            }
        }
        """;
    }
}