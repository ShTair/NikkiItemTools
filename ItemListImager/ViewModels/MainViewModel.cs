using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ItemListImager.ViewModels
{
    class MainViewModel
    {
        public ObservableCollection<CategoryViewModel> Categories { get; private set; }

        public MainViewModel()
        {
            var path = Environment.GetCommandLineArgs()[1];
            Categories = new ObservableCollection<CategoryViewModel>(Directory.EnumerateFiles(path, "*.txt").Select(file => CategoryViewModel.Load(file)).Where(t => t != null).OrderBy(t => t.Id));
        }
    }
}
