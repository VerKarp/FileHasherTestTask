using FileHasherTestTask.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHasherTestTask.ViewModels
{
    internal class MainWindowVM : ViewModel
    {
        private string _path;
        private IEnumerable<FileInfo> _files;
        private FileInfo _selectedFile;

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

    }
}