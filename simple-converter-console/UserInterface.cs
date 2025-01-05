using Spectre.Console;
using System.ComponentModel.DataAnnotations;
namespace simple_converter_console;

internal static class UserInterface
{
    internal static void userMenu()
    {
        while (true)
        {
            string[] menuChoices = { 
                "Select Input File",
                "Select Output Directory",
                "Select Output File Type",
                "\n-Start Conversion-" 
            };

            Console.Clear();
            Console.WriteLine($"Output Path:\t{Program.outputPath}");
            Console.WriteLine($"Input File Path:\t{Program.filePath}");
            Console.WriteLine($"Input File Type:\t{Program.oldFileType}");
            Console.WriteLine($"Output File Type:\t{Program.newFileType}\n");
            
            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices( menuChoices )
            );

            switch (menuChoice)
            {
                case "Select Input File":
                    Program.filePath = Helper.ChooseFile();
                    if(Program.filePath != "?") { Program.oldFileType = Path.GetExtension(Program.filePath).TrimStart('.'); }
                    break;
                case "Select Output Directory":
                    Program.outputPath = Helper.ChooseDirectory();
                    break;
                case "Select Output File Type":
                    Program.newFileType = Helper.SelectNewFileType();
                    break;
                case "\n-Start Conversion-":
                    Converter.Start();
                    break;
                
            }
        }
    }

    public enum MenuAction
    {
        ChooseFile,
        ChooseOutputDirectory,
        ChooseNewFileType,
        StartConvert
    }
}