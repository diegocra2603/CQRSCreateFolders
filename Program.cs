
namespace CQRSCreateFolders;

class Program
{
    private List<string> EsquemasACrear = new List<string>();

    static string directorioActual = System.IO.Directory.GetCurrentDirectory();

    static void Main(string[] args)
    {
        CrearDirectorios(args);
    }

    static string[] ObtenerUltimosDirectorios()
    {
        string[] partes = directorioActual.Split(System.IO.Path.DirectorySeparatorChar);

        var cantidad = 3;

        if (partes.Length >= cantidad)
        {
            string[] ultimosDirectorios = new string[cantidad];
            Array.Copy(partes, partes.Length - cantidad, ultimosDirectorios, 0, cantidad);
            return ultimosDirectorios;
        }
        else
        {
            return partes;
        }
    }

    static void CrearDirectorios(string[] args)
    {
        foreach (var nombreDirectorio in SchemesToCreate.SchemesDirectories)
        {
            string rutaDirectorio = Path.Combine(directorioActual, nombreDirectorio.Directory);

            if (!Directory.Exists(rutaDirectorio))
            {
                Directory.CreateDirectory(rutaDirectorio);
                Console.WriteLine($"Directorio creado: {rutaDirectorio}");

                foreach (var file in nombreDirectorio.Files)
                {
                    var namefile = args[0] ?? "General";
                    CrearArchivos(file, rutaDirectorio, namefile);
                }

            }

        }
    }

    static void CrearArchivos(SchemeFile schemeFile, string directorio, string nameFile)
    {
        string rutaArchivoCS = Path.Combine(directorio, $"{schemeFile.Start}{nameFile}{schemeFile.NameEnd}.{schemeFile.Extension}");

        if (!File.Exists(rutaArchivoCS))
        {
            string[] directorios = ObtenerUltimosDirectorios();

            var fileNamespace = $"{directorios[0]}.{directorios[1]}.{directorios[2]}";

            var createContentFile = new CreateContentFile(schemeFile.TypeFile, nameFile, fileNamespace, schemeFile.Start, schemeFile.End);

            string contenidoArchivo = createContentFile.Create();

            File.WriteAllText(rutaArchivoCS, contenidoArchivo);
        }
        else
        {
            Console.WriteLine($"El archivo .cs ya existe en {directorio}");
        }
    }
}