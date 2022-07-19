using FileHasherTestTask.Commands;
using FileHasherTestTask.Models;
using FileHasherTestTask.ViewModels.Base;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace FileHasherTestTask.ViewModels
{
    internal class MainWindowVM : ViewModel
    {
        private string _status;
        private string _path;
        private IEnumerable<FileInfo> _files;
        private FileInfo _selectedFile;

        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        public string Path
        {
            get => _path;
            set
            {
                Set(ref _path, value);

                if (Directory.Exists(_path))
                    Files = new DirectoryInfo(_path).EnumerateFiles().Select(file => new FileInfo(file.FullName));
            }
        }

        public IEnumerable<FileInfo> Files
        {
            get => _files;
            set => Set(ref _files, value);
        }

        public FileInfo SelectedFile
        {
            get => _selectedFile;
            set => Set(ref _selectedFile, value);
        }

        public ICommand HashFileCommand { get; }
        private bool CanHashFileExecute(object p) => true;
        private void OnHashFileExecuted(object p)
        {
            Status = "";
            Thread _thread = new(CalculateHash) { IsBackground = true };
            _thread.Start();
        }

        private void CalculateHash(object? state)
        {
            string hash = FileHasher.Hash(SelectedFile.FullName);

            if (hash is null)
            {
                Status = "Не удалось получить доступ к файлу";
                return;
            }

            Status = hash;
            DBWriter.Write(hash);
        }

        public MainWindowVM()
        {
            HashFileCommand = new LambdaCommand(OnHashFileExecuted, CanHashFileExecute);
        }
    }
}