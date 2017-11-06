using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UxParticles.Demo.ListBoxPerformance
{
    using System.Windows.Interop;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Width = 1920;
            this.Height = 1080;
            RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;

            this.DataContext = new MainWindowViewModel();            
        }
    }


}
