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

namespace scoutingProject.Pages
{
    /// <summary>
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : Page
    {

        Classes_For_Function.runadbcommand runadbcommand = new Classes_For_Function.runadbcommand();

        public Import()
        {
            InitializeComponent();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            string output = runadbcommand.runADBCommand("devices");
            List<String> listOfDeviceNumbers = new List<string>();
            if (output.Length > 28)
            {
                int z = output.IndexOf("\n");
                var y = output.Substring(z + 1);
                //Initial Split
                string[] lines = y.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //Extra Split
                try
                {
                    foreach (string m in lines)
                    {
                        listOfDeviceNumbers.Add((m.Replace("\tdevice", "")).Trim());
                    }
                }
                catch (Exception) { }
            }

            int NumberInList = listOfDeviceNumbers.Count;

            switch (NumberInList)
            {
                case 0:
                    {
                        Circle1.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle2.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle3.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle4.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle5.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle6.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        break;
                    }
                case 1:
                    {
                        Circle1.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle2.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle3.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle4.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle5.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle6.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        break;
                    }
                case 2:
                    {
                        Circle1.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle2.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle3.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle4.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle5.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle6.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        break;
                    }
                case 3:
                    {
                        Circle1.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle2.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle3.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle4.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle5.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle6.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        break;
                    }
                case 4:
                    {
                        Circle1.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle2.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle3.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle4.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle5.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        Circle6.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        break;
                    }
                case 5:
                    {
                        Circle1.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle2.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle3.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle4.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle5.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle6.Fill = (SolidColorBrush)Application.Current.FindResource("IconRedBrush");
                        break;
                    }
                case 6:
                    {
                        Circle1.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle2.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle3.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle4.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle5.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        Circle6.Fill = (SolidColorBrush)Application.Current.FindResource("IconGreenBrush");
                        break;
                    }

            }

        }    

        private void import(object sender, RoutedEventArgs e)
        {
            var currentuser = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string output = runadbcommand.runADBCommand("devices");
            List<string> listOfDeviceNumbers = new List<string>();
            if (output.Length > 28)
            {
                int z = output.IndexOf("\n");
                var y = output.Substring(z + 1);
                //Initial Split
                string[] lines = y.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //Extra Split
                try
                {
                    foreach (string m in lines)
                    {

                        listOfDeviceNumbers.Add((m.Replace("\tdevice", "")).Trim());
                    }
                }
                catch (Exception) { }

                string[] delimiterCharsNicew = new string[] { "\r\n" };

                foreach (string deviceNumber in listOfDeviceNumbers)
                {
                    string files = runadbcommand.runADBCommand("-s " + deviceNumber + " shell ls sdcard/Download");
                    string[] lssoutput = files.Split(delimiterCharsNicew, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string fileExpansion in lssoutput)
                    {
                        var rRemove = fileExpansion.Replace("\r", "");
                        var nRemoved = rRemove.Replace("\n","");
                        runadbcommand.runADBCommand("-s " + deviceNumber + " pull sdcard/Download/" + nRemoved + " " + currentuser+ "//SkoutResources"+ "//DirectFileInbound");
                        runadbcommand.runADBCommand("-s " + deviceNumber + " shell rm -f sdcard/Download/" + nRemoved);
                    }
                }
            }
        }
    }
}
