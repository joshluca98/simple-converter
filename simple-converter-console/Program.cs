
using simple_converter_console;

internal class Program
{
    public static string outputPath { get; set; } = Environment.CurrentDirectory;
    public static string filePath { get; set; } = "?";

    [STAThread]
    static void Main(string[] args)
    {
        UserInterface.userMenu();
    }
}
