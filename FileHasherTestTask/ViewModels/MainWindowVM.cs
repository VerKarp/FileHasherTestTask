using FileHasherTestTask.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHasherTestTask.ViewModels
{
    internal class MainWindowVM : ViewModel
    {
        private string _title = "File Hasher";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

    }
}