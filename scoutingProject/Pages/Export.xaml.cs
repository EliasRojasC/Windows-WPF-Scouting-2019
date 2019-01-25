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
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using static Google.Apis.Sheets.v4.SheetsService;

namespace scoutingProject.Pages
{
    /// <summary>
    /// Interaction logic for Export.xaml
    /// </summary>
    public partial class Export : Page
    {
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        public static string ApplicationName1 { get => ApplicationName; set => ApplicationName = value; }

        public Export()
        {
            var currentuser = System.Environment.GetEnvironmentVariable("USERPROFILE");
            InitializeComponent();
            string sheetsid = "";
            string csvdir = "";
            if (File.Exists(currentuser + "\\SkoutResources\\Settings\\CSVDIR"))
            {
                csvdir = System.IO.File.ReadAllText(currentuser + "\\SkoutResources\\Settings\\CSVDIR");
            }
            if (File.Exists(currentuser + "\\SkoutResources\\Settings\\SHEETSID"))
            {
                sheetsid = System.IO.File.ReadAllText(currentuser + "\\SkoutResources\\Settings\\SHEETSID");
            }
            if (sheetsid.Length > 0)
            {
                CurrentSheetsId.Text = sheetsid;
            }
            if (csvdir.Length > 0)
            {
                CurrentCsvDIR.Text = csvdir;
            }
        }

        private void CreateCSV(object sender, RoutedEventArgs e)
        {
            var currentuser = System.Environment.GetEnvironmentVariable("USERPROFILE");
            var UsableDIR = CurrentCsvDIR.Text.Replace(@"\", "\\");
            if (CurrentCsvDIR.Text != "There is no set Directory")
            {
                string datetime = DateTime.Now.ToString("h_mm_ss");

                string compleatedExportFile = "";

                string currentFileName = "//" + Guid.NewGuid().ToString() + "datafile.csv";

                string[] filePaths = Directory.GetFiles(currentuser + "//SkoutResources//DirectFileInbound", "*.csv", SearchOption.AllDirectories);

                foreach (string file in filePaths)
                {
                    compleatedExportFile = compleatedExportFile + System.IO.File.ReadAllText(file) + "\r\n";
                }

                System.IO.File.WriteAllText(UsableDIR + currentFileName, compleatedExportFile);

            }
            else
            {
                MessageBox.Show("There is no set CSV directory. Please set the directory in the settings tab and try again", "Error");
            }
        }

        private void SheetsClickSender(object sender, RoutedEventArgs e)
        {
            var currentuser = System.Environment.GetEnvironmentVariable("USERPROFILE");
            if (CurrentSheetsId.Text != "There is no set Sheets ID")
            {
                UserCredential credential;

                using (var stream =
                    new FileStream(@"C:\Users\elias\SUPER IMPORTANT FILES\client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Sheets API service.
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName1,
                });
                void editSheetsCell(string cell2edit, string datatoedit)
                {
                    // Define request parameters.
                    String spreadsheetId = CurrentSheetsId.Text;
                    String range = "Sheet1!" + cell2edit;
                    SpreadsheetsResource.ValuesResource.GetRequest request =
                            service.Spreadsheets.Values.Get(spreadsheetId, range);

                    ValueRange valueRange = new ValueRange();
                    valueRange.MajorDimension = "COLUMNS";//"ROWS";//COLUMNS

                    var oblist = new List<object>() { datatoedit };
                    valueRange.Values = new List<IList<object>> { oblist };

                    SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    UpdateValuesResponse result = update.Execute();
                }

                string compleatedExportFile = "";

                string[] filePaths = Directory.GetFiles(currentuser + "//SkoutResources//DirectFileInbound", "*.csv", SearchOption.AllDirectories);

                foreach (string file in filePaths)
                {
                    compleatedExportFile = compleatedExportFile + System.IO.File.ReadAllText(file) + "\r\n";
                }

                List<string> datasets = new List<string>();

                string[] delimiterchars = new string[] { "\r\n" };

                string[] comma = new string[] { "," };

                string[] weiredString = new string[] { "System.String[]" };

                int rownum = 1;

                int alphabetindex = 0;

                int stopwatch = 0;

                bool stagger = false;

                List<string> alphabet = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

                foreach (string largeDataSet in compleatedExportFile.Split(delimiterchars, StringSplitOptions.RemoveEmptyEntries))
                {
                    datasets.Add(largeDataSet);
                }

                if(datasets.Count > 99)
                {
                    stagger = true;
                }



                foreach (string dataset in datasets)
                {
                    List<string> dataForStuff = new List<string>();
                    

                    editSheetsCell(alphabet.ElementAt(alphabetindex) + rownum, dataset);
                    

                    //foreach (string datapeice in (dataset.Split(comma, StringSplitOptions.None)))
                    //{
                    //dataForStuff.Add(datapeice);
                    //editSheetsCell(alphabet.ElementAt(alphabetindex) + rownum, datapeice);
                    //alphabetindex = alphabetindex + 1;
                    //}

                    //foreach (string indydata in dataForStuff)
                    //{
                    //editSheetsCell(alphabet.ElementAt(alphabetindex) + rownum, indydata);
                    //alphabetindex = alphabetindex + 1;
                    //System.Threading.Thread.Sleep(200);
                    //}

                    alphabetindex = 0;
                    rownum = rownum + 1;
                    if(stagger == true)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    

                    
                    

                }

            }
            else
            {
                MessageBox.Show("There is no set Sheets© ID. Please set the ID in the settings tab and try again", "Error");
            }
        }
    }
}
