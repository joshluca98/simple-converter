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
            Console.WriteLine($"File Path:\t{Program.filePath}\n");
            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                .AddChoices(Enum.GetValues<MenuAction>()));

            switch (menuChoice)
            {
                case MenuAction.SelectFile:
                    Program.filePath = Helper.ChooseFile();
                    break;
                case MenuAction.SetOutputPath:
                    Program.outputPath = Helper.ChooseDirectory();
                    break;
                case MenuAction.Convert:
                    Converter.Start();
                    break;
            }
        }
    }

    public enum MenuAction
    {
        SelectFile,
        SetOutputPath,
        Convert
    }
}
