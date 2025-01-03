using simple_converter_console;
namespace simple_converter_console;


internal static class Converter
{
    internal static void Start()
    {

        if (Program.outputPath == null || Program.filePath == "?" || Program.oldFileType == "?" || Program.newFileType == "?")
        {
            Console.WriteLine("Missing parameters.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Conversion would begin now.");
            Console.WriteLine($"Output Path:\t{Program.outputPath}");
            Console.WriteLine($"File Path:\t{Program.filePath}");
            Console.WriteLine($"File Type:\t{Program.oldFileType}");
            Console.WriteLine($"New File Type:\t{Program.newFileType}\n");
            Console.ReadLine();
        }
    }
}
