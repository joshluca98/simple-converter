using Spectre.Console;
namespace simple_converter_console;

internal static class UserInterface
{
    internal static void userMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Output Path:\t{Program.outputPath}");
            Console.WriteLine($"File Path:\t{Program.filePath}");
            Console.WriteLine($"File Type:\t{Program.oldFileType}");
            Console.WriteLine($"New File Type:\t{Program.newFileType}\n");
            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                .AddChoices(Enum.GetValues<MenuAction>()));

            switch (menuChoice)
            {
                case MenuAction.ChooseFile:
                    Program.filePath = Helper.ChooseFile();
                    Program.oldFileType = Path.GetExtension(Program.filePath).TrimStart('.');
                    break;
                case MenuAction.ChooseOutputDirectory:
                    Program.outputPath = Helper.ChooseDirectory();
                    break;
                case MenuAction.ChooseNewFileType:
                    Program.newFileType = Helper.SelectNewFileType();
                    break;
                case MenuAction.StartConvert:
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
