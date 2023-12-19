namespace CQRSCreateFolders;

public class UnitOfWorkConfig 
{
    private readonly string _end;
    public UnitOfWorkConfig(string end)
    {
        _end = end;
    }
    
    public string FieldUnitOfWork => (_end == "Command" ) ? "\n    private readonly IUnitOfWork _unitOfWork;" : "";
    public string ParamUnitOfWork => (_end == "Command" ) ? ", IUnitOfWork unitOfWork" : "";
    public string UsingUnitOfWork => (_end == "Command" ) ? "\nusing Domain.Primitives;" : "";
    public string AssignUntiOfWork => (_end == "Command" ) ? "\n        _unitOfWork = unitOfWork;" : "";
}