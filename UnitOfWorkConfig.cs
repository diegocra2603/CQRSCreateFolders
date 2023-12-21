namespace CQRSCreateFolders;

public class UnitOfWorkConfig 
{
    public string FieldUnitOfWork => "\n    private readonly IUnitOfWork _unitOfWork;";
    public string ParamUnitOfWork => ",\n        IUnitOfWork unitOfWork";
    public string UsingUnitOfWork => "\nusing Domain.Primitives;";
    public string AssignUntiOfWork => "\n        _unitOfWork = unitOfWork;";
}