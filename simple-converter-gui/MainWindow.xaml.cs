using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;



namespace simple_converter_gui
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _outputPath = Environment.CurrentDirectory;
        private string _filePath = "?";
        private string _oldFileType = "?";
        private string _newFileType = "?";

        public string OutputPath
        {
            get => _outputPath;
            set
            {
                _outputPath = value;
                OnPropertyChanged(nameof(OutputPath));
            }
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public string OldFileType
        {
            get => _oldFileType;
            set
            {
                _oldFileType = value;
                OnPropertyChanged(nameof(OldFileType));
            }
        }

        public string NewFileType
        {
            get => _newFileType;
            set
            {
                _newFileType = value;
                OnPropertyChanged(nameof(NewFileType));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                CheckFileExists = false,
                ValidateNames = false,
                FileName = "Folder Selection"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                OutputPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select an Input File",
                Filter = "Audio Files (*.mp3;*.wav;*.flac)|*.mp3;*.wav;*.flac|All Files (*.*)|*.*",
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                OldFileType = Path.GetExtension(FilePath).TrimStart('.');
                newFileTypesCombo.ItemsSource = GenerateNewFileTypes(OldFileType);
                
            }
        }

        private List<string> GenerateNewFileTypes(string oldFileType)
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

        private void ComboBoxFileTypes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Ensure the selection is not null
            if (newFileTypesCombo.SelectedItem != null)
            {
                NewFileType = newFileTypesCombo.SelectedItem.ToString();
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (OutputPath == null || FilePath == "?" || OldFileType == "?" || NewFileType == "?")
            {
                MessageBox.Show("Error: Missing parameters.");
            }
            else
            {
                switch (NewFileType)
                {
                    case "mp3":
                    case "wav":
                    case "flac":
                    case "aac":
                    case "m4a":
                        ConvertAudio(OutputPath, FilePath, OldFileType, NewFileType);
                        break;
                    default:
                        MessageBox.Show("Error: File type not supported!");
                        break;
                }
            }
        }

        private void ConvertAudio(string outputPath, string filePath, string oldFileType, string newFileType)
        {
           
            try
            {
                string arguments;
                string ffmpegPath = "ffmpeg";
                if (newFileType == "aac")
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
                    MessageBox.Show("Conversion successful!");
                }
                else
                {
                    MessageBox.Show("Error: file has not been converted!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: file has not been converted!");
            }
        }
    }
}