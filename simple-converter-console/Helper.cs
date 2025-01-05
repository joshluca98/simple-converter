using Spectre.Console;
using System;
using System.Windows.Forms;

namespace simple_converter_console;

internal static class Helper
{
    internal static string ChooseDirectory()
    {
        using (FolderBrowserDialog folderDialog = new())
        {
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return folderDialog.SelectedPath;
            }
            else
            {
                return Environment.CurrentDirectory;
            }
        }
    }

    internal static string ChooseFile()
    {
        using (OpenFileDialog fileDialog = new())
        {
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK){ return fileDialog.FileName; }
            return "?";
        }
    }

    internal static List<string> GenerateNewFileTypes(string oldFileType)
    {
        List<string> newFileTypes = new();

        Dictionary<string, string> FileTypeCategories = new()
        {
            { "mp3", "Audio" },
            { "wav", "Audio" },
            { "flac", "Audio" },
            { "aac", "Audio" },
        };

        Dictionary<string, List<string>> ConversionOptions = new()
        {
            { "Audio", new List<string> { "mp3", "wav", "flac", "aac" } },
        };

        if (!FileTypeCategories.TryGetValue(oldFileType, out string category))
        {
            return null;
        }

        if (ConversionOptions.TryGetValue(category, out List<string> eligibleConversions))
        {
            foreach (string conversion in eligibleConversions)
            {
                if (!conversion.Equals(oldFileType, StringComparison.OrdinalIgnoreCase))
                {
                    newFileTypes.Add(conversion);
                }
            }
        }

        //newFileTypes.ForEach(Console.WriteLine);
        return newFileTypes;
    }

    internal static string SelectNewFileType()
    {
        if (Program.outputPath == null || Program.filePath == "?" || Program.oldFileType == "?")
        {
            Console.WriteLine("Error: Missing parameters. Press ENTER to return to menu..");
            Console.ReadLine();
            return "?";
        }
        List<string> newFileTypes = GenerateNewFileTypes(Program.oldFileType);

        var newFileType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Possible Conversion Types: ")
            .AddChoices(newFileTypes));

        return newFileType;
    }

    internal static void ResetFileVariables()
    {
        Program.filePath = "?";
        Program.oldFileType = "?";
        Program.newFileType = "?";
    }
}
