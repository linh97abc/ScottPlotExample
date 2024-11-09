using Plot.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Plot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            this.viewModel = new MainViewModel(this.chart);
            this.DataContext = viewModel;
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            this.SelectDataFile();
        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            this.chart.Plot.Axes.AutoScale();
            this.chart.Refresh();
        }

        private void btnZoomXY_Checked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnZoomXY_Checked");
            Helper.PlotInteract.Zoom_XY(new ScottPlot.WPF.WpfPlot[] { this.chart });
        }

        private void btnZoomX_Checked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnZoomX_Checked");
            Helper.PlotInteract.Zoom_X(new ScottPlot.WPF.WpfPlot[] { this.chart });
        }

        private void btnZoomY_Checked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnZoomY_Checked");
            Helper.PlotInteract.Zoom_Y(new ScottPlot.WPF.WpfPlot[] { this.chart });
        }

        void SelectDataFile()
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.Filter = "CSV file (*.csv)|*.csv";
            var res = ofd.ShowDialog();

            if (res == true)
            {
                this.viewModel.DataFilePath = ofd.FileName;
                this.btnZoomXY.IsChecked = true;
                this.btnZoom_Click(null, null);
            }
        }

        private void StackPanel_Drop(object sender, System.Windows.DragEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("File Data Drop");
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

                var matFile = files[0];
                if (System.IO.Path.GetExtension(matFile).ToUpper() != ".CSV") return;
                

                this.viewModel.DataFilePath = matFile;
                this.btnZoomXY.IsChecked = true;
                this.btnZoom_Click(null, null);
            }

        }

    }

}
