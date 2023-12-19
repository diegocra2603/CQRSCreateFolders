namespace CQRSCreateFolders;

public class SelectByIdConfig 
{
    private readonly string _nameFile;
    public SelectByIdConfig(string nameFile)
    {
        _nameFile = nameFile;
    }


    public string ContentHandler => $$"""
    if (await _{{_nameFile.FirstCharLower()}}Repository.FirstOrDefaultAsync(x => x.Id == new {{_nameFile}}Id(request.Id)) is not {{_nameFile}} {{_nameFile.FirstCharLower()}})
            {
                return Error.NotFound();
            }

            return _mapper.Map<{{_nameFile}}DTO>({{_nameFile.FirstCharLower()}});
    """;
}