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
}
