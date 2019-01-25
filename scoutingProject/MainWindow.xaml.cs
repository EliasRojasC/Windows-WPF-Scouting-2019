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

namespace scoutingProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Classes_For_Function.runadbcommand runadbcommand = new Classes_For_Function.runadbcommand();
        public MainWindow()
        {
            var currentuser = System.Environment.GetEnvironmentVariable("USERPROFILE");
            InitializeComponent();
            var overallDir = currentuser+"//SkoutResources";
            var bc = new BrushConverter();
            Content.Content = new Pages.Export();
            System.IO.Directory.CreateDirectory(overallDir);
            System.IO.Directory.CreateDirectory(overallDir + "//Settings");
            System.IO.Directory.CreateDirectory(overallDir + "//DirectFileInbound");
            System.IO.Directory.CreateDirectory(overallDir + "//outGoingFiles");
            System.IO.Directory.CreateDirectory(overallDir + "//archive");
            System.IO.Directory.CreateDirectory(overallDir + "//scheduleArchive");
            ExportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTRedBrush");
            try
            {
                runadbcommand.runADBCommand("kill-server");
                runadbcommand.runADBCommand("start-server");
            }catch (Exception)
            {
                MessageBox.Show("The ADB server was not instantiated, You might have issues using Import and Schedule Functions. Please reset if things do not work.", "Error");
            }
        }

        private void exportButton(object sender, RoutedEventArgs e)
        {
            Content.Content = new Pages.Export();
            ExportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTRedBrush");
            SettingsButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            ImportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            ScheduleButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
        }

        private void importButton(Object sender, RoutedEventArgs e)
        {
            Content.Content = new Pages.Import();
            ExportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            SettingsButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            ImportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTRedBrush");
            ScheduleButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
        }

        private void settingsButton(Object sender, RoutedEventArgs e)
        {
            Content.Content = new Pages.Settings();
            ExportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            SettingsButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTRedBrush");
            ImportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            ScheduleButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
        }

        private void scheduleButton(Object sender, RoutedEventArgs e)
        {
            Content.Content = new Pages.Schedule();
            ExportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            SettingsButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            ImportButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTBlueBrush");
            ScheduleButton.Foreground = (SolidColorBrush)Application.Current.FindResource("FIRSTRedBrush");
        }
    }
}
