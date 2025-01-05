using simple_converter_console;
using System.Diagnostics;
namespace simple_converter_console;

internal static class Converter
{
    internal static void Start()
    {
        if (Program.outputPath == null || Program.filePath == "?" || Program.oldFileType == "?" || Program.newFileType == "?")
        {
            Console.WriteLine("Error: Missing parameters. Press ENTER to return to menu..");
            Console.ReadLine();
        }
        else
        {
            switch (Program.newFileType)
            {
                case "mp3":
                case "wav":
                case "flac":
                case "aac":
                case "m4a":
                    ConvertAudio(Program.outputPath, Program.filePath, Program.oldFileType, Program.newFileType);
                    break;
                default:
                    Console.WriteLine("File type not supported");
                    Console.ReadLine();
                    break;
            }
        }
    }

    internal static void ConvertAudio(string outputPath, string filePath, string oldFileType, string newFileType)
    {
        Console.WriteLine($"'{Path.GetFileName(filePath)}' will be converted from {oldFileType} to {newFileType}");
        Console.WriteLine("\nPress ENTER to proceed..");
        Console.ReadLine();
        try
        {
            string arguments;
            string ffmpegPath = "ffmpeg";
            if(newFileType == "aac")
            {
                arguments = $"-i \"{filePath}\" -c:a aac \"{outputPath}\\{Path.GetFileNameWithoutExtension(filePath)}.{newFileType}\"";
            }
            else
            {
                arguments = $"-i \"{filePath}\" \"{outputPath}\\{Path.GetFileNameWithoutExtension(filePath)}.{newFileType}\"";
            }
            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                Console.WriteLine("Conversion Successful");
                Helper.ResetFileVariables();
                Console.WriteLine("\nPress ENTER to return to menu..");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Error during conversion: {error}");
                Console.WriteLine("\nPress ENTER to return to menu..");
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("\nPress ENTER to return to menu..");
            Console.ReadLine();
        }

    }
}