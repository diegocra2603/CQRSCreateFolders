using CQRSCreateFolders.Models;

namespace CQRSCreateFolders.Features.Files.DTOs;

public class DTOFile
{
    public string GetContent(DTO dTO){

        return $$"""
        {{dTO.NameSpace}}

        public sealed class {{dTO.Name}}DTO
        {
            {{dTO.Content}}
        }  
        """;
    }
}