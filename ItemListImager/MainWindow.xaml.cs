using ItemListImager.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ItemListImager
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            Save();
        }

        private async void Save()
        {
            await Task.Delay(1000);

            var rend = new RenderTargetBitmap((int)IC.ActualWidth, (int)IC.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rend.Render(IC);

            using (var s = File.Create($"{DateTime.Now:yyyyMMddHHmmss}.png"))
            {
                var png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rend));
                png.Save(s);
            }
        }
    }
}
