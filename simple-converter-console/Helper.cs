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
            if (result == DialogResult.OK)
            {
                return fileDialog.FileName;
            }
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
            { "jpg", "Image" },
            { "png", "Image" },
            { "bmp", "Image" },
        };

        Dictionary<string, List<string>> ConversionOptions = new()
        {
            { "Audio", new List<string> { "mp3", "wav", "flac" } },
            { "Image", new List<string> { "jpg", "png", "bmp" } },
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
        List<string> newFileTypes = GenerateNewFileTypes(Program.oldFileType);

        var newFileType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Possible Conversion Types: ")
            .AddChoices(newFileTypes));

        return newFileType;
    }
}
