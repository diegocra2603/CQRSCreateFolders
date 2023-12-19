namespace CQRSCreateFolders;

public static class SchemesToCreate
{
    public static List<SchemeDirectory> SchemesDirectories = [
        new SchemeDirectory("DTOs",
        [
            new SchemeFile("cs","","DTO", TypeFile.DTO, "DTO")
        ]
        ),
        new SchemeDirectory("Create",
        [
            new SchemeFile("cs","Create","Command", TypeFile.Command,"Command"),
            new SchemeFile("cs","Create","Command", TypeFile.Handler,"CommandHandler"),
            new SchemeFile("cs","Create","Command", TypeFile.Validation,"CommandValidation"),
        ]
        ),
        new SchemeDirectory("Update",
        [
            new SchemeFile("cs","Update","Command", TypeFile.Command,"Command"),
            new SchemeFile("cs","Update","Command", TypeFile.Handler,"CommandHandler"),
            new SchemeFile("cs","Update","Command", TypeFile.Validation,"CommandValidation"),
        ]
        ),
        new SchemeDirectory("Delete",
        [
            new SchemeFile("cs","Delete","Command", TypeFile.Command ,"Command"),
            new SchemeFile("cs","Delete","Command", TypeFile.Handler ,"CommandHandler"),
            new SchemeFile("cs","Delete","Command", TypeFile.Validation ,"CommandValidation"),
        ]
        ),
        new SchemeDirectory("SelectAll",
        [
            new SchemeFile("cs","SelectAll","Query", TypeFile.Command,"Query"),
            new SchemeFile("cs","SelectAll","Query", TypeFile.Handler,"QueryHandler"),
            new SchemeFile("cs","SelectAll","Query", TypeFile.Validation,"QueryValidation"),
        ]
        ),
        new SchemeDirectory("SelectBy",
        [
            new SchemeFile("cs","SelectById","Query", TypeFile.Command,"Query"),
            new SchemeFile("cs","SelectById","Query", TypeFile.Handler,"QueryHandler"),
            new SchemeFile("cs","SelectById","Query", TypeFile.Validation,"QueryValidation"),
        ]
        )
    ];
}