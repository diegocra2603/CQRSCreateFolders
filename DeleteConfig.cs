namespace CQRSCreateFolders;

public class DeleteConfig 
{
    private readonly string _nameFile;
    public DeleteConfig(string nameFile)
    {
        _nameFile = nameFile;
    }

    public string IRequestResponse => $"ErrorOr<Unit>";
    public string ContentHandler => $$"""
    if (await _{{_nameFile.FirstCharLower()}}Repository.FirstOrDefaultAsync( x => x.Id == new {{_nameFile}}Id(request.Id)) is not {{_nameFile}} {{_nameFile.FirstCharLower()}})
            {
                return Error.NotFound();
            }

            _{{_nameFile.FirstCharLower()}}Repository.Delete({{_nameFile.FirstCharLower()}});

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
    """;
}