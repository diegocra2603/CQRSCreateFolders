using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.Files.Commands;

public class CommandFile
{
    public string GetContent(Command command){

        var usings = "";

        foreach (var usingField in command.Usings)
        {
            usings += $"\n{usingField};";
        }

        return $$"""
        {{usings}}

        {{command.NameSpace}};

        public partial record {{command.Name}}({{command.CommandValue}}) : IRequest<{{command.Reponse}}>;
        """;
    }
}