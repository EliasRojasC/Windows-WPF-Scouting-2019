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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace scoutingProject.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var currentuser = System.Environment.GetEnvironmentVariable("USERPROFILE");
            var sheetsId = SheetsIDTextBox.GetLineText(0);
            var CsvDiR = CsvDocumentDirectoryTextBox.GetLineText(0);
            if (SheetsIDTextBox.GetLineText(0).Length > 0)
            {
                if (File.Exists(currentuser + "\\SkoutResources\\Settings\\SHEETSID"))
                {
                    System.IO.FileInfo SheetsId = new FileInfo(currentuser + "\\SkoutResources\\Settings\\SHEETSID");
                    SheetsId.Delete();
                }
                System.IO.File.WriteAllText(currentuser + "\\SkoutResources\\Settings\\SHEETSID", sheetsId);
            }
            if (CsvDocumentDirectoryTextBox.GetLineText(0).Length > 0)
            {
                if (File.Exists(currentuser + "\\SkoutResources\\Settings\\CSVDIR"))
                {
                    System.IO.FileInfo SheetsId = new FileInfo(currentuser + "\\SkoutResources\\Settings\\CSVDIR");
                    SheetsId.Delete();
                }
                System.IO.File.WriteAllText(currentuser + "\\SkoutResources\\Settings\\CSVDIR", CsvDiR);
            }
        }
    }
}
